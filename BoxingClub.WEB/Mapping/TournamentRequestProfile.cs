using AutoMapper;
using BoxingClub.Web.Models;
using HttpClients.Models;

namespace BoxingClub.Web.Mapping
{
    public class TournamentRequestProfile : Profile
    {
        public TournamentRequestProfile()
        {
            CreateMap<TournamentRequestModel, TournamentRequestViewModel>().ReverseMap();
        }
    }
}
