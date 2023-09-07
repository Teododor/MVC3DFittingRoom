using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Proiect.BusinessLogic.Base;
using Proiect.BusinessLogic.Implementation.FavoriteProducts.Models;
using Proiect.BusinessLogic.Implementation.Product.Models;
using Proiect.Entities;
using System.Net.WebSockets;

namespace Proiect.BusinessLogic.Implementation.Product
{
    public class ProductService : BaseService
    {


        public ProductService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {

        }
        public List<Entities.ProductInfo> GetProductsInformation()
        {
            var information = UnitOfWork.ProductInfo.Get().ToList();
            return information;
        }


        public List <ProductModel> GetPaginatedProducts(int pg, string searchText, int? selectedType)
        {
            var query = new List<ProductModel>();

			const int pageSize = 8;
			if (pg < 1)
			{
				pg = 1;
			}



            int recsCount = UnitOfWork.Products.Get().Count();
  
			int recSkip = (pg - 1) * pageSize;

            //if (selectedType == "ALL")
            //    selectedType = null;


            var mainQuery = UnitOfWork.Products.Get();

            if (!string.IsNullOrEmpty(searchText))
            {
                mainQuery = mainQuery
                    .Where(p => p.Name.Contains(searchText));
            }

            if (selectedType.HasValue)
            {
                mainQuery = mainQuery
                        .Where(p => p.ProductInfoId == selectedType.Value);
            }

            var result = mainQuery.Skip(recSkip).Take(pageSize).OrderBy(p => p.Id)/*.ToList()*/;

           var  newResult = result.Select(p => new ProductModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = (double)p.Price,
				StockNo = p.StockNo,
                ImageId = p.ImageId,
                ProductInfoId = p.ProductInfoId
            });


            var listNewResult = newResult.ToList();

            return listNewResult;
        }

        public List<ProductModel> GetProducts()
        {

            var productsList = UnitOfWork.Products.Get().Select(product => new ProductModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = (double)product.Price,
                StockNo = (int)product.StockNo,
                ImageId = (int)product.ImageId

            }).ToList();

            return productsList;

        }


		public List<ProductModel> GetProductsIncludingGivenName(string name)
		{
            /*            var query = UnitOfWork.Products.Get();

						if (!string.IsNullOrEmpty(searchText))
						{
							query = query.Where();
						}

						query = query.Skip().take().Tolist();*/

            if (name == "")
                return GetProducts();

			var productsList = UnitOfWork.Products.Get().Where(p => p.Name == name)
                
                .Select(product => new ProductModel
			{
				Id = product.Id,
				Name = product.Name,
				Description = product.Description,
				Price = (double)product.Price,
				StockNo = (int)product.StockNo,
				ImageId = (int)product.ImageId

			}).ToList();

			return productsList;

		}


		public List<int> GetUserProductsIds(int userid)
        {
            var userProductsList = UnitOfWork.UserProducts.Get()
                .Where(product => product.UserId == userid)
                .Select(product => product.ProductId)
                .ToList();

            return userProductsList;
        }

        public void AddCommentToProduct(ReviewModel model)
        {
            var comment = new Entities.Comment
            {
                UserId = model.userId,
                ProductId = model.productId,
                Review = model.review
            };

            bool hasUserAlreadyCommented = UnitOfWork
                .Comments
                .Get()
                .Where(p => model.userId == p.UserId)
                .Where(p => model.productId == p.ProductId)
                .Count() != 0;


            if(!hasUserAlreadyCommented)
            {
                UnitOfWork.Comments.Insert(comment);
                UnitOfWork.SaveChanges();
            }
            else
            {
                UnitOfWork.Comments.Update(comment);
                UnitOfWork.SaveChanges();   
            }

        }


        public void ModifyProductComment(ReviewModel model)
        {
            var comment = new Entities.Comment
            {
                UserId = model.userId,
                ProductId = model.productId,
                Review = model.review
            };

            UnitOfWork.Comments.Update(comment);
            UnitOfWork.SaveChanges();
        }



        public byte[] getProductImageService(int ProductId)
        {
            var image = UnitOfWork.Images.Get().Where(u => u.Id == ProductId).Select(image => image.ImageContent).FirstOrDefault();
            return image;

        }


        /*------------------------------------------------------------------------------------------------------------------------------*/


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

        public List <int> GetUserBasketProductsIds(int userId)
        {
            return UnitOfWork.UserProducts.Get()
                .Where(p => p.UserId == userId).Select(p => p.ProductId).ToList();    
        }

        public List<ProductModel> GetUserFavoriteProducts()
        {
            List<int> favoriteProductsIds = GetUserFavouriteProductIds(CurrentUser.Id);

            var favoriteProductsList = UnitOfWork.Products.Get().Select(product => new ProductModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = (double)product.Price, 
                StockNo = (int)product.StockNo,
                ImageId = (int)product.ImageId

            }).Where(p => favoriteProductsIds.Contains(p.Id)).ToList();
            return favoriteProductsList;
        }
        public List<ProductModel> GetUserBasketProducts()
        {
   
            var products = UnitOfWork.Products.Get()
                .Where(p => (p.UserProducts.Select((a => a.ProductId)).Contains(p.Id)))
                .Select(product => new ProductModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = (double)product.Price,
                    StockNo = (int)product.StockNo,
                    ImageId = (int)product.ImageId,
                })
                 // product.id sa fie in userproducts.id
                .ToList(); 


            var userBasketProductsList =
                
                
                
                products.Select(product => new ProductModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = (double)product.Price,
                StockNo = (int)product.StockNo,
                ImageId = (int)product.ImageId,
            }).ToList();

            return userBasketProductsList;
        }


        public void DeleteProductFromBasket(int ProductId)
        {
            Entities.UserProduct userProduct = new Entities.UserProduct
            {
                UserId = CurrentUser.Id,
                ProductId = ProductId
            };
            UnitOfWork.UserProducts.Delete(userProduct);
            UnitOfWork.SaveChanges();
        }

        public static byte[] ConvertIFormFileToByteArray(IFormFile formFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                formFile.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public void AddProductByAdminService(AddProductByAdminModel model)
        {
            var image = new Entities.Image();
            image.ImageContent = ConvertIFormFileToByteArray(model.Image);
            image.ImageName = "SomeProductImage";
            image.ContentType = "image/png";

            UnitOfWork.Images.Insert(image);
            UnitOfWork.SaveChanges();

            var product = new Entities.Product
            {
                Name = model.Name,
                Description = model.Description,
                Price = (decimal)model.Price,
                ImageId = image.Id,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
                CreatedBy = model.Id,
                DeletedAt = DateTime.Now,
                ProductDiscount = 1,
                ProductInventory = model.StockNo,
                CurrencyId = 1
            };

            UnitOfWork.Products.Insert(product);
            UnitOfWork.SaveChanges();

        }
    }
}


/*        public List<ProductModel> GetUserProductsById(int userId)
        {
            var userProductsList = UnitOfWork.UserProducts.Get()
                .Where(product => product.UserId == userId)
                .ToList();

            var mappedUserProductsList = Mapper.Map<ProductModel>(userProductsList);

            mappedUserProductsList.Name = "tbd";
            mappedUserProductsList.Description = "tbd";
            mappedUserProductsList.Price = 0;
            mappedUserProductsList.StockNo = 0;

            return mappedUserProductsList;

        }*/


/*        public List<ProductModel> GetFavouriteProducts(int userId)
        {
            var products = UnitOfWork.FavoriteProducts.Get()
                .Include(p => p.User)
                .Include(p => p.Product)
                .Select(p => new ProductModel
                {
                    Name = p.User.FirstName
                })
                .Where(p => p.UserId == userId)
                .ToList();

        }*/