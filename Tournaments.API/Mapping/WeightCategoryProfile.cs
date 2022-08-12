using AutoMapper;
using HttpClients.Models.SpecModels;
using Tournaments.BLL.Entities;
using Tournaments.BLL.Entities.SpecificationModels;

namespace Tournaments.API.Mapping
{
    public class WeightCategoryProfile : Profile
    {
        public WeightCategoryProfile()
        {
            CreateMap<WeightCategoryModel, WeightCategoryDTO>(MemberList.Destination).ForMember(dest => dest.StartWeight, opt => opt.MapFrom(src => src.MinValue))
                                                                                     .ForMember(dest => dest.EndWeight, opt => opt.MapFrom(src => src.MaxValue));
            CreateMap<WeightCategoryDTO, WeightCategory>().ReverseMap();
        }
    }
}
