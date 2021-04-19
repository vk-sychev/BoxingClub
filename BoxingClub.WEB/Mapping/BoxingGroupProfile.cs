using AutoMapper;
using BoxingClub.BLL.DTO;
using BoxingClub.DAL.Entities;
using BoxingClub.WEB.Models;

namespace BoxingClub.Web.Mapping
{
    public class BoxingGroupProfile : Profile
    {
        public BoxingGroupProfile()
        {
            CreateMap<BoxingGroup, BoxingGroupDTO>().ReverseMap();
            CreateMap<BoxingGroupDTO, BoxingGroupFullViewModel>().ReverseMap();
            CreateMap<BoxingGroupLiteViewModel, BoxingGroupDTO>().ForMember(dest => dest.Students, opt => opt.Ignore())
                                                                 .ReverseMap();
        }
    }
}
