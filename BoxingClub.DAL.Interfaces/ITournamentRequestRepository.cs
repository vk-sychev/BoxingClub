using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BoxingClub.DAL.Entities;

namespace BoxingClub.DAL.Interfaces
{
    public interface ITournamentRequestRepository : IRepository<TournamentRequest>
    {
        Task CreateTournamentRequestRangeAsync(List<TournamentRequest> requests);

        Task<List<TournamentRequest>> GetTournamentRequestsByStudentIds(List<int> ids);
    }
}
