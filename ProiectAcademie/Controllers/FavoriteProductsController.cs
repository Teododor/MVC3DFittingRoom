using Microsoft.AspNetCore.Mvc;
using Proiect.BusinessLogic.Implementation.FavoriteProducts;
using Proiect.BusinessLogic.Implementation.FavoriteProducts.Models;
using Proiect.BusinessLogic.Implementation.Product;
using Proiect.Entities;
using Proiect.WebApp.Code.Base;
using ProiectAcademie.Models;

namespace ProiectAcademie.Controllers
{
	public class FavoriteProductsController : BaseController
	{

		private readonly ProductService _ProductService;
		private readonly FavoriteProductsService _FavoriteProductsService;


		public FavoriteProductsController(ControllerDependencies dependencies, ProductService ProductService, FavoriteProductsService FavoriteProductsService) : base(dependencies)
		{
			this._FavoriteProductsService = FavoriteProductsService;
			this._ProductService = ProductService;
		}

		[HttpPost]
		public IActionResult AddFavorite([FromBody] AddFavoriteModel model)
		{
			_FavoriteProductsService.AddProductToUserFavorites(model.UserId, model.ProductId);
			return Ok();
		}


		[HttpPost]
		public IActionResult DeleteFavorite([FromBody] DeleteFavoriteModel model)
		{
			_FavoriteProductsService.DeleteProductFromUserFavorites(model.productId);
            return Ok();
        }

		[HttpPost]
		public IActionResult DeleteFavoriteFromFavPage(int productId)
		{
			_FavoriteProductsService.DeleteProductFromUserFavorites(productId);
			return Json(new { message = "Successfully deleted from favorites." });	
		}

		

		public IActionResult Index()
		{
			return View();
		}
	}


}
