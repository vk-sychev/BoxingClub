using BoxingClub.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoxingClub.BLL.Interfaces
{
    public interface IRoleService
    {
        Task<AccountResultDTO> CreateRoleAsync(RoleDTO role);

        Task<AccountResultDTO> DeleteRoleAsync(string id);

        Task<AccountResultDTO> EditRoleAsync(RoleDTO role);

        Task<RoleDTO> FindRoleByIdAsync(string id);

        Task<List<RoleDTO>> GetRolesAsync();
    }
}
