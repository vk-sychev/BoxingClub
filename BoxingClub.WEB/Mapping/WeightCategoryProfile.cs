using AutoMapper;
using BoxingClub.BLL.DomainEntities;
using BoxingClub.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxingClub.Web.Mapping
{
    public class WeightCategoryProfile : Profile
    {
        public WeightCategoryProfile()
        {
            CreateMap<WeightCategory, WeightCategoryDTO>().ReverseMap();
        }
    }
}
