using AutoMapper;
using BoxingClub.BLL.DomainEntities;
using BoxingClub.DAL.Entities;
using BoxingClub.Web.Models;
using HttpClients.Models;

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
            CreateMap<BoxingGroupLiteModel, BoxingGroupLiteViewModel>().ReverseMap();
            CreateMap<BoxingGroupFullModel, BoxingGroupFullViewModel>().ReverseMap();
            CreateMap<BoxingGroupLiteModel, BoxingGroupDTO>().ReverseMap();
            CreateMap(typeof(PageViewModel<BoxingGroupLiteViewModel>), typeof(PageModel<BoxingGroupLiteModel>)).ReverseMap();
        }
    }
}
