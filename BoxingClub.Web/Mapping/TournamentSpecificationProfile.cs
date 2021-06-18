using AutoMapper;
using BoxingClub.BLL.DomainEntities;
using BoxingClub.BLL.DomainEntities.Models;

namespace BoxingClub.Web.Mapping
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
