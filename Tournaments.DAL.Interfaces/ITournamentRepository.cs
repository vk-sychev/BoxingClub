using System.Collections.Generic;
using System.Threading.Tasks;
using Tournaments.DAL.Entities;

namespace Tournaments.DAL.Interfaces
{
    public interface ITournamentRepository : IRepository<Tournament>
    {
        Task<List<Tournament>> GetAcceptedTournamentsAsync();

        Task<Tournament> GetTournamentByIdWithStudentsAsync(int id);
    }
}
