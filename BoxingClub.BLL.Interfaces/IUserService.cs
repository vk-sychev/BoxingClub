using BoxingClub.BLL.DomainEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoxingClub.BLL.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> FindUserByIdAsync(string userId);

        Task<UserDTO> FindUserByNameAsync(string name);

        Task DeleteUserByIdAsync(string userId);

        Task<List<UserDTO>> GetUsersAsync();

        Task<PageModelDTO<UserDTO>> GetUsersPaginatedAsync(int pageIndex, int pageSize);

        Task<List<UserDTO>> GetUsersByRoleAsync(string roleName);

        Task<AccountResultDTO> SignUpAsync(UserDTO user, string password);

        Task<AccountResultDTO> UpdateUserAsync(UserDTO user);
    }
}
