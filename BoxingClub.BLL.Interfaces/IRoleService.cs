using BoxingClub.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoxingClub.BLL.Interfaces
{
    public interface IRoleService
    {
        Task<RoleDTO> FindRoleByIdAsync(string id);

        Task<bool> IsInRoleAsync(UserDTO user, string roleName);

        Task<AccountResultDTO> RemoveFromRoleAsync(string userId, string roleName);

        Task<AccountResultDTO> AddToRoleAsync(string userId, string roleName);

        Task<List<RoleDTO>> GetRolesAsync();
    }
}
