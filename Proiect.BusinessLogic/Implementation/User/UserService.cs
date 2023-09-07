using Microsoft.AspNetCore.Http;
using Proiect.BusinessLogic.Base;
using Proiect.BusinessLogic.Implementation.User.Models;
using System.Data.Common;

namespace Proiect.BusinessLogic.Implementation
{
	public class UserService : BaseService
	{
		public UserService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
		{

		}

		public List<UserModel> GetUsers()
		{
			var usersList = UnitOfWork.Users.Get()
				.Select(p => new UserModel
				{
					Id = p.Id,
					Email = p.Email,
					FirstName = p.FirstName,
					LastName = p.LastName
				})
				.ToList();

			return usersList;
		}

		public Entities.User? GetUserById(int id)
		{
			return UnitOfWork.Users.Get().FirstOrDefault(p => p.Id == id);

		}

		public Entities.User? GetUserByEmail(string email) 
		{
			return UnitOfWork.Users.Get().FirstOrDefault(p => p.Email == email);
		}

		public EditUserModel? GetEditUserModelById(int id)
		{
			var user = GetUserById(id);
			return Mapper.Map<EditUserModel>(user);
		}
		public void UpdateUser(EditUserModel model)
		{
			var user = GetUserById(model.Id);
			user.CreatedAt = DateTime.Now;
			user.ModifiedAt = DateTime.Now;
			user.FirstName = model.FirstName;
			user.LastName = model.LastName;
			user.Email = model.Email;
			user.PasswordHash = Implementation.Account.Hashing.AesEncryption.Encrypt(model.Password);

			UnitOfWork.SaveChanges();

		}


		public void AddNewUser(AddUserModel model)
		{
			var user = Mapper.Map<Entities.User>(model);
			user.CreatedAt = DateTime.Now;
			user.ModifiedAt = DateTime.Now;

			user.PasswordHash = Implementation.Account.Hashing.AesEncryption.Encrypt(user.PasswordHash);

			user.ModifiedBy = user.Id;

			try
			{
				UnitOfWork.Users.Insert(user);
				UnitOfWork.SaveChanges();
			}

			catch (DbException ex)
			{
				var innerException = ex.InnerException;
				if (innerException != null)
				{
					Console.WriteLine(innerException);
				}
			}

		}

		public void DeleteUser(int id)
		{
			var user = GetUserById(id);
			UnitOfWork.Users.Delete(user);
			UnitOfWork.SaveChanges();
		}
        public static byte[] ConvertIFormFileToByteArray(IFormFile formFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                formFile.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public void EditUserByUserService(EditUserByUserModel model)
		{
			var image = new Entities.Image();
			image.ImageContent = ConvertIFormFileToByteArray(model.Image);
			image.ImageName = "User Image";
			image.ContentType = "image/png";

			UnitOfWork.Images.Insert(image);
			UnitOfWork.SaveChanges();

			var user = new Entities.User
			{
				
				FirstName = model.FirstName,
				LastName = model.LastName,
				PasswordHash = Implementation.Account.Hashing.AesEncryption.Encrypt( model.NewPassword ),
				Email = model.Email,
				MobileNo = model.MobileNo,
				ImageId = image.Id,
				CreatedAt = DateTime.Now,
				ModifiedAt = DateTime.Now,
				NumberOfFailedAttempts = 0,

			};

			var currentUser = UnitOfWork.Users.Get()
				.Where(u => u.Id == CurrentUser.Id)
				.FirstOrDefault();


				

			UnitOfWork.Users.Update(user);
			UnitOfWork.SaveChanges();
		}
	}

}
