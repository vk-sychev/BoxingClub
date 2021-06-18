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
