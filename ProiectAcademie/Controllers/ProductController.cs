using AutoMapper.Configuration.Conventions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Proiect.BusinessLogic.Implementation;
using Proiect.BusinessLogic.Implementation.FavoriteProducts;
using Proiect.BusinessLogic.Implementation.Product;
using Proiect.BusinessLogic.Implementation.Product.Models;
using Proiect.BusinessLogic.Implementation.UserBasket;
using Proiect.BusinessLogic.Implementation.UserRating;
using Proiect.WebApp.Code.Base;
using ProiectAcademie.Models;

namespace ProiectAcademie.Controllers
{
	public class ProductController : BaseController
	{

		private readonly ProductService Service;
		private readonly FavoriteProductsService FavoriteProductService;
		private readonly UserBasketService UserBasketService;
		private readonly UserRatingService UserRatingsService;


		public ProductController(ControllerDependencies dependencies, ProductService service, FavoriteProductsService favoriteProductService, UserBasketService userBasketService, UserRatingService userRatingService) : base(dependencies)
		{
			this.Service = service;
			this.FavoriteProductService = favoriteProductService;
			this.UserBasketService = userBasketService;
			this.UserRatingsService = userRatingService;
		}

		[HttpGet]
		public IActionResult Index(int? SelectedType, string SearchText, int pg = 1)
		{
			var totalProducts = Service.GetProducts();
			var products = Service.GetPaginatedProducts(pg, SearchText, SelectedType);
			var favouriteProducts = FavoriteProductService.GetUserFavouriteProductIds(CurrentUser.Id);
			var userProductsIds = Service.GetUserProductsIds(CurrentUser.Id);

			int recsCount = 0;
			if (SearchText is not null)
			{
				recsCount = Service.GetProductsIncludingGivenName(SearchText).Count();
			}
			else
				recsCount = totalProducts.Count;

			int pageSize = 8;

			var pager = new Pager(recsCount, pg, pageSize, SearchText, SelectedType);
			this.ViewBag.Pager = pager;

			var productsInformation = Service.GetProductsInformation();
			var userRatings = UserRatingsService.GetUserRatings();

			var model = new ProductPageModel
			{
				Products = products,
				FavouriteProductIds = favouriteProducts,
				UserProductsIds = userProductsIds,
				ProductsInformation = productsInformation,
				UserRatings = userRatings
			};
			return View(model);
		}



		[HttpGet]
		public IActionResult FavoriteProducts()
		{
			var products = Service.GetUserFavoriteProducts();
			var favouriteProducts = FavoriteProductService.GetUserFavouriteProductIds(CurrentUser.Id);
			var userProductsIds = Service.GetUserProductsIds(CurrentUser.Id);
			var userRatings = UserRatingsService.GetUserRatings();

			var model = new ProductPageModel
			{
				Products = products,
				FavouriteProductIds = favouriteProducts,
				UserProductsIds = userProductsIds,
				UserRatings = userRatings 
			};

			return View(model);
		}

		[HttpGet]
		public IActionResult ViewBasket()
		{
			var products = Service.GetUserBasketProducts();
			var favouriteProducts = FavoriteProductService.GetUserFavouriteProductIds(CurrentUser.Id);
			var userProductsIds = Service.GetUserProductsIds(CurrentUser.Id);
			var userRatings = UserRatingsService.GetUserRatings();
			var model = new ProductPageModel
			{
				Products = products,
				FavouriteProductIds = favouriteProducts,
				UserProductsIds = userProductsIds,
				UserRatings = userRatings	

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

			if (newGivenReview.review is not null)
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

		[HttpPost]
		public IActionResult RemoveProductFromBasket(int ProductId)
		{
			Service.DeleteProductFromBasket(ProductId);
			return Json(new { message = "Successfully removed the product from basket" });
		}


		[HttpGet]
		public IActionResult AddProductByAdmin()
		{

			return View();
		}
		[Authorize(Roles = "Admin")]
		[HttpPost]
		public IActionResult AddProductByAdmin(AddProductByAdminModel model)
		{
			Service.AddProductByAdminService(model);
			return View();
		}

	}


}
