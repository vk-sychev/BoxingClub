using AutoMapper;
using BoxingClub.Web.Models;
using HttpClients.Models;

namespace BoxingClub.Web.Mapping
{
    public class TournamentProfile : Profile
    {
        public TournamentProfile()
        {
            CreateMap<TournamentModel, TournamentViewModel>().ReverseMap();
        }
    }
}
