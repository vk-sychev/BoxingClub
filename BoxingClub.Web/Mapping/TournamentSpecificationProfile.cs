using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BoxingClub.BLL.DomainEntities;
using BoxingClub.BLL.Implementation.HttpSpecificationClient.Models;

namespace BoxingClub.Web.Mapping
{
    public class TournamentSpecificationProfile : Profile
    {
        public TournamentSpecificationProfile()
        {
            CreateMap<TournamentSpecification, SpecificationModelFromServer>();
        }
    }
}
