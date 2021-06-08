using BoxingClub.BLL.DomainEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoxingClub.BLL.Interfaces
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