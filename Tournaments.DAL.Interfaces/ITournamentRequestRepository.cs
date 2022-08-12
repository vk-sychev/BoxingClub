using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tournaments.DAL.Entities;

namespace Tournaments.DAL.Interfaces
{
    public interface ITournamentRequestRepository : IRepository<TournamentRequest>
    {
        Task CreateTournamentRequestRangeAsync(List<TournamentRequest> requests);

        Task<List<TournamentRequest>> GetTournamentRequestsByStudentIds(List<int> ids);

        Task<List<TournamentRequest>> GetTournamentRequestsByTournamentId(int id);

        void DeleteTournamentRequestsRange(List<TournamentRequest> tournamentRequests);
    }
}
