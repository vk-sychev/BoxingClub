using AutoMapper;
using HttpClients.Models;
using Students.API.Models;
using Students.BLL.DomainEntities;

namespace Students.API.Mapping
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleDTO, RoleViewModel>().ReverseMap();
            CreateMap<RoleModel, RoleDTO>().ReverseMap();
        }
    }
}
