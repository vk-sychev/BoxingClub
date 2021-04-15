using AutoMapper;
using BoxingClub.BLL.DTO;
using BoxingClub.DAL.Entities;
using BoxingClub.WEB.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxingClub.WEB.Mapping
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
                                                                       
            CreateMap<UserDTO, SignInViewModel>(MemberList.Destination).ReverseMap()
                                                                       .ForMember(dest => dest.Id, opt => opt.Ignore())
                                                                       .ForMember(dest => dest.Email, opt => opt.Ignore())
                                                                       .ForMember(dest => dest.LockoutOnFailure, opt => opt.Ignore());

/*            CreateMap<UserDTO, UserRoleViewModel>(MemberList.Destination).ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                                                                         .ForMember(dest => dest.IsSelected, opt => opt.Ignore()).ReverseMap();*/
            CreateMap<UserDTO, UserViewModel>().ReverseMap();
            CreateMap<UserDTO, SignIn>().ReverseMap();
            CreateMap<ApplicationUser, SignIn>(MemberList.Destination).ForMember(dest => dest.Password, opt => opt.Ignore())
                                                                      .ForMember(dest => dest.LockoutOnFailure, opt => opt.Ignore())
                                                                      .ForMember(dest => dest.RememberMe, opt => opt.Ignore())
                                                                      .ReverseMap();

            CreateMap<ApplicationUser, UserDTO>(MemberList.Destination).ForMember(dest => dest.Password, opt => opt.Ignore())
                                                                       .ForMember(dest => dest.LockoutOnFailure, opt => opt.Ignore())
                                                                       .ForMember(dest => dest.RememberMe, opt => opt.Ignore())
                                                                       .ForMember(dest => dest.Role, opt => opt.Ignore())
                                                                       .ReverseMap();
        }
    }
}
