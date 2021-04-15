using AutoMapper;
using BoxingClub.BLL.DTO;
using BoxingClub.DAL.Entities;
using BoxingClub.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxingClub.Web.Mapping
{
    public class BoxingGroupProfile : Profile
    {
        public BoxingGroupProfile()
        {
            CreateMap<BoxingGroup, BoxingGroupDTO>().ReverseMap();
            CreateMap<BoxingGroupDTO, BoxingGroupFullViewModel>().ReverseMap();
            CreateMap<BoxingGroupLiteViewModel, BoxingGroupDTO>().ForMember(dest => dest.Students, opt => opt.Ignore())
                                                                 .ReverseMap();
        }
    }
}
