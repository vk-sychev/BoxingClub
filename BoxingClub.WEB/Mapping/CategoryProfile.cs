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
    public class CategoryProfile:Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<CategoryDTO, CategoryViewModel>(MemberList.Source).ReverseMap();
        }
    }
}
