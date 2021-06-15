﻿using AutoMapper;
using BoxingClub.BLL.DomainEntities;
using BoxingClub.DAL.Entities;
using BoxingClub.Web.Models;
using System.Linq;

namespace BoxingClub.Web.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            /*            CreateMap<UserDTO, SignUpViewModel>(MemberList.Destination).ForMember(dest => dest.ConfirmPassword, opt => opt.Ignore())
                                                                                   .ReverseMap()
                                                                                   .ForMember(dest => dest.Id, opt => opt.Ignore())
                                                                                   .ForMember(dest => dest.RememberMe, opt => opt.Ignore())
                                                                                   .ForMember(dest => dest.LockoutOnFailure, opt => opt.Ignore());

                        CreateMap<UserDTO, SignInViewModel>(MemberList.Destination).ReverseMap()
                                                                                   .ForMember(dest => dest.Id, opt => opt.Ignore())
                                                                                   .ForMember(dest => dest.Email, opt => opt.Ignore())
                                                                                   .ForMember(dest => dest.LockoutOnFailure, opt => opt.Ignore());


                        CreateMap<UserDTO, SignIn>().ReverseMap();
                        CreateMap<ApplicationUser, SignIn>(MemberList.Destination).ForMember(dest => dest.Password, opt => opt.Ignore())
                                                                                  .ForMember(dest => dest.LockoutOnFailure, opt => opt.Ignore())
                                                                                  .ForMember(dest => dest.RememberMe, opt => opt.Ignore())
                                                                                  .ReverseMap();

                        CreateMap<ApplicationUser, UserDTO>(MemberList.Destination).ForMember(dest => dest.Password, opt => opt.Ignore())
                                                                                   .ForMember(dest => dest.LockoutOnFailure, opt => opt.Ignore())
                                                                                   .ForMember(dest => dest.RememberMe, opt => opt.Ignore())
                                                                                   .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.UserRoles.FirstOrDefault().Role))
                                                                                   .ReverseMap();  */
            CreateMap<UserDTO, UserViewModel>().ReverseMap();

        }
    }
}
