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
    public class TournamentProfile : Profile
    {
        public TournamentProfile()
        {
            CreateMap<Tournament, TournamentDTO>(MemberList.Source).ForSourceMember(src => src.Categories, opt => opt.DoNotValidate())
                                                                   .ReverseMap()
                                                                   .ForMember(dest => dest.Categories, opt => opt.Ignore());

            CreateMap<TournamentDTO, TournamentViewModel>().ReverseMap();
        }
    }
}
