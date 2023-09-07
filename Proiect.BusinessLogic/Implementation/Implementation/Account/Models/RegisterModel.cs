using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Proiect.BusinessLogic.Implementation.Implementation.Account.Models
{
	public class RegisterModel
	{
		public int Id { get; set; }
		[Required(ErrorMessage ="Email Is Required")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Password is Required")]
		public string Password { get; set; }

		[Required(ErrorMessage ="Password Confirmation is Required")]
		public string ConfirmPassword { get; set; }

		[Required(ErrorMessage ="First Name is Required")]
		public string FirstName { get; set; }

		[Required(ErrorMessage ="Last Name is Required")]
		public string LastName { get; set; }

		[Required(ErrorMessage ="Image Field Is Required")]
		public IFormFile Image { get; set; }


		/*ADDITIONAL DATA ABOUT USER*/
		[Required(ErrorMessage ="Country Name Is Required")]
		public string CountryName { get; set; }
		public List<string> AvailableCountries { get;set; }

		/*		public string ISO { get; set; }*/
		[Required(ErrorMessage ="Street Field Completion is Required")]
		public string? Street { get; set; }
		[Required(ErrorMessage ="Block Field Completion Is Required")]
		public string? Block { get; set; }

		[Required(ErrorMessage ="Number Field Completion Is Required")]
		public int? Number { get; set; }
		[Required(ErrorMessage ="Entrance Field Completion is Required")]
		public string? Entrace { get; set; }
		[Required(ErrorMessage ="Postal Code Field Completion is Required")]
		public string? PostalCode { get; set; }
		[Required(ErrorMessage ="City Field Completion is Required")]
		public string? City { get; set; }


		[Required(ErrorMessage ="Mobile Number Field Completion is Required")]
		public string? MobileNo { get; set; }

	}
}
