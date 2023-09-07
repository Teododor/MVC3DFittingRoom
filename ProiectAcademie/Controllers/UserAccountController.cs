using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Proiect.BusinessLogic.Implementation.Countries;
using Proiect.BusinessLogic.Implementation.Implementation;
using Proiect.BusinessLogic.Implementation.Implementation.Account.Models;
using Proiect.Common.DTOs;
using Proiect.WebApp.Code.Base;
using System.Security.Claims;
using Proiect.BusinessLogic.Implementation.City;
using static Proiect.BusinessLogic.Implementation.Implementation.UserAccountService;

namespace ProiectAcademie.Controllers
{
	public class UserAccountController : BaseController
	{

		protected readonly UserAccountService Service;
		protected readonly CountriesService countriesService;
		protected readonly CityService cityService;



		public List <string?> GetAvailableCountries()
		{
			var availableCountries = countriesService.GetAvailableCountries();
			return availableCountries;
		}

		public List<string?> GetAvailableCities()
		{
			var availableCities = cityService.GetAllCities();
			return availableCities;
		}

		public UserAccountController(ControllerDependencies dependencies, UserAccountService service, CountriesService countriesservice, CityService cityservice)
		   : base(dependencies)
		{
			this.Service = service;
			this.countriesService = countriesservice;
			this.cityService = cityservice;
		}

		[HttpGet]
		public IActionResult Register()	
		{
			var model = new RegisterModel();
			this.ViewBag.AvailableCountries = GetAvailableCountries();
			this.ViewBag.AvailableCities = GetAvailableCities();
			return View(model);
		}

		[HttpPost]
		public IActionResult Register(RegisterModel model)
		{
			if (model == null)
			{
				return View("Error_NotFound");
			}

			this.ViewBag.AvailableCountries = GetAvailableCountries();
			this.ViewBag.AvailableCities = GetAvailableCities();

			ServiceResult result =  Service.RegisterNewUser(model);
			if (!result.Success)
			{
				ModelState.AddModelError(string.Empty, result.Message);
				return View(model);
			}
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

			if(user.IsTemporarilyBanned == true)
			{
				model.isAccountTemporarilyBanned = true;
				return View(model);
			}

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

