using AutoMapper;
using BoxingClub.BLL.DTO;
using BoxingClub.BLL.Interfaces;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using BoxingClub.Infrastructure.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArgumentNullException = BoxingClub.Infrastructure.Exceptions.ArgumentNullException;

namespace BoxingClub.BLL.Implementation.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleProvider _roleProvider;
        private readonly IMapper _mapper;

        public RoleService(IRoleProvider roleProvider,
                           IMapper mapper)
        {
            _roleProvider = roleProvider;
            _mapper = mapper;
        }

        public async Task<RoleDTO> FindRoleByIdAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(nameof(userId), "Role's id is null");
            }
            var role = await _roleProvider.FindRoleByIdAsync(userId);
            if (role == null)
            {
                throw new NotFoundException($"Role with id = {userId} isn't found", "");
            }
            return _mapper.Map<RoleDTO>(role);
        }

        public async Task<List<RoleDTO>> GetRolesAsync()
        {
            var roles = await _roleProvider.GetRolesAsync();
            return _mapper.Map<List<RoleDTO>>(roles);
        }

        public Task<bool> IsInRoleAsync(UserDTO user, string roleName)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User is null");
            }

            if (string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentNullException(nameof(roleName), "Role is null");
            }

            var result = _roleProvider.IsInRoleAsync(_mapper.Map<ApplicationUser>(user), roleName);
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
            return _mapper.Map<AccountResultDTO>(result);
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
            return _mapper.Map<AccountResultDTO>(result);
        }
    }
}
