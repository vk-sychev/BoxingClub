using AutoMapper;
using BoxingClub.BLL.DomainEntities;
using BoxingClub.BLL.Interfaces;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using BoxingClub.Infrastructure.Constants;
using BoxingClub.Infrastructure.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArgumentNullException = BoxingClub.Infrastructure.Exceptions.ArgumentNullException;
using InvalidOperationException = BoxingClub.Infrastructure.Exceptions.InvalidOperationException;

namespace BoxingClub.BLL.Implementation.Services
{
    public class UserService : IUserService
    {
        private readonly IRoleProvider _roleProvider;
        private readonly IUserProvider _userProvider;
        private readonly IAuthenticationProvider _authenticationProvider;
        private readonly IMapper _mapper;
        private const string DefaultRoleName = Constants.UserRoleName;

        public UserService(IUserProvider userProvider,
                           IRoleProvider roleProvider,
                           IAuthenticationProvider authenticationProvider,
                           IMapper mapper)
        {
            _userProvider = userProvider;
            _roleProvider = roleProvider;
            _authenticationProvider = authenticationProvider;
            _mapper = mapper;
        }

        private async Task<RoleDTO> FindUserRoleAsync(ApplicationUser user)
        {
            var role = await _roleProvider.GetUserRole(user);
            if (string.IsNullOrEmpty(role))
            {
                throw new NotFoundException($"Role for user = {user.UserName} isn't found", "");
            }

            var roleObject = await _roleProvider.FindRoleByNameAsync(role);
            if (roleObject == null)
            {
                throw new NotFoundException($"Role with name = {role} isn't found", "");
            }

            var mappedRole = _mapper.Map<RoleDTO>(roleObject);
            return mappedRole;
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

            var mappedUser = await AssignRole(user);
            return mappedUser;
        }


        private async Task<UserDTO> AssignRole(ApplicationUser user)
        {
            var mappedRole = await FindUserRoleAsync(user);
            var mappedUser = _mapper.Map<UserDTO>(user);
            mappedUser.Role = mappedRole;
            return mappedUser;
        }

        public async Task<List<UserDTO>> GetUsersAsync()
        {
            var users = await _userProvider.GetUsersAsync();
            var mappedUsers = new List<UserDTO>();
            foreach (var user in users)
            {
                var mappedUser = await AssignRole(user);
                mappedUsers.Add(mappedUser);
            }
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
                await _authenticationProvider.SignInAsync(mappedUser, isPersistent: false);
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

            ChangeUserProperties(userFromDb, user);
            var roleResult = await ChangeUserRole(userFromDb, user.Role.Id);

            if (roleResult.Succeeded)
            {
                var result = await _userProvider.UpdateUserAsync(userFromDb);
                var mappedResult = _mapper.Map<AccountResultDTO>(result);
                return mappedResult;
            }
            return roleResult;                  
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

        private void ChangeUserProperties(ApplicationUser userFromDb, UserDTO user)
        {
            userFromDb.Name = user.Name;
            userFromDb.Surname = user.Surname;
            userFromDb.Patronymic = user.Patronymic;
            userFromDb.UserName = user.UserName;
            userFromDb.Description = user.Description;
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

        public async Task<PageModelDTO<UserDTO>> GetUsersPaginatedAsync(int pageIndex, int pageSize)
        {
            var users = _userProvider.GetUsersPaginatedAsync(pageIndex, pageSize);
            var userDTOs = _mapper.Map<List<UserDTO>>(users);
            var count = await _userProvider.GetCountOfUsersAsync();
            var model = new PageModelDTO<UserDTO>() { Items = userDTOs, Count = count };
            return model;
        }
    }
}
