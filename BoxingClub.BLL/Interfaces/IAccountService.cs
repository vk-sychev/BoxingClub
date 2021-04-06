using BoxingClub.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoxingClub.BLL.Interfaces
{
    public interface IAccountService
    {
        Task<AccountResultDTO> AddToRole(UserDTO user, string roleName);

        Task<AccountResultDTO> CreateRole(RoleDTO role);

        Task<AccountResultDTO> Delete(string id);

        Task<AccountResultDTO> EditRole(RoleDTO role);

        Task<RoleDTO> FindRoleById(string id);

        Task<UserDTO> FindUserById(string id);

        List<RoleDTO> GetRoles();

        List<UserDTO> GetUsers();

        Task<bool> IsInRole(UserDTO user, string roleName);

        Task<AccountResultDTO> RemoveFromRole(UserDTO user, string roleName);

        Task<SignInResultDTO> SignIn(UserDTO user);

        Task SignOut();

        Task<AccountResultDTO> SignUp(UserDTO user, string password);
    }
}