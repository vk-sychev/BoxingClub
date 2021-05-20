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
            CreateMap<Tournament, TournamentLiteDTO>(MemberList.Destination).ReverseMap();
            CreateMap<Tournament, TournamentFullDTO>(MemberList.Source).ForSourceMember(src => src.TournamentRequirements, opt => opt.DoNotValidate())
                                                                       .ReverseMap()
                                                                       .ForMember(src => src.TournamentRequirements, dest => dest.Ignore());
            CreateMap<TournamentLiteDTO, TournamentLiteViewModel>().ReverseMap();
            CreateMap<TournamentFullDTO, TournamentFullViewModel>().ReverseMap();
        }
    }
}
