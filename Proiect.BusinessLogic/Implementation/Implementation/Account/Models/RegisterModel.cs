using Microsoft.AspNetCore.Http;

namespace Proiect.BusinessLogic.Implementation.Implementation.Account.Models
{
	public class RegisterModel
	{
		public int Id { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }	
		public string FirstName { get; set; }
		public string LastName { get; set; }	

		public IFormFile Image { get; set; } 

/*		public DateTime BirthDay { get; set; }
		public int CityId { get; set; }
		public int CountyId { get; set; }
		public byte GenderId { get; set; }
		public byte PrivacyId { get; set; }*/

	}
}
