using BoxingClub.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoxingClub.BLL.Interfaces
{
    public interface ICoachService
    {
        Task DeleteCoachAsync(int? id);

        Task<CoachDTO> GetCoachAsync(int? id);

        Task<List<CoachDTO>> GetCoachesAsync();

        Task UpdateCoachAsync(CoachDTO coachDTO);
    }
}