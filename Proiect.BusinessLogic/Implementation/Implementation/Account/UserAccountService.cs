using Microsoft.EntityFrameworkCore;
using Proiect.BusinessLogic.Base;
using Proiect.BusinessLogic.Implementation.Implementation.Account.Models;
using Proiect.BusinessLogic.Implementation.Implementation.Account.Validations;
using Proiect.Common.DTOs;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Proiect.Entities;

namespace Proiect.BusinessLogic.Implementation.Implementation
{
	public class UserAccountService : BaseService
	{
		private readonly RegisterUserValidator RegisterUserValidator;

		public UserAccountService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
		{
			this.RegisterUserValidator = new RegisterUserValidator();
		}



		public byte[] getCurrentUserImageService()
		{
			return UnitOfWork.Users.Get().Where(u => u.Id == CurrentUser.Id).Select(user => user.Image).FirstOrDefault();
		}


		public int getCountryIdByCountryName(string countryName)
		{
			var countryId = UnitOfWork.Country.Get().Where(p => p.Name == countryName).Select(p => p.Id).FirstOrDefault();
			return countryId;
		}

		public int getCityIdByCityName(string cityName)
		{
			var cityId = UnitOfWork.City.Get().Where(p => p.Name == cityName).Select(p => p.Id).FirstOrDefault();
			return cityId;
		}


		public CurrentUserDto Login(string email, string password)
		{
			var user = UnitOfWork.Users.Get()
				.FirstOrDefault(user => user.Email == email);

			if (user == null)
			{
				return new CurrentUserDto { IsAuthenticated = false };
			}



			DateTime CurrentDateTime = DateTime.Now;
			


			TimeSpan timeDifference = (TimeSpan)(CurrentDateTime - user.LastFailedAttempt);

			
			bool isGreaterThan5Minutes = timeDifference.TotalMinutes > 5;

			if(isGreaterThan5Minutes) {
				user.NumberOfFailedAttempts = 0;
				UnitOfWork.Users.Update(user);
				UnitOfWork.SaveChanges();
			}

			var passwordHash = Proiect.BusinessLogic.Implementation.Implementation.Account.Hashing.AesEncryption.Encrypt(password);
			if(user.NumberOfFailedAttempts <3)
			{
				if (user.PasswordHash != passwordHash)
				{
					user.NumberOfFailedAttempts++;
					user.LastFailedAttempt = CurrentDateTime;

					UnitOfWork.Users.Update(user);
					UnitOfWork.SaveChanges();

					if (user.NumberOfFailedAttempts >= 3)
					{
						return new CurrentUserDto { IsTemporarilyBanned = true, IsAuthenticated = false };
					}
					return new CurrentUserDto { IsTemporarilyBanned = false, IsAuthenticated = false };
				}
			}

			else
			{
				return new CurrentUserDto { IsTemporarilyBanned = true, IsAuthenticated = false };
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


		public class ServiceResult
		{
			public bool Success { get; set; }
			public string Message { get; set; }
		}

		public static bool IsEmailFormatValid(string email)
		{
			string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
			Regex regex = new Regex(emailPattern);
			return regex.IsMatch(email);	
		}

		public ServiceResult RegisterNewUser(RegisterModel model)
		{
			RegisterUserValidator.Validate(model);
			var user = Mapper.Map<RegisterModel, Entities.User>(model);
			user.PasswordHash = Account.Hashing.AesEncryption.Encrypt(user.PasswordHash);
			model.ConfirmPassword = Account.Hashing.AesEncryption.Encrypt(model.ConfirmPassword);


			if (user.PasswordHash != model.ConfirmPassword)
			{
				return new ServiceResult { Success = false, Message = "Password and Confirm Password do not match" };
			}

			user.CreatedAt = DateTime.Now;
			user.ModifiedAt = DateTime.Now;

			user.ModifiedBy = user.Id;

			var image = new Entities.Image();
			image.ImageContent = user.Image;
			image.ImageName = "";
			image.ContentType = "image/png";

			var country = new Entities.Country();
			country.Name = model.CountryName;

			var city = new Entities.City();
			city.Name = model.City;

			var addressLine = new Entities.AddressLine();
			addressLine.Street = model.Street;
			addressLine.Block = model.Block;
			addressLine.Number = model.Number;
			addressLine.Entrance = model.Entrace;
			addressLine.PostalCode = model.PostalCode;

			if (!IsEmailFormatValid(user.Email))
			{
				return new ServiceResult { Success = false, Message = "Wrong Email Format" };
			}

			var role = UnitOfWork.Roles.Get().FirstOrDefault(p => p.Id == 4);
            user.Roles.Add(role);


            var userAddress = new UserAddress();

           

            UnitOfWork.Images.Insert(image);
            UnitOfWork.SaveChanges();
            user.ImageId = image.Id;

            UnitOfWork.Users.Insert(user);
            UnitOfWork.SaveChanges();
            userAddress.UserId = user.Id;


            UnitOfWork.AddressLine.Insert(addressLine);
            UnitOfWork.SaveChanges();
            userAddress.AddressLineId = addressLine.Id;


			userAddress.CountryId = getCountryIdByCountryName(country.Name);
			if(userAddress.CountryId == 0)
			{
				return new ServiceResult { Success = false, Message = "Country Must Be Selected" };
			}
			userAddress.CityId = getCityIdByCityName(city.Name);
            if(userAddress.CityId == 0)
			{
				return new ServiceResult { Success = false, Message = "City Must Be Selected" };
			}

            try
            {
				/*UnitOfWork.Country.Insert(country);
				UnitOfWork.SaveChanges();*/

				/*UnitOfWork.City.Insert(city);
	            UnitOfWork.SaveChanges();*/

				UnitOfWork.UserAddress.Insert(userAddress);
	            UnitOfWork.SaveChanges();


				

				
				


				return new ServiceResult
				{
					Success = true,
					Message = "User Registered Successfully"
				};
			}
			catch (DbUpdateException ex)
			{
				var innerException = ex.InnerException;
				if (innerException != null)
				{
					Console.WriteLine(innerException);
				}
			}

			return new ServiceResult { Success = false, Message = "For some reason user was not registered" };
		}
	}
}
