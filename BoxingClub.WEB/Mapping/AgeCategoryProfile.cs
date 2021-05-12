using AutoMapper;
using BoxingClub.BLL.DomainEntities;
using BoxingClub.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxingClub.Web.Mapping
{
    public class AgeCategoryProfile : Profile
    {
        public AgeCategoryProfile()
        {
            CreateMap<AgeCategory, AgeCategoryDTO>().ReverseMap();
        }
    }
}
