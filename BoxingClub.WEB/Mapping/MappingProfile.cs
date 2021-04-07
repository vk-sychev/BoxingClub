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
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<StudentFullDTO, StudentFullViewModel>().ReverseMap();
            CreateMap<StudentLiteDTO, StudentLiteViewModel>().ReverseMap();
            CreateMap<StudentLiteDTO, Student>(MemberList.Source).ReverseMap();
            CreateMap<StudentFullDTO, Student>().ReverseMap();

            CreateMap<UserDTO, SignUpViewModel>(MemberList.Destination).ForMember(dest => dest.ConfirmPassword, opt => opt.Ignore())
                                                                       .ReverseMap().ForMember(dest => dest.Id, opt => opt.Ignore())
                                                                       .ForMember(dest => dest.RememberMe, opt => opt.Ignore())
                                                                       .ForMember(dest => dest.LockoutOnFailure, opt => opt.Ignore());
                                                                       
            CreateMap<UserDTO, SignInViewModel>(MemberList.Destination).ReverseMap()
                                                                       .ForMember(dest => dest.Id, opt => opt.Ignore())
                                                                       .ForMember(dest => dest.Email, opt => opt.Ignore())
                                                                       .ForMember(dest => dest.LockoutOnFailure, opt => opt.Ignore());

            CreateMap<RoleDTO, RoleViewModel>().ReverseMap();
            CreateMap<RoleDTO, Role>().ReverseMap();
            CreateMap<RoleDTO, IdentityRole>(MemberList.Source).ReverseMap();
            CreateMap<RoleViewModel, IdentityRole>(MemberList.Source).ReverseMap();

            CreateMap<UserDTO, UserRoleViewModel>(MemberList.Destination).ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                                                                         .ForMember(dest => dest.IsSelected, opt => opt.Ignore()).ReverseMap();

            CreateMap<Role, IdentityRole>(MemberList.Source).ForMember(src => src.Id, dest => dest.Ignore()).ReverseMap();

            CreateMap<RoleViewModel, RoleDTO>().ReverseMap();
            CreateMap<UserDTO, User>().ReverseMap();

            CreateMap<IdentityUser, User>(MemberList.Destination).ForMember(dest => dest.Password, opt => opt.Ignore())
                                                                 .ForMember(dest => dest.LockoutOnFailure, opt => opt.Ignore())
                                                                 .ForMember(dest => dest.RememberMe, opt => opt.Ignore()).ReverseMap();

            CreateMap<IdentityUser, UserDTO>(MemberList.Destination).ForMember(dest => dest.Password, opt => opt.Ignore())
                                                                    .ForMember(dest => dest.LockoutOnFailure, opt => opt.Ignore())
                                                                    .ForMember(dest => dest.RememberMe, opt => opt.Ignore()).ReverseMap();
          
            CreateMap<AccountResultDTO, IdentityResult>(MemberList.Source).ReverseMap();
            CreateMap<AccountErrorDTO, IdentityError>(MemberList.Source).ReverseMap();
            CreateMap<SignInResultDTO, SignInResult>(MemberList.Source).ReverseMap();


            CreateMap<Coach, CoachDTO>().ReverseMap();
            CreateMap<CoachDTO, CoachViewModel>().ReverseMap();

            CreateMap<BoxingGroup, BoxingGroupDTO>().ReverseMap();
            CreateMap<BoxingGroupDTO, BoxingGroupViewModel>().ReverseMap();
        }
    }
}
