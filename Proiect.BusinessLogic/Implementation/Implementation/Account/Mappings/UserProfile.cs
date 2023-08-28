using AutoMapper;
using Microsoft.AspNetCore.Http;
using Proiect.BusinessLogic.Implementation.Implementation.Account.Models;

namespace Proiect.BusinessLogic.Implementation.Implementation.Account.Mappings
{
	public class UserProfile : Profile
	{
		public byte[] ConvertIFormFileToByteArray(IFormFile formFile)
		{
			using (var memoryStream = new MemoryStream())
			{
				formFile.CopyTo(memoryStream);
				return memoryStream.ToArray();
			}
		}
		public UserProfile() 
		{

			CreateMap<RegisterModel, Entities.User>()
				.ForMember(a => a.Email, opt => opt.MapFrom(s => s.Email))
				.ForMember(a => a.PasswordHash, opt => opt.MapFrom(s => s.Password))
				.ForMember(a => a.FirstName, opt => opt.MapFrom(s => s.FirstName))
				.ForMember(a => a.LastName, opt => opt.MapFrom(s => s.LastName))
				.ForMember(a => a.Image, opt => opt.MapFrom(s => ConvertIFormFileToByteArray(s.Image)));
		}	
	}
}
