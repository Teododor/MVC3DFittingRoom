using Proiect.BusinessLogic.Implementation.FavoriteProducts.Models;
using Proiect.BusinessLogic.Implementation.Product.Models;
using Proiect.Entities;

namespace ProiectAcademie.Models
{
	public class ProductPageModel
	{
        public List<ProductModel> Products { get; set; }
        public List<int> FavouriteProductIds { get; set; }

        public List<int> UserProductsIds { get; set; }
    }
}
