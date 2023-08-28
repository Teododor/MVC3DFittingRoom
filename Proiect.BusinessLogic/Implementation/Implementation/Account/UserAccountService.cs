using Microsoft.EntityFrameworkCore;
using Proiect.BusinessLogic.Base;
using Proiect.BusinessLogic.Implementation.Implementation.Account.Models;
using Proiect.BusinessLogic.Implementation.Implementation.Account.Validations;
using Proiect.Common.DTOs;

namespace Proiect.BusinessLogic.Implementation.Implementation
{
	public class UserAccountService : BaseService
	{
		private readonly RegisterUserValidator RegisterUserValidator;

		public UserAccountService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
		{
			this.RegisterUserValidator = new RegisterUserValidator();
		}


		public byte[] getUserImageService()
		{
			return UnitOfWork.Users.Get().Where(u => u.Id == CurrentUser.Id).Select(user => user.Image).FirstOrDefault();
		}

		public CurrentUserDto Login(string email, string password)
		{
			var user = UnitOfWork.Users.Get()
				.FirstOrDefault(user => user.Email == email);

			if (user == null)
			{
				return new CurrentUserDto { IsAuthenticated = false };
			}

			var passwordHash = Proiect.BusinessLogic.Implementation.Implementation.Account.Hashing.AesEncryption.Encrypt(password);

			if (user.PasswordHash != passwordHash)
			{
				return new CurrentUserDto { IsAuthenticated = false };
			}

			return new CurrentUserDto
			{
				Id = user.Id,
				Email = user.Email,
				FirstName = user.FirstName,
				LastName = user.LastName,
				IsAuthenticated = true,
				Roles = user.Roles.Select(r => r.Name).ToList()
			};
		}


		public void RegisterNewUser(RegisterModel model)
		{
			RegisterUserValidator.Validate(model);
			var user = Mapper.Map<RegisterModel, Entities.User>(model);

			user.PasswordHash = Account.Hashing.AesEncryption.Encrypt(user.PasswordHash);

			user.CreatedAt = DateTime.Now;
			user.ModifiedAt = DateTime.Now;

			user.ModifiedBy = user.Id;
			user.Telephone = "2342342342";


			try
			{

				UnitOfWork.Users.Insert(user);
				UnitOfWork.SaveChanges();
			}
			catch (DbUpdateException ex)
			{
				var innerException = ex.InnerException;
				if (innerException != null)
				{
					Console.WriteLine(innerException);
				}
			}
		}
	}
}
