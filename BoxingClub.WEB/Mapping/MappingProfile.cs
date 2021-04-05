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

            /*            CreateMap<SignUpViewModel, IdentityUser>(MemberList.Source);
                        CreateMap<IdentityUser, SignUpViewModel>(MemberList.Destination);*/
            CreateMap<SignUpViewModel, UserDTO>(MemberList.Source).ForSourceMember(src => src.ConfirmPassword, dest => dest.DoNotValidate());
            CreateMap<UserDTO, SignUpViewModel>(MemberList.Source).ForSourceMember(src => src.Id, dest => dest.DoNotValidate())
                                                                  .ForSourceMember(src => src.RememberMe, dest => dest.DoNotValidate())
                                                                  .ForSourceMember(src => src.LockoutOnFailure, dest => dest.DoNotValidate());

            CreateMap<SignInViewModel, UserDTO>(MemberList.Source);
            CreateMap<UserDTO, SignInViewModel>(MemberList.Source).ForSourceMember(src => src.Id, dest => dest.DoNotValidate())
                                                                  .ForSourceMember(src => src.Email, dest => dest.DoNotValidate())
                                                                  .ForSourceMember(src => src.LockoutOnFailure, dest => dest.DoNotValidate());

            CreateMap<RoleDTO, RoleViewModel>().ReverseMap();
            CreateMap<RoleDTO, Role>().ReverseMap();
            CreateMap<RoleDTO, IdentityRole>(MemberList.Source);
            CreateMap<IdentityRole, RoleDTO>(MemberList.Destination);
            CreateMap<IdentityRole, RoleViewModel>(MemberList.Destination);
            CreateMap<RoleViewModel, IdentityRole>(MemberList.Source);

            //CreateMap<UserRoleViewModel, IdentityUser>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId)).ReverseMap();
            CreateMap<UserDTO, UserRoleViewModel>(MemberList.Destination).ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                                                                         .ForMember(dest => dest.IsSelected, opt => opt.Ignore());

            CreateMap<UserRoleViewModel, UserDTO>(MemberList.Source).ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId))
                                                                    .ForSourceMember(src => src.IsSelected, dest => dest.DoNotValidate());

            CreateMap<IdentityRole, Role>(MemberList.Destination);
            CreateMap<Role, IdentityRole>(MemberList.Source).ForMember(src => src.Id, dest => dest.Ignore());

            CreateMap<RoleViewModel, RoleDTO>().ReverseMap();
            CreateMap<UserDTO, User>().ReverseMap();
            //CreateMap<UserRoleViewModel, UserDTO>().ReverseMap();

            CreateMap<IdentityUser, User>(MemberList.Destination).ForMember(dest => dest.Password, opt => opt.Ignore())
                                                                 .ForMember(dest => dest.LockoutOnFailure, opt => opt.Ignore())
                                                                 .ForMember(dest => dest.RememberMe, opt => opt.Ignore()).ReverseMap();

            CreateMap<User, IdentityUser>(MemberList.Source).ForSourceMember(src => src.Password, dest => dest.DoNotValidate())
                                                            .ForSourceMember(src => src.RememberMe, dest => dest.DoNotValidate())
                                                            .ForSourceMember(src => src.LockoutOnFailure, dest => dest.DoNotValidate())
                                                            //.ForSourceMember(src => src.Id, dest => dest.DoNotValidate())
                                                            .ForMember(src => src.Id, dest => dest.Ignore());


            CreateMap<IdentityUser, UserDTO>(MemberList.Destination).ForMember(dest => dest.Password, opt => opt.Ignore())
                                                                    .ForMember(dest => dest.LockoutOnFailure, opt => opt.Ignore())
                                                                    .ForMember(dest => dest.RememberMe, opt => opt.Ignore());
            //CreateMap<UserDTO, IdentityUser>(MemberList.Source);

            CreateMap<IdentityResult, AccountResultDTO>(MemberList.Destination);
            CreateMap<AccountResultDTO, IdentityResult>(MemberList.Source);

            CreateMap<IdentityError, AccountErrorDTO>(MemberList.Destination);
            CreateMap<AccountErrorDTO, IdentityError>(MemberList.Source);

            CreateMap<SignInResult, SignInResultDTO>(MemberList.Destination);
            CreateMap<SignInResultDTO, SignInResult>(MemberList.Source);
        }
    }
}
