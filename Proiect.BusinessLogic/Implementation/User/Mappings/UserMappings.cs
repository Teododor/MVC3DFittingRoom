using AutoMapper;
using Proiect.BusinessLogic.Implementation.User.Models;

namespace Proiect.BusinessLogic.Implementation.User.Mappings
{
    public class UserMappings : Profile
    {
        public UserMappings()

        {
            CreateMap<AddUserModel, Entities.User >()
                .ForMember(a => a.FirstName, opt => opt.MapFrom(s => s.FirstName))
                .ForMember(a => a.LastName, opt => opt.MapFrom(s => s.LastName))
                .ForMember(a => a.Email, opt => opt.MapFrom(s => s.Email));



            CreateMap<Entities.User, EditUserModel>()
                .ForMember(a => a.FirstName, opt => opt.MapFrom(s => s.FirstName))
                .ForMember(a => a.LastName, opt => opt.MapFrom(s => s.LastName))
                .ForMember(a => a.Email, opt => opt.MapFrom(s => s.Email));


        }

    }
}

