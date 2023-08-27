using FluentValidation;
using Proiect.BusinessLogic.Implementation.Implementation.Account.Models;

namespace Proiect.BusinessLogic.Implementation.Implementation.Account.Validations
{
	public class RegisterUserValidator : AbstractValidator<RegisterModel>
	{
		public RegisterUserValidator() 
		{
			//validare de obligatoriu, dimensiuninile din BD, pt email structura, userBirthDate daca il mai cer
			//daca mail-ul este unic, alte validari unde sa verific daca este ceva deja in baza
			RuleFor(R => R.Email).NotEmpty().WithMessage("Camp Obligatoriu");
			RuleFor(R => R.Password).NotEmpty().WithMessage("Camp Obligatoriu");
			RuleFor(R => R.FirstName).NotEmpty().WithMessage("Camp Obligatoriu");
			RuleFor(R => R.LastName).NotEmpty().WithMessage("Camp Obligatoriu");
			/*RuleFor(R => R.BirthDay).NotEmpty().WithMessage("Camp obligatoriu");
			RuleFor(R => R.CityId).NotEmpty().WithMessage("Camp obligatoriu");
			RuleFor(R => R.CountyId).NotEmpty().WithMessage("Camp obligatoriu");
			RuleFor(R => R.GenderId).NotEmpty().WithMessage("Camp obligatoriu");
			RuleFor(R => R.PrivacyId).NotEmpty().WithMessage("Camp obligatoriu");*/
		}

		public bool NotAlreadyExist(string email)
		{
			return true;
		}
	}
}
