using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BoxingClub.BLL.DomainEntities.Models;

namespace BoxingClub.BLL.Interfaces.HttpSpecificationClient
{
    public interface ISpecificationHttpClient
    {
        public Task<TournamentSpecificationModel> GetTournamentSpecifications(int tournamentId);
    }
}
