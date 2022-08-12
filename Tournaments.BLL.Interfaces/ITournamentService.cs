using System.Collections.Generic;
using System.Threading.Tasks;
using Tournaments.BLL.Entities;

namespace Tournaments.BLL.Interfaces
{
    public interface ITournamentService
    {
        Task CreateTournamentAsync(TournamentDTO tournamentDTO);

        Task DeleteTournamentAsync(int id);

        Task<TournamentDTO> GetTournamentByIdAsync(int id);

        Task<List<TournamentDTO>> GetTournamentsAsync();

        Task<List<TournamentDTO>> GetAcceptedTournamentsAsync();

        Task UpdateTournamentAsync(TournamentDTO tournamentDTO);
    }
}