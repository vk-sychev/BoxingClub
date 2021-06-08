using AutoMapper;
using BoxingClub.BLL.DomainEntities;
using BoxingClub.BLL.DomainEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxingClub.Web.Mapping
{
    public class AgeGroupProfile : Profile
    {
        public AgeGroupProfile()
        {
            CreateMap<AgeGroupModel, AgeGroupDTO>(MemberList.Destination).ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Sex));
        }
    }
}
