using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer.BLL.Entities;

namespace IdentityServer.BLL.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> FindUserByIdAsync(string userId);

        Task<UserDTO> FindUserByNameAsync(string name);

        Task DeleteUserByIdAsync(string userId);

        Task<List<UserDTO>> GetUsersAsync();

        Task<PageModelDTO<UserDTO>> GetUsersPaginatedAsync(SearchModelDTO searchDTO);

        Task<List<UserDTO>> GetUsersByRoleAsync(string roleName);

        Task<AccountResultDTO> SignUpAsync(UserDTO user, string password);

        Task<AccountResultDTO> UpdateUserAsync(UserDTO user);

        Task<List<Claim>> GetUserClaims(string userId);

        bool IsSupportUserRole();
    }
}
