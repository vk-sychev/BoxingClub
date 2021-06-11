using AutoMapper;
using BoxingClub.Infrastructure.Constants;
using BoxingClub.Infrastructure.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer.BLL.Entities;
using IdentityServer.BLL.Interfaces;
using IdentityServer.DAL.Entities;
using IdentityServer.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using ArgumentNullException = BoxingClub.Infrastructure.Exceptions.ArgumentNullException;
using InvalidOperationException = BoxingClub.Infrastructure.Exceptions.InvalidOperationException;

namespace IdentityServer.BLL.Implementation
{
    public class UserService : IUserService
    {
        private readonly IRoleProvider _roleProvider;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
        private readonly IUserProvider _userProvider;
        private readonly IMapper _mapper;
        private const string DefaultRoleName = Constants.UserRoleName;

        public UserService(IUserProvider userProvider,
                           IRoleProvider roleProvider,
                           IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
                           IMapper mapper)
        {
            _userProvider = userProvider ?? throw new ArgumentNullException(nameof(userProvider), "userProvider is null");
            _roleProvider = roleProvider ?? throw new ArgumentNullException(nameof(roleProvider), "roleProvider is null");
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), "mapper is null");
        }

        public async Task<UserDTO> FindUserByIdAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(nameof(userId), "User's id is null");
            }
            var user = await _userProvider.FindUserByIdAsync(userId);
            if (user == null)
            {
                throw new NotFoundException($"User with id = {userId} isn't found", "");
            }
            var mappedUser = _mapper.Map<UserDTO>(user);
            return mappedUser;
        }

        public async Task<List<UserDTO>> GetUsersAsync()
        {
            var users = await _userProvider.GetUsersAsync();
            var mappedUsers = _mapper.Map<List<UserDTO>>(users);
            return mappedUsers;
        }

        public async Task<AccountResultDTO> SignUpAsync(UserDTO user, string password)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User is null");
            }
            var defaultRole = await _roleProvider.FindRoleByNameAsync(DefaultRoleName);
            if (defaultRole == null)
            {
                throw new InvalidOperationException($"Role with name {DefaultRoleName} doesn't exist");
            }

            var mappedUser = _mapper.Map<ApplicationUser>(user);
            var result = await _userProvider.CreateUserAsync(mappedUser, password);

            if (result.Succeeded)
            {
                await _roleProvider.AddToRoleAsync(mappedUser.Id, DefaultRoleName);
                //await _authenticationProvider.SignInAsync(mappedUser, isPersistent: false);
            }

            var mappedResult = _mapper.Map<AccountResultDTO>(result);
            return mappedResult;
        }

        public async Task DeleteUserByIdAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(nameof(userId), "User's id is null");
            }

            var user = await _userProvider.FindUserByIdAsync(userId);
            if (user == null)
            {
                throw new NotFoundException($"User with id = {userId} isn't found", "");
            }

            var res = await _userProvider.DeleteUserAsync(user);

            if (!res.Succeeded)
            {
                throw new InvalidOperationException($"Cannot delete user with id = {userId}");
            }
        }

        public async Task<UserDTO> FindUserByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name), "User's name is null");
            }
            var user = await _userProvider.GetUserByNameAsync(name);
            if (user == null)
            {
                throw new NotFoundException($"User with name = {name} isn't found", "");
            }
            var mappedUser = _mapper.Map<UserDTO>(user);
            return mappedUser;
        }

        public async Task<List<UserDTO>> GetUsersByRoleAsync(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentNullException(nameof(roleName), "Role's name is null");
            }
            var users = await _userProvider.GetUsersByRoleAsync(roleName);
            var mappedUsers = _mapper.Map<List<UserDTO>>(users);
            return mappedUsers;
        }

        public async Task<AccountResultDTO> UpdateUserAsync(UserDTO user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User is null");
            }

            var userFromDb = await _userProvider.FindUserByIdAsync(user.Id);

            if (userFromDb == null)
            {
                throw new NotFoundException($"User with id = {user.Id} isn't found", "");
            }

            var mappedUser = _mapper.Map<ApplicationUser>(user);

            userFromDb.ChangeUserProperties(mappedUser);
            var roleResult = await ChangeUserRole(userFromDb, user.Role.Id);

            if (roleResult.Succeeded)
            {
                var result = await _userProvider.UpdateUserAsync(userFromDb);
                var mappedResult = _mapper.Map<AccountResultDTO>(result);
                return mappedResult;
            }
            return roleResult;                  
        }

        public async Task<PageModelDTO<UserDTO>> GetUsersPaginatedAsync(SearchModelDTO searchDTO)
        {
            if (searchDTO == null)
            {
                throw new ArgumentNullException(nameof(searchDTO), "SearchDTO is null");
            }

            if (searchDTO.PageIndex == null)
            {
                searchDTO.PageIndex = PageModelConstants.PageIndex;
            }

            if (searchDTO.PageSize == null)
            {
                searchDTO.PageSize = PageModelConstants.PageSize;
            }

            var users = await _userProvider.GetUsersPaginatedAsync(searchDTO.PageIndex.Value, searchDTO.PageSize.Value);
            if (users.Count == 0)
            {
                searchDTO.PageIndex = PageModelConstants.PageIndex;
                users = await _userProvider.GetUsersPaginatedAsync(searchDTO.PageIndex.Value, searchDTO.PageSize.Value);
            }
            var userDTOs = _mapper.Map<List<UserDTO>>(users);

            var count = await _userProvider.GetCountOfUsersAsync();
            var model = new PageModelDTO<UserDTO>() { Items = userDTOs, Count = count};
            return model;
        }

        public async Task<List<Claim>> GetUserClaims(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(nameof(userId), "User's id is null");
            }

            var user = await _userProvider.FindUserByIdAsync(userId);
            if (user == null)
            {
                throw new NotFoundException($"User with id = {userId} isn't found", "");
            }

            var userClaims = await _userClaimsPrincipalFactory.CreateAsync(user);

            return userClaims.Claims.ToList();
        }

        public bool IsSupportUserRole()
        {
            return _userProvider.IsSupportUserRole();
        }

        private async Task<AccountResultDTO> UpdateRoleIfChanged(string userId, string oldRoleName, string newRoleName)
        {
            var result = await _roleProvider.RemoveFromRoleAsync(userId, oldRoleName);
            if (result.Succeeded)
            {
                result = await _roleProvider.AddToRoleAsync(userId, newRoleName);
            }
            var mappedResult = _mapper.Map<AccountResultDTO>(result);
            return mappedResult;
        }


        private async Task<string> GetUserRole(ApplicationUser userFromDb)
        {
            var role = await _roleProvider.GetUserRole(userFromDb);
            if (string.IsNullOrEmpty(role))
            {
                throw new NotFoundException($"Role for user = {userFromDb.UserName} isn't found", "");
            }
            return role;
        }

        private async Task<AccountResultDTO> ChangeUserRole(ApplicationUser userFromDb, string newRoleId)
        {
            var oldRoleName = await GetUserRole(userFromDb);
            var userId = userFromDb.Id;

            if (string.IsNullOrEmpty(newRoleId))
            {
                throw new ArgumentNullException(nameof(newRoleId), "newRoleId is null");
            }

            var newRole = await _roleProvider.FindRoleByIdAsync(newRoleId);
            if (newRole == null)
            {
                throw new NotFoundException($"Role for user = {userId} isn't found", "");
            }

            var newRoleName = newRole.Name;
            if (!oldRoleName.Equals(newRoleName))
            {
                var result = await UpdateRoleIfChanged(userId, oldRoleName, newRoleName);
                return result;
            }
            return new AccountResultDTO { Succeeded = true };
        }
    }
}
