using Microsoft.AspNetCore.Mvc;
using Proiect.BusinessLogic.Implementation;
using Proiect.BusinessLogic.Implementation.UserBasket;
using Proiect.BusinessLogic.Implementation.UserBasket.Models;
using Proiect.WebApp.Code.Base;
using System.Runtime.Intrinsics.X86;

namespace ProiectAcademie.Controllers
{
	public class UserBasketController : BaseController
	{

		protected readonly UserBasketService Service;
		public UserBasketController(ControllerDependencies dependencies, UserBasketService service) : base(dependencies)
		{
			this.Service = service;
		}

		public IActionResult Index()
		{
			return View();
		}


		[HttpPost]
		public IActionResult AddProduct([FromBody] UserBasketModel model)
		{
			Service.AddProductToBasket(model.userId, model.productId);
			return View();
		}


		[HttpPost]
		public IActionResult AddProductQuantity([FromBody] UserBasketModel model)
		{
			Service.AddProductQuantityService(model.userId, model.productId);
			return View();
		}

	}
}
