using AutoMapper;
using BoxingClub.BLL.DTO;
using BoxingClub.DAL.Entities;
using BoxingClub.WEB.Models;
using Microsoft.AspNetCore.Identity;

namespace BoxingClub.Web.Mapping
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleDTO, RoleViewModel>().ReverseMap();
            CreateMap<RoleDTO, Role>().ReverseMap();
            CreateMap<RoleDTO, IdentityRole>(MemberList.Source).ReverseMap();

            CreateMap<Role, IdentityRole>(MemberList.Source).ForMember(src => src.Id, dest => dest.Ignore()).ReverseMap();
        }
    }
}
