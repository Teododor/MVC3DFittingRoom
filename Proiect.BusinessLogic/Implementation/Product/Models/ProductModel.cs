using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect.BusinessLogic.Implementation.Product.Models
{
	public class ProductModel
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public string? Description { get; set; }

		public Double Price { get; set; }

		public int? StockNo { get; set; }

		public int ImageId { get; set; }

/*		public byte[] Image { get; set; }*/

		public int? ProductInfoId { get; set; }
	
	}
}