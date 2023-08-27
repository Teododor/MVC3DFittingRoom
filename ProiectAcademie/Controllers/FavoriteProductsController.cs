using Microsoft.AspNetCore.Mvc;
using Proiect.BusinessLogic.Implementation.FavoriteProducts;
using Proiect.BusinessLogic.Implementation.FavoriteProducts.Models;
using Proiect.WebApp.Code.Base;

namespace ProiectAcademie.Controllers
{
	public class FavoriteProductsController : BaseController
	{

		private readonly FavoriteProductsService Service;

		public FavoriteProductsController(ControllerDependencies dependencies, FavoriteProductsService service) : base(dependencies)
		{
			this.Service = service;
		}


		[HttpPost]
		public IActionResult AddFavorite([FromBody] AddFavoriteModel model)
		{
			/*			if(model is null)
						{
							return BadRequest("The Model is Null");
						}*/
			Service.AddProductToUserFavorites(model.UserId, model.ProductId);
			return View();
		}


		[HttpPost]
		public IActionResult DeleteFavorite([FromBody] DeleteFavoriteModel model)
		{
			/*			if(model is null)
						{
							return BadRequest("The Model is null");
						}*/
			Service.DeleteProductFromUserFavorites(model.userId, model.productId);
			return View();
		}

		public IActionResult Index()
		{
			return View();
		}
	}


}
