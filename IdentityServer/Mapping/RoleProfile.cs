using AutoMapper;
using IdentityServer.BLL.Entities;
using IdentityServer.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Mapping
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleDTO, ApplicationRole>(MemberList.Source).ReverseMap();
        }
    }
}
