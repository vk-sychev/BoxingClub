using AutoMapper;
using System.Linq;
using IdentityServer.BLL.Entities;
using IdentityServer.DAL.Entities;
using IdentityServer.Models;

namespace IdentityServer.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDTO, SignUpViewModel>(MemberList.Destination).ForMember(dest => dest.ConfirmPassword, opt => opt.Ignore())
                                                                       .ReverseMap()
                                                                       .ForMember(dest => dest.Id, opt => opt.Ignore())
                                                                       .ForMember(dest => dest.RememberMe, opt => opt.Ignore())
                                                                       .ForMember(dest => dest.LockoutOnFailure, opt => opt.Ignore());
                                                                       
            CreateMap<ApplicationUser, UserDTO>(MemberList.Destination).ForMember(dest => dest.Password, opt => opt.Ignore())
                                                                       .ForMember(dest => dest.LockoutOnFailure, opt => opt.Ignore())
                                                                       .ForMember(dest => dest.RememberMe, opt => opt.Ignore())
                                                                       .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.UserRoles.FirstOrDefault().Role))
                                                                       .ReverseMap();

            CreateMap<UserViewModel, UserDTO>().ReverseMap();

        }
    }
}
