using BoxingClub.BLL.DomainEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoxingClub.BLL.Interfaces
{
    public interface ITournamentService
    {
        Task CreateTournamentAsync(TournamentFullDTO tournamentDTO);

        Task DeleteTournamentAsync(int? id);

        Task<TournamentFullDTO> GetTournamentByIdAsync(int? id);

        Task<List<TournamentLiteDTO>> GetTournamentsAsync();

        Task UpdateTournamentAsync(TournamentFullDTO tournamentDTO);

        Task<List<CategoryDTO>> GetCategories();
    }
}