using AutoMapper;
using BoxingClub.Infrastructure.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer.BLL.Entities;
using IdentityServer.BLL.Interfaces;
using IdentityServer.DAL.Entities;
using IdentityServer.DAL.Interfaces;
using ArgumentNullException = BoxingClub.Infrastructure.Exceptions.ArgumentNullException;

namespace IdentityServer.BLL.Implementation
{
    public class RoleService : IRoleService
    {
        private readonly IRoleProvider _roleProvider;
        private readonly IMapper _mapper;

        public RoleService(IRoleProvider roleProvider,
                           IMapper mapper)
        {
            _roleProvider = roleProvider ?? throw new ArgumentNullException(nameof(roleProvider), "roleProvider is null");
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), "mapper is null");
        }

        public async Task<RoleDTO> FindRoleByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id), "Role's id is null");
            }
            var role = await _roleProvider.FindRoleByIdAsync(id);
            if (role == null)
            {
                throw new NotFoundException($"Role with id = {id} isn't found", "");
            }
            var mappedRole = _mapper.Map<RoleDTO>(role);
            return mappedRole;
        }

        public async Task<List<RoleDTO>> GetRolesAsync()
        {
            var roles = await _roleProvider.GetRolesAsync();
            var mappedRoles = _mapper.Map<List<RoleDTO>>(roles);
            return mappedRoles;
        }

        public async Task<bool> IsInRoleAsync(UserDTO user, string roleName)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User is null");
            }

            if (string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentNullException(nameof(roleName), "Role is null");
            }

            var mappedUser = _mapper.Map<ApplicationUser>(user);
            var result = await _roleProvider.IsInRoleAsync(mappedUser, roleName);
            return result;
        }

        public async Task<AccountResultDTO> RemoveFromRoleAsync(string userId, string roleName)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(nameof(userId), "User's id is null");
            }

            if (string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentNullException(nameof(roleName), "Role is null");
            }

            var result = await _roleProvider.RemoveFromRoleAsync(userId, roleName);
            var mappedResult = _mapper.Map<AccountResultDTO>(result);
            return mappedResult;
        }

        public async Task<AccountResultDTO> AddToRoleAsync(string userId, string roleName)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(nameof(userId), "User's id is null");
            }

            if (string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentNullException(nameof(roleName), "Role is null");
            }

            var result = await _roleProvider.AddToRoleAsync(userId, roleName);
            var mappedResult = _mapper.Map<AccountResultDTO>(result);
            return mappedResult;
        }
    }
}
