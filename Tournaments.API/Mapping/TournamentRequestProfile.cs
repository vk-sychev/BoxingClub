using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tournaments.BLL.Entities;
using Tournaments.DAL.Entities;

namespace Tournaments.API.Mapping
{
    public class TournamentRequestProfile : Profile
    {
        public TournamentRequestProfile()
        {
            CreateMap<TournamentRequestDTO, TournamentRequest>().ReverseMap();
        }
    }
}
