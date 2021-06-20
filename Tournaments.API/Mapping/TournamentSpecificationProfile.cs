using AutoMapper;
using Tournaments.BLL.Entities;
using Tournaments.BLL.Entities.SpecificationModels;

namespace Tournaments.API.Mapping
{
    public class TournamentSpecificationProfile : Profile
    {
        public TournamentSpecificationProfile()
        {
            CreateMap<TournamentSpecificationModel, TournamentSpecification>(MemberList.Destination);
            CreateMap<TournamentSpecification, HttpClients.Models.SpecModels.TournamentSpecification>().ReverseMap();
        }
    }
}
