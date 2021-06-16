using AutoMapper;
using HttpClients.Models;
using Students.API.Models;
using Students.BLL.DomainEntities;

namespace Students.API.Mapping
{
    public class UserClientProfile : Profile
    {
        public UserClientProfile()
        {
            CreateMap<SearchModelDTO, SearchModel>().ReverseMap();
            CreateMap(typeof(PageViewModel<UserViewModel>), typeof(PageModel<UserModel>)).ReverseMap();
            CreateMap<UserModel, UserViewModel>().ReverseMap();
            CreateMap<UserModel, UserDTO>().ReverseMap();
        }
    }
}
