using AutoMapper;
using HttpClients.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tournaments.BLL.Entities;
using Tournaments.DAL.Entities;

namespace Tournaments.API.Mapping
{
    public class TournamentProfile : Profile
    {
        public TournamentProfile()
        {
            CreateMap<Tournament, TournamentDTO>(MemberList.Destination).ReverseMap();
            CreateMap<Tournament, TournamentModel>().ReverseMap();
        }
    }
}
