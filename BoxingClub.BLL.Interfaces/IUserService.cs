using BoxingClub.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoxingClub.BLL.Interfaces
{
    public interface IUserService
    {
        Task<AccountResultDTO> AddToRoleAsync(string userId, string roleName);

        Task<UserDTO> FindUserByIdAsync(string userId);

        Task<UserDTO> FindUserByNameAsync(string name);

        Task DeleteUserByIdAsync(string userId);

        Task<List<UserDTO>> GetUsersAsync();

        Task<List<UserDTO>> GetUsersByRoleAsync(string roleName);

        Task<bool> IsInRoleAsync(UserDTO user, string roleName);

        Task<AccountResultDTO> RemoveFromRoleAsync(string userId, string roleName);

        Task<AccountResultDTO> SignUpAsync(UserDTO user, string password);
    }
}
