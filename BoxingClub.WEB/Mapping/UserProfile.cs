using AutoMapper;
using BoxingClub.Web.Models;
using HttpClients.Models;

namespace BoxingClub.Web.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserModel, UserViewModel>().ReverseMap();
            CreateMap<SignUpViewModel, SignUpModel>().ReverseMap();
            CreateMap(typeof(PageViewModel<UserViewModel>), typeof(PageModel<UserModel>)).ReverseMap();
        }
    }
}
