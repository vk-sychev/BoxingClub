using BoxingClub.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoxingClub.BLL.Interfaces
{
    public interface ICoachService
    {
        Task DeleteCoach(int? id);
        Task<CoachDTO> GetCoach(int? id);
        Task<List<CoachDTO>> GetCoaches();
        Task UpdateCoach(CoachDTO coachDTO);
    }
}