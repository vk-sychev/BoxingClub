using BoxingClub.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoxingClub.BLL.Interfaces
{
    public interface IUserService
    {
        Task<AccountResultDTO> AddToRoleAsync(UserDTO user, string roleName);

        Task<UserDTO> FindUserByIdAsync(string id);

        Task<UserDTO> FindUserByNameAsync(string name);

        Task DeleteUserAsync(string id);

        Task<List<UserDTO>> GetUsersAsync();

        Task<List<UserDTO>> GetUsersByRoleAsync(string roleName);

        Task<bool> IsInRoleAsync(UserDTO user, string roleName);

        Task<AccountResultDTO> RemoveFromRoleAsync(UserDTO user, string roleName);

        Task<AccountResultDTO> SignUpAsync(UserDTO user, string password);
    }
}
