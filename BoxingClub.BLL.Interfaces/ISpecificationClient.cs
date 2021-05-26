using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BoxingClub.BLL.DomainEntities;

namespace BoxingClub.BLL.Interfaces
{
    public interface ISpecificationClient
    {
        public Task<List<TournamentSpecification>> GetTournamentSpecifications(int tournamentId);
    }
}
