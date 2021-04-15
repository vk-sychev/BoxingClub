using AutoMapper;
using BoxingClub.BLL.DTO;
using BoxingClub.DAL.Entities;
using BoxingClub.WEB.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxingClub.Web.Mapping
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleDTO, RoleViewModel>().ReverseMap();
            CreateMap<RoleDTO, Role>().ReverseMap();
            CreateMap<RoleDTO, IdentityRole>(MemberList.Source).ReverseMap();
            CreateMap<RoleViewModel, IdentityRole>(MemberList.Source).ReverseMap();
            CreateMap<Role, IdentityRole>(MemberList.Source).ForMember(src => src.Id, dest => dest.Ignore()).ReverseMap();
        }
    }
}
