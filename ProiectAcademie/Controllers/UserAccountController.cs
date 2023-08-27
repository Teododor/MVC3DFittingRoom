using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Abstractions;
using Proiect.BusinessLogic.Implementation.Implementation;
using Proiect.BusinessLogic.Implementation.Implementation.Account.Models;
using Proiect.Common.DTOs;
using Proiect.WebApp.Code.Base;
using ProiectAcademie.Models;
using System.Security.Claims;

namespace ProiectAcademie.Controllers
{
	public class UserAccountController : BaseController
	{

		protected readonly UserAccountService Service;

		public UserAccountController(ControllerDependencies dependencies, UserAccountService service)
		   : base(dependencies)
		{
			this.Service = service;
		}

		[HttpGet]
		public IActionResult Register()	
		{
			var model = new RegisterModel();
			return View(model);
		}

		[HttpPost]
		public IActionResult Register(RegisterModel model)
		{
			if (model == null)
			{
				return View("Error_NotFound");
			}
/*			var newModel = new RegisterModel
			{
				Email = model.Email,
				FirstName = model.FirstName,
				LastName = model.LastName,
				Image = ConvertIFormFileToByteArray(model.Image)
			};*/

			Service.RegisterNewUser(model);
			return RedirectToAction("Index", "Home");
		}

		[HttpGet]
		public IActionResult Login()
		{
			var model = new LoginModel();
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginModel model)
		{
			var user = Service.Login(model.Email, model.Password);

			if (!user.IsAuthenticated)
			{
				model.AreCredentialsInvalid = true;
				return View(model);
			}



			await LogIn(user);

			return RedirectToAction("Index", "Home");
		}

		private async Task LogIn(CurrentUserDto user)
		{
			var claims = new List<Claim>
			{
				new Claim("Id", user.Id.ToString()),
				new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
				new Claim(ClaimTypes.Email, user.Email)
			};

			user.Roles.ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));

			var identity = new ClaimsIdentity(claims, "Cookies");
			var principal = new ClaimsPrincipal(identity);

			await HttpContext.SignInAsync
				(scheme: "ProiectAcademieCookies",
					principal: principal
				);
		}


		[HttpGet]
		public async Task<IActionResult> Logout()
		{
			await LogOut();
			return RedirectToAction("Index", "Home");
		}
		private async Task LogOut()
		{
			await HttpContext.SignOutAsync(scheme: "ProiectAcademieCookies");
		}
	}
}

