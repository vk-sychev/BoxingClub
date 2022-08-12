using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tournaments.BLL.Entities.SpecificationModels;

namespace Tournaments.BLL.Interfaces.HttpSpecificationClient
{
    public interface ISpecificationHttpClient
    {
        public Task<TournamentSpecificationModel> GetTournamentSpecifications(int tournamentId);
    }
}
