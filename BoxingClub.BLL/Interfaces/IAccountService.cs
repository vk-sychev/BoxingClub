using BoxingClub.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoxingClub.BLL.Interfaces
{
    public interface IAccountService
    {
        Task<AccountResultDTO> AddToRoleAsync(UserDTO user, string roleName);

        Task<AccountResultDTO> CreateRoleAsync(RoleDTO role);

        Task<AccountResultDTO> DeleteAsync(string id);

        Task<AccountResultDTO> EditRoleAsync(RoleDTO role);

        Task<RoleDTO> FindRoleByIdAsync(string id);

        Task<UserDTO> FindUserByIdAsync(string id);

        Task<List<RoleDTO>> GetRolesAsync();

        Task<List<UserDTO>> GetUsersAsync();

        Task<bool> IsInRoleAsync(UserDTO user, string roleName);

        Task<AccountResultDTO> RemoveFromRoleAsync(UserDTO user, string roleName);

        Task<SignInResultDTO> SignInAsync(UserDTO user);

        Task SignOutAsync();

        Task<AccountResultDTO> SignUpAsync(UserDTO user, string password);
    }
}