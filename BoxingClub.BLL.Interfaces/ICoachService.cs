using BoxingClub.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoxingClub.BLL.Interfaces
{
    public interface ICoachService
    {
        Task DeleteCoachAsync(int? id);

        Task<UserDTO> GetCoachByIdAsync(int? id);

        Task<UserDTO> GetCoachByNameAsync(string? name);

        Task<List<UserDTO>> GetCoachesAsync();

        Task UpdateCoachAsync(UserDTO coachDTO);
    }
}