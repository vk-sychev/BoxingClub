using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BoxingClub.BLL.DomainEntities;
using BoxingClub.Web.Models;
using HttpClients.Models;

namespace BoxingClub.Web.Mapping
{
    public class SearchModelProfile : Profile
    {
        public SearchModelProfile()
        {
            CreateMap<SearchModelDTO, SearchModel>().ReverseMap();
        }
    }
}
