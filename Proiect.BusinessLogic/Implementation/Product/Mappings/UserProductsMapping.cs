using AutoMapper;
using Proiect.BusinessLogic.Implementation.Product.Models;
using Proiect.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect.BusinessLogic.Implementation.Product.Mappings
{
	public class UserProductsMapping : Profile
	{
		public UserProductsMapping() 
		{
			CreateMap<UserProduct, ProductModel>()
				.ForMember(a => a.Id, opt => opt.MapFrom(opt => opt.ProductId));
				/*.ForMember(a => a.ImageId, opt => opt.MapFrom(opt => opt.ImageId));*/
		}	
	}
}
