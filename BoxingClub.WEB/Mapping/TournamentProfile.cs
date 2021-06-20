using AutoMapper;
using BoxingClub.BLL.DomainEntities;
using BoxingClub.DAL.Entities;
using BoxingClub.Web.Models;
using HttpClients.Models;
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
            CreateMap<Tournament, TournamentDTO>(MemberList.Destination).ReverseMap();
            CreateMap<TournamentDTO, TournamentViewModel>().ReverseMap();
            CreateMap<Tournament, TournamentModel>().ReverseMap();
            CreateMap<TournamentModel, TournamentViewModel>().ReverseMap();
        }
    }
}
