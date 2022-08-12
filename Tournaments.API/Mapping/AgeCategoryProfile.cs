using AutoMapper;
using HttpClients.Models.SpecModels;
using Tournaments.BLL.Entities;
using Tournaments.BLL.Entities.SpecificationModels;

namespace Tournaments.API.Mapping
{
    public class AgeCategoryProfile : Profile
    {
        public AgeCategoryProfile()
        {
            CreateMap<AgeCategoryModel, AgeCategoryDTO>(MemberList.Destination).ForMember(dest => dest.StartAge, opt => opt.MapFrom(src => src.MinAge))
                                                                               .ForMember(dest => dest.EndAge, opt => opt.MapFrom(src => src.MaxAge));
            CreateMap<AgeCategoryDTO, AgeCategory>().ReverseMap();
        }
    }
}
