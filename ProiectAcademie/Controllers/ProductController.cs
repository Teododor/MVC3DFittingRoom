using Microsoft.AspNetCore.Mvc;
using Proiect.BusinessLogic.Implementation;
using Proiect.BusinessLogic.Implementation.FavoriteProducts;
using Proiect.BusinessLogic.Implementation.Product;
using Proiect.BusinessLogic.Implementation.Product.Models;
using Proiect.BusinessLogic.Implementation.UserBasket;
using Proiect.WebApp.Code.Base;
using ProiectAcademie.Models;

namespace ProiectAcademie.Controllers
{
	public class ProductController : BaseController
	{

		private readonly ProductService Service;
		private readonly FavoriteProductsService FavoriteProductService;
		private readonly UserBasketService UserBasketService;


		public ProductController(ControllerDependencies dependencies, ProductService service, FavoriteProductsService favoriteProductService, UserBasketService userBasketService) : base(dependencies)
		{
			this.Service = service;
			this.FavoriteProductService = favoriteProductService;
			this.UserBasketService = userBasketService;
		}

		[HttpGet]
		public IActionResult Index()
		{
			var products = Service.GetProducts();
			var favouriteProducts = FavoriteProductService.GetUserFavouriteProductIds(CurrentUser.Id);
			var userProductsIds = Service.GetUserProductsIds(CurrentUser.Id);


			var model = new ProductPageModel
			{
				Products = products,
				FavouriteProductIds = favouriteProducts,
				UserProductsIds = userProductsIds
			};

			return View(model);
		}



		[HttpGet]
		public IActionResult FavoriteProducts()
		{
			var products = Service.GetUserFavoriteProducts();
			var favouriteProducts = FavoriteProductService.GetUserFavouriteProductIds(CurrentUser.Id);
			var userProductsIds = Service.GetUserProductsIds(CurrentUser.Id);


			var model = new ProductPageModel
			{
				Products = products,
				FavouriteProductIds = favouriteProducts,
				UserProductsIds = userProductsIds
			};

			return View(model);
		}

		[HttpGet]
		public IActionResult ViewBasket()
		{
			var products = Service.GetUserBasketProducts();
            var favouriteProducts = FavoriteProductService.GetUserFavouriteProductIds(CurrentUser.Id);
            var userProductsIds = Service.GetUserProductsIds(CurrentUser.Id);
            var model = new ProductPageModel
			{
				Products = products,
				FavouriteProductIds = favouriteProducts,
				UserProductsIds = userProductsIds

			};
			return View(model);
		}

		[HttpGet]
		public IActionResult AddRating()
		{
			return View();
		}

		[HttpPost]
		public IActionResult GiveReview([FromBody] ReviewModel model)
		{
			var givenReview = new ReviewModel
			{
				review = model.review,
				productId = model.productId,
				userId = CurrentUser.Id
			};

			if (givenReview.review is not null)
			{

				Service.AddCommentToProduct(givenReview);
			}
			return Ok();
		}
		[HttpPost]
		public IActionResult ModifyReview([FromBody] ReviewModel model)
		{
			var newGivenReview = new ReviewModel
			{
				review = model.review,
				productId = model.productId,
				userId = CurrentUser.Id
			};

			if(newGivenReview.review is not null) 
			{
				Service.ModifyProductComment(newGivenReview);
			}

			return Ok();
		}

		[HttpGet]
		public IActionResult DisplayProductImage(int ProductId)
		{
			var image = Service.getProductImageService(ProductId);
				return File(image, "image/png");
		}
	}


}
