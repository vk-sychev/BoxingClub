using AutoMapper;
using BoxingClub.BLL.DomainEntities;
using BoxingClub.DAL.Entities;
using BoxingClub.Web.Models;
using HttpClients.Models;
using Microsoft.AspNetCore.Identity;

namespace BoxingClub.Web.Mapping
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleDTO, RoleViewModel>().ReverseMap();
            CreateMap<RoleModel, RoleDTO>().ReverseMap();
            CreateMap<RoleViewModel, RoleModel>().ReverseMap();
        }
    }
}
