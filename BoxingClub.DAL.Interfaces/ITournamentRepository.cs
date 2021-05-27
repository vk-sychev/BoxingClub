using System.Collections.Generic;
using System.Threading.Tasks;
using BoxingClub.DAL.Entities;

namespace BoxingClub.DAL.Interfaces
{
    public interface ITournamentRepository:IRepository<Tournament>
    {
        Task<List<Tournament>> GetAcceptedTournamentsAsync();

        Task<Tournament> GetTournamentByIdWithStudentsAsync(int id);
    }
}
