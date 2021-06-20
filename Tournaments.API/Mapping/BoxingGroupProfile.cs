using AutoMapper;
using HttpClients.Models;

namespace Tournaments.API.Mapping
{
    public class BoxingGroupProfile : Profile
    {
        public BoxingGroupProfile()
        {
/*            CreateMap<BoxingGroupDTO, BoxingGroupFullViewModel>().ReverseMap();
            CreateMap<BoxingGroupLiteViewModel, BoxingGroupDTO>().ForMember(dest => dest.Students, opt => opt.Ignore())
                                                                 .ReverseMap();
            CreateMap<BoxingGroupLiteModel, BoxingGroupLiteViewModel>().ReverseMap();
            CreateMap<BoxingGroupFullModel, BoxingGroupFullViewModel>().ReverseMap();
            CreateMap<BoxingGroupLiteModel, BoxingGroupDTO>().ReverseMap();
            CreateMap(typeof(PageViewModel<BoxingGroupLiteViewModel>), typeof(PageModel<BoxingGroupLiteModel>)).ReverseMap();*/
        }
    }
}
