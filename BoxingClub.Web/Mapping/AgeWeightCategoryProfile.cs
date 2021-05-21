using AutoMapper;
using BoxingClub.BLL.DomainEntities;
using BoxingClub.DAL.Entities;
using BoxingClub.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxingClub.Web.Mapping
{
    public class AgeWeightCategoryProfile : Profile
    {
        public AgeWeightCategoryProfile()
        {
            CreateMap<AgeWeightCategory, AgeWeightCategoryDTO>().ReverseMap();
            CreateMap<AgeWeightCategoryDTO, AgeWeightCategoryViewModel>().ReverseMap();
        }
    }
}
