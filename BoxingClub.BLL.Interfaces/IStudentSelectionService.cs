using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BoxingClub.BLL.DomainEntities;

namespace BoxingClub.BLL.Interfaces
{
    public interface IStudentSelectionService
    {
        Task<List<StudentFullDTO>> GetStudentsByTournamentId(string token, int tournamentId);

        Task CreateTournamentRequest(int tournamentId, List<StudentFullDTO> students);

        Task UpdateTournamentRequest(int tournamentId, List<StudentFullDTO> students);

        Task DeleteTournamentRequest(int tournamentId);

        Task<List<StudentFullDTO>> GetSelectedStudentsByTournamentId(string token, int tournamentId);
    }
}
