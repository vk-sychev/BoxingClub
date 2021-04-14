using BoxingClub.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoxingClub.BLL.Interfaces
{
    public interface ICoachService
    {
        Task DeleteCoachAsync(int? id);

        Task<UserDTO> GetCoachAsync(int? id);

        Task<List<UserDTO>> GetCoachesAsync();

        Task UpdateCoachAsync(UserDTO coachDTO);
    }
}