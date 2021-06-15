using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BoxingClub.BLL.DomainEntities;
using BoxingClub.Web.Models;
using HttpClients.Models;

namespace BoxingClub.Web.Mapping
{
    public class UserClientProfile : Profile
    {
        public UserClientProfile()
        {
            CreateMap<SignUpViewModel, SignUpModel>().ReverseMap();
            CreateMap<SearchModelDTO, SearchModel>().ReverseMap();
            CreateMap(typeof(PageViewModel<UserViewModel>), typeof(PageModel<UserModel>)).ReverseMap();
            CreateMap<UserModel, UserViewModel>().ReverseMap();
            CreateMap<UserModel, UserDTO>().ReverseMap();
            CreateMap<RoleViewModel, RoleModel>().ReverseMap();
        }
    }
}
