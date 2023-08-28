using Proiect.BusinessLogic.Base;
using Proiect.DataAccess;
using Proiect.Entities;

namespace Proiect.BusinessLogic.Implementation.UserBasket
{

	public class UserBasketService : BaseService
	{
		public UserBasketService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
		{

		}
		public void AddProductToBasket(int userId, int productId)
		{
			UserProduct userProduct = new UserProduct
			{
				UserId = userId,
				ProductId = productId
			};
			UnitOfWork.UserProducts.Insert(userProduct);
			UnitOfWork.SaveChanges();
		}

		public void AddProductQuantityService(int userId, int productId)
		{
			var product = UnitOfWork.UserProducts
				.Get()
				.Where(p => p.UserId == userId)
				.Where(p => p.ProductId == productId)
				.ToList();


			var UniqueProduct = product[0];
			UniqueProduct.Quantity++;

			UnitOfWork.UserProducts.Update(UniqueProduct);
			UnitOfWork.SaveChanges();
		}
	}
}
