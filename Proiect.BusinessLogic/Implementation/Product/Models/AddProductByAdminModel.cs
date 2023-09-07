using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect.BusinessLogic.Implementation.Product.Models
{
    public class AddProductByAdminModel
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; } 
        public double Price { get; set; }
        public int StockNo { get; set; }
        public IFormFile Image { get; set; }
    }
}
