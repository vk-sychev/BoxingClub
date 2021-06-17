using AutoMapper;
using BoxingClub.BLL.DomainEntities;
using BoxingClub.DAL.Entities;
using BoxingClub.Web.Models;
using System.Linq;
using HttpClients.Models;

namespace BoxingClub.Web.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDTO, UserViewModel>().ReverseMap();
            CreateMap<UserModel, UserViewModel>().ReverseMap();
            CreateMap<UserModel, UserDTO>().ReverseMap();
            CreateMap<SignUpViewModel, SignUpModel>().ReverseMap();
            CreateMap(typeof(PageViewModel<UserViewModel>), typeof(PageModel<UserModel>)).ReverseMap();
        }
    }
}
