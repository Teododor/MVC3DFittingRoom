using Microsoft.EntityFrameworkCore;
using Proiect.BusinessLogic.Base;
using Proiect.BusinessLogic.Implementation.FavoriteProducts.Models;
using Proiect.BusinessLogic.Implementation.Product.Models;
using Proiect.DataAccess;

namespace Proiect.BusinessLogic.Implementation.FavoriteProducts
{
	public class FavoriteProductsService : BaseService
	{
		public FavoriteProductsService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
		{

		}

		public Entities.User? GetUserById(int id)
		{
			return UnitOfWork.Users.Get().FirstOrDefault(user => user.Id == id);
		}

		public Entities.Product? GetProductById(int id)
		{
			return UnitOfWork.Products.Get().First(product => product.Id == id);
		}

		public List<int> GetUserFavouriteProductIds(int userId)
		{
			return UnitOfWork.FavoriteProducts.Get()
				.Where(p => p.UserId == userId)
				.Select(p => p.ProductId)
				.ToList();
		}

		public void AddProductToUserFavorites(int userId, int productId)
		{

			Models.AddFavoriteModel model = new Models.AddFavoriteModel();
			model.UserId = userId;
			model.ProductId = productId;

			var newModel = Mapper.Map<Entities.FavoriteProduct>(model);
			newModel.DateAddFavProduct = DateTime.Now;


			UnitOfWork.FavoriteProducts.Insert(newModel);
			UnitOfWork.SaveChanges();

		}

		public void DeleteProductFromUserFavorites( int productId)
		{
			var model = new DeleteFavoriteModel
			{
				userId = CurrentUser.Id,
				productId = productId
			};

			var newModel = Mapper.Map<Entities.FavoriteProduct>(model);
			newModel.DateAddFavProduct = DateTime.Now;


			UnitOfWork.FavoriteProducts.Delete(newModel);
			UnitOfWork.SaveChanges();
		}

	}
}

