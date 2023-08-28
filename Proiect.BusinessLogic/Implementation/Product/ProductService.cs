using Proiect.BusinessLogic.Base;
using Proiect.BusinessLogic.Implementation.FavoriteProducts.Models;
using Proiect.BusinessLogic.Implementation.Product.Models;


namespace Proiect.BusinessLogic.Implementation.Product
{
    public class ProductService : BaseService
    {
        public ProductService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {

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
            UnitOfWork.Comments.Insert(comment);
            UnitOfWork.SaveChanges();
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
            List<int> userBasketProducts = GetUserBasketProductsIds(CurrentUser.Id);
            var userBasketProductsList = UnitOfWork.Products.Get().Select(product => new ProductModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = (double)product.Price,
                StockNo = (int)product.StockNo,
                ImageId = (int)product.ImageId

            }).Where(p => userBasketProducts.Contains(p.Id)).ToList();
            return userBasketProductsList;
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