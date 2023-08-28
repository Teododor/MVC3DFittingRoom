using AutoMapper;
using Proiect.BusinessLogic.Implementation.FavoriteProducts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect.BusinessLogic.Implementation.FavoriteProducts.Mappings
{
	public class FavoriteProduct : Profile
	{
		public FavoriteProduct()
		{
			CreateMap<AddFavoriteModel, Entities.FavoriteProduct>()
				.ForMember(a => a.ProductId, opt => opt.MapFrom(opt => opt.ProductId))
				.ForMember(a => a.UserId, opt => opt.MapFrom(opt => opt.UserId));


			CreateMap<DeleteFavoriteModel, Entities.FavoriteProduct>()
				.ForMember(a => a.ProductId, opt => opt.MapFrom(opt => opt.productId))
				.ForMember(a => a.UserId, opt => opt.MapFrom(opt => opt.userId));

		}


	}
}
