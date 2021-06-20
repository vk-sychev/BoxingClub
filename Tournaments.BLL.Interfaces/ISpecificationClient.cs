using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tournaments.BLL.Entities;

namespace Tournaments.BLL.Interfaces
{
    public interface ISpecificationClient
    {
        public Task<TournamentSpecification> GetTournamentSpecifications(int tournamentId);
    }
}
