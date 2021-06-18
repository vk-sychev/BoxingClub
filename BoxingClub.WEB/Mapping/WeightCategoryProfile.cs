using AutoMapper;
using BoxingClub.BLL.DomainEntities;
using BoxingClub.BLL.DomainEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HttpClients.Models.SpecModels;

namespace BoxingClub.Web.Mapping
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
