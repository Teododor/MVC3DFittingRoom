using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect.BusinessLogic.Implementation.FavoriteProducts.Models
{
	public class FavoriteProductModel
	{
		public int Id { get; set; }	
		public string Name { get; set; }
		public string Description { get; set; }	
		public decimal Price { get; set; }

        public bool IsFavouriteProduct { get; set; }

    }
}
