using AutoMapper;
using BoxingClub.Web.Models;
using HttpClients.Models;

namespace BoxingClub.Web.Mapping
{
    public class BoxingGroupProfile : Profile
    {
        public BoxingGroupProfile()
        {
            CreateMap<BoxingGroupLiteModel, BoxingGroupLiteViewModel>().ReverseMap();
            CreateMap<BoxingGroupFullModel, BoxingGroupFullViewModel>().ReverseMap();
            CreateMap(typeof(PageViewModel<BoxingGroupLiteViewModel>), typeof(PageModel<BoxingGroupLiteModel>)).ReverseMap();
        }
    }
}
