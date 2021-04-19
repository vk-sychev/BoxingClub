using AutoMapper;
using BoxingClub.BLL.DTO;
using BoxingClub.BLL.Interfaces;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
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
        private readonly IMapper _mapper;
        private const string DefaultRoleName = "User";

        public UserService(IUserProvider userProvider,
                           IRoleProvider roleProvider,
                           IMapper mapper)
        {
            _userProvider = userProvider;
            _mapper = mapper;
            _roleProvider = roleProvider;
        }

        public async Task<UserDTO> FindUserByIdAsync(string userId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId), "User's id is null");
            }
            var user = await _userProvider.FindUserByIdAsync(userId);
            if (user == null)
            {
                throw new NotFoundException($"User with id = {userId} isn't found", "");
            }
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<List<UserDTO>> GetUsersAsync()//вопрос
        {
            var users = await _userProvider.GetUsersAsync();
            var mappedUsers = new List<UserDTO>();
            foreach (var user in users)
            {
                var role = await _userProvider.GetUserRole(user);
                if (role == null)
                {
                    throw new NotFoundException($"Role with name = {role} isn't found ", "");
                }

                var roleObject = await _roleProvider.FindRoleByNameAsync(role);
                if (roleObject == null)
                {
                    throw new NotFoundException($"Role with name = {role} isn't found", "");
                }

                var mappedUser = _mapper.Map<UserDTO>(user);
                var mappedRole = _mapper.Map<RoleDTO>(roleObject);
                mappedUser.Role = mappedRole;
                mappedUsers.Add(mappedUser);
            }
            return mappedUsers;
        }

        public async Task<bool> IsInRoleAsync(UserDTO user, string roleName)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User is null");
            }

            var result = await _userProvider.IsInRoleAsync(_mapper.Map<ApplicationUser>(user), roleName);
            return result;
        }

        public async Task<AccountResultDTO> RemoveFromRoleAsync(string userId, string roleName)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId), "User's id is null");
            }
            var result = await _userProvider.RemoveFromRoleAsync(userId, roleName);
            return _mapper.Map<AccountResultDTO>(result);
        }

        public async Task<AccountResultDTO> AddToRoleAsync(string userId, string roleName)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId), "User's id is null");
            }
            var result = await _userProvider.AddToRoleAsync(userId, roleName);
            return _mapper.Map<AccountResultDTO>(result);
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
            var result = await _userProvider.SignUpAsync(_mapper.Map<ApplicationUser>(user), password, DefaultRoleName);
            return _mapper.Map<AccountResultDTO>(result);
        }

        public async Task DeleteUserByIdAsync(string userId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId), "User's id is null");
            }
            if (! await _userProvider.DeleteUserAsync(userId))
            {
                throw new NotFoundException($"User with id = {userId} isn't found", "");
            }
        }

        public async Task<UserDTO> FindUserByNameAsync(string name)
        {
            if (name == null)
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
            var users = await _userProvider.GetUsersByRoleAsync(roleName);
            var mappedUsers = _mapper.Map<List<UserDTO>>(users);
            return mappedUsers;
        }
    }
}
