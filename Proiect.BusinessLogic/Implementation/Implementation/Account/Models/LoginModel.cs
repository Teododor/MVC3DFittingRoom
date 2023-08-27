namespace Proiect.BusinessLogic.Implementation.Implementation.Account.Models
{
	public class LoginModel
	{
		public int Id { get; set; }
		public string? Email { get; set; }
		public string Password { get; set; }		
		public bool AreCredentialsInvalid { get; set; }
	}
}
