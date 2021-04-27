using AutoMapper;
using BoxingClub.BLL.DTO;
using BoxingClub.BLL.Implementation.Services;
using BoxingClub.BLL.Interfaces;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using BoxingClub.Infrastructure.Exceptions;
using BoxingClub.Web.Mapping;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArgumentNullException = BoxingClub.Infrastructure.Exceptions.ArgumentNullException;
using InvalidOperationException = BoxingClub.Infrastructure.Exceptions.InvalidOperationException;

namespace BoxingClub.BLL.UnitTests
{
    [TestFixture]
    class UserServiceTests
    {
        private IMapper _mapper;
        private Mock<IUserProvider> _mockUserProvider;
        private Mock<IAuthenticationProvider> _mockAuthProvider;
        private Mock<IRoleProvider> _mockRoleProvider;
        private IUserService _userService;
        private static readonly List<ApplicationUser> usersList = new List<ApplicationUser>()
        {
            new ApplicationUser { Id = "test1" },
            new ApplicationUser { Id = "test2" },
            new ApplicationUser { Id = "test3" }
        };
        private static readonly ApplicationUser user = new ApplicationUser() { Id = "testId", UserName = "Test" };
        private static readonly IdentityRole role = new IdentityRole() { Id = "testId", Name = "Test" };
        private static readonly string roleName = role.Name;
        private static readonly string roleId = role.Id;
        private static readonly string userId = user.Id;
        private static readonly string userName = user.UserName;
        private static readonly UserDTO userDTO = new UserDTO { Id = "test", Name = "Test" };
        private static readonly string password = "test";


        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var mapperProfiles = new List<Profile>() { new UserProfile(), new ResultProfile(), new RoleProfile() };
            var mapperConfig = new MapperConfiguration(mc => mc.AddProfiles(mapperProfiles));
            _mapper = mapperConfig.CreateMapper();
        }

        [SetUp]
        public void SetUp()
        {
            _mockUserProvider = new Mock<IUserProvider>();
            _mockAuthProvider = new Mock<IAuthenticationProvider>();
            _mockRoleProvider = new Mock<IRoleProvider>();
            _userService = new UserService(_mockUserProvider.Object, _mockRoleProvider.Object, _mockAuthProvider.Object, _mapper);
        }

        [Test]
        public async Task FindUserByIdAsync_ValidInput()
        {
            _mockUserProvider.Setup(p => p.FindUserByIdAsync(It.IsAny<string>()).Result).Returns(user);
            _mockRoleProvider.Setup(p => p.GetUserRole(It.IsAny<ApplicationUser>()).Result).Returns(role.Name);
            _mockRoleProvider.Setup(p => p.FindRoleByNameAsync(It.IsAny<string>()).Result).Returns(role);

            var userFromService = await _userService.FindUserByIdAsync(user.Id);

            _mockUserProvider.Verify(p => p.FindUserByIdAsync(It.IsAny<string>()), Times.Once);
            _mockRoleProvider.Verify(p => p.GetUserRole(It.IsAny<ApplicationUser>()), Times.Once);
            _mockRoleProvider.Verify(p => p.FindRoleByNameAsync(It.IsAny<string>()), Times.Once);
            Assert.IsNotNull(userFromService);
            Assert.AreEqual(user.Id, userFromService.Id);
        }

        [Test]
        public void FindUserByIdAsync_InputIsNull_ShouldThrowArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _userService.FindUserByIdAsync(null));
        }


        [Test]
        public void FindUserByIdAsync_InvalidInput_ShouldThrowNotFoundException()
        {
            var userId = "test";
            var exceptionMessage = $"User with id = {userId} isn't found";

            _mockUserProvider.Setup(p => p.FindUserByIdAsync(It.IsAny<string>()).Result);

            var exception = Assert.ThrowsAsync<NotFoundException>(async () => await _userService.FindUserByIdAsync(userId));
            _mockUserProvider.Verify(p => p.FindUserByIdAsync(It.IsAny<string>()), Times.Once);
            Assert.AreEqual(exceptionMessage, exception.Message);
        }

        [Test]
        public void FindUserByIdAsync_ValidInput_RoleNameIsNull_ShouldThrowNotFoundException()
        {
            var exceptionMessage = $"Role for user = {user.UserName} isn't found";

            _mockUserProvider.Setup(p => p.FindUserByIdAsync(It.IsAny<string>()).Result).Returns(user);
            _mockRoleProvider.Setup(p => p.GetUserRole(It.IsAny<ApplicationUser>()).Result);

            var exception = Assert.ThrowsAsync<NotFoundException>(async () => await _userService.FindUserByIdAsync(user.Id));

            _mockUserProvider.Verify(p => p.FindUserByIdAsync(It.IsAny<string>()), Times.Once);
            _mockRoleProvider.Verify(p => p.GetUserRole(It.IsAny<ApplicationUser>()), Times.Once);
            Assert.AreEqual(exceptionMessage, exception.Message);
        }


        [Test]
        public void FindUserByIdAsync_ValidInput_RoleObjectIsNull_ShouldThrowNotFoundException()
        {
            var exceptionMessage = $"Role with name = {roleName} isn't found";

            _mockUserProvider.Setup(p => p.FindUserByIdAsync(It.IsAny<string>()).Result).Returns(user);
            _mockRoleProvider.Setup(p => p.GetUserRole(It.IsAny<ApplicationUser>()).Result).Returns(roleName);
            _mockRoleProvider.Setup(p => p.FindRoleByNameAsync(It.IsAny<string>()).Result);

            var exception = Assert.ThrowsAsync<NotFoundException>(async () => await _userService.FindUserByIdAsync(user.Id));

            _mockUserProvider.Verify(p => p.FindUserByIdAsync(It.IsAny<string>()), Times.Once);
            _mockRoleProvider.Verify(p => p.GetUserRole(It.IsAny<ApplicationUser>()), Times.Once);
            _mockRoleProvider.Verify(p => p.FindRoleByNameAsync(It.IsAny<string>()), Times.Once);
            Assert.AreEqual(exceptionMessage, exception.Message);
        }


        [Test]
        public async Task GetUsersAsync_ReturnList()
        {
            _mockUserProvider.Setup(p => p.GetUsersAsync().Result).Returns(usersList);
            _mockRoleProvider.Setup(p => p.GetUserRole(It.IsAny<ApplicationUser>()).Result).Returns(role.Name);
            _mockRoleProvider.Setup(p => p.FindRoleByNameAsync(It.IsAny<string>()).Result).Returns(role);

            var users = await _userService.GetUsersAsync();

            _mockUserProvider.Verify(p => p.GetUsersAsync(), Times.Once);
            _mockRoleProvider.Verify(p => p.GetUserRole(It.IsAny<ApplicationUser>()), Times.Exactly(usersList.Count));
            _mockRoleProvider.Verify(p => p.FindRoleByNameAsync(It.IsAny<string>()), Times.Exactly(usersList.Count));
            Assert.AreEqual(usersList.Count, users.Count);
        }

        [Test]
        public void GetUserAsync_RoleNameIsNull_ShouldThrowNotFoundException()
        {
            var firstUser = usersList.First();
            var exceptionMessage = $"Role for user = {firstUser.UserName} isn't found";

            _mockUserProvider.Setup(p => p.GetUsersAsync().Result).Returns(usersList);
            _mockRoleProvider.Setup(p => p.GetUserRole(It.IsAny<ApplicationUser>()).Result);

            var exception = Assert.ThrowsAsync<NotFoundException>(async () => await _userService.GetUsersAsync());
            _mockUserProvider.Verify(p => p.GetUsersAsync(), Times.Once);
            _mockRoleProvider.Verify(p => p.GetUserRole(It.IsAny<ApplicationUser>()), Times.Once);
            Assert.AreEqual(exceptionMessage, exception.Message);
        }


        [Test]
        public void GetUserAsync_RoleObjectIsNull_ShouldThrowNotFoundException()
        {
            var exceptionMessage = $"Role with name = {roleName} isn't found";

            _mockUserProvider.Setup(p => p.GetUsersAsync().Result).Returns(usersList);
            _mockRoleProvider.Setup(p => p.GetUserRole(It.IsAny<ApplicationUser>()).Result).Returns(roleName);
            _mockRoleProvider.Setup(p => p.FindRoleByNameAsync(It.IsAny<string>()).Result);

            var exception = Assert.ThrowsAsync<NotFoundException>(async () => await _userService.GetUsersAsync());
            _mockUserProvider.Verify(p => p.GetUsersAsync(), Times.Once);
            _mockRoleProvider.Verify(p => p.GetUserRole(It.IsAny<ApplicationUser>()), Times.Once);
            _mockRoleProvider.Verify(p => p.FindRoleByNameAsync(It.IsAny<string>()), Times.Once);
            Assert.AreEqual(exceptionMessage, exception.Message);
        }


        [Test]
        public async Task SignUpAsync_ValidInput()
        {
            var identityResult = IdentityResult.Success;       

            _mockRoleProvider.Setup(p => p.FindRoleByNameAsync(It.IsAny<string>()).Result).Returns(role);
            _mockUserProvider.Setup(p => p.CreateUserAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()).Result).Returns(identityResult);
            _mockRoleProvider.Setup(p => p.AddToRoleAsync(It.IsAny<string>(), It.IsAny<string>()).Result);
            _mockAuthProvider.Setup(p => p.SignInAsync(It.IsAny<ApplicationUser>(), false));

            var result = await _userService.SignUpAsync(userDTO, password);

            _mockRoleProvider.Verify(p => p.FindRoleByNameAsync(It.IsAny<string>()), Times.Once);
            _mockUserProvider.Verify(p => p.CreateUserAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()), Times.Once);
            _mockRoleProvider.Verify(p => p.AddToRoleAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _mockAuthProvider.Verify(p => p.SignInAsync(It.IsAny<ApplicationUser>(), It.IsAny<bool>()));
            Assert.IsNotNull(result);
            Assert.AreEqual(identityResult.Succeeded, result.Succeeded);
        }

        [Test]
        public async Task SignUpAsync_ValidInput_ResultFailed()
        {
            var identityResult = new IdentityResult();

            _mockRoleProvider.Setup(p => p.FindRoleByNameAsync(It.IsAny<string>()).Result).Returns(role);
            _mockUserProvider.Setup(p => p.CreateUserAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()).Result).Returns(identityResult);

            var result = await _userService.SignUpAsync(userDTO, password);

            _mockRoleProvider.Verify(p => p.FindRoleByNameAsync(It.IsAny<string>()), Times.Once);
            _mockUserProvider.Verify(p => p.CreateUserAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual(identityResult.Succeeded, result.Succeeded);
        }

        [Test]
        public void SignUpAsync_UserIsNull_ShouldThrowArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _userService.SignUpAsync(null, password));
        }

        [Test]
        public void SignUpAsync_ValidInput_RoleNotFound_ShouldThrowInvalidOperationException()
        {
            _mockRoleProvider.Setup(p => p.FindRoleByNameAsync(It.IsAny<string>()).Result);

            Assert.ThrowsAsync<InvalidOperationException>(async () => await _userService.SignUpAsync(userDTO, password));
            _mockRoleProvider.Verify(p => p.FindRoleByNameAsync(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task DeleteUserByIdAsync_ValidInput()
        {
            var identityResult = IdentityResult.Success;

            _mockUserProvider.Setup(p => p.FindUserByIdAsync(It.IsAny<string>()).Result).Returns(user);
            _mockUserProvider.Setup(p => p.DeleteUserAsync(It.IsAny<ApplicationUser>()).Result).Returns(identityResult);

            await _userService.DeleteUserByIdAsync(userId);

            _mockUserProvider.Verify(p => p.FindUserByIdAsync(It.IsAny<string>()), Times.Once);
            _mockUserProvider.Verify(p => p.DeleteUserAsync(It.IsAny<ApplicationUser>()), Times.Once);
        }

        [Test]
        public void DeleteUserByIdAsync_InputIsNull_ShouldThrowArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _userService.DeleteUserByIdAsync(null));
        }

        [Test]
        public void DeleteUserByIdAsync_InvalidInput_ShouldThrowNotFoundException()
        {
            _mockUserProvider.Setup(p => p.FindUserByIdAsync(It.IsAny<string>()).Result);

            Assert.ThrowsAsync<NotFoundException>(async () => await _userService.DeleteUserByIdAsync(userId));
        }

        [Test]
        public void DeleteUserByIdAsync_ValidInput_ErrorDelete_ShouldThrowInvalidOperationException()
        {
            var identityResult = new IdentityResult();

            _mockUserProvider.Setup(p => p.FindUserByIdAsync(It.IsAny<string>()).Result).Returns(user);
            _mockUserProvider.Setup(p => p.DeleteUserAsync(It.IsAny<ApplicationUser>()).Result).Returns(identityResult);

            Assert.ThrowsAsync<InvalidOperationException>(async () => await _userService.DeleteUserByIdAsync(userId));

            _mockUserProvider.Verify(p => p.FindUserByIdAsync(It.IsAny<string>()), Times.Once);
            _mockUserProvider.Verify(p => p.DeleteUserAsync(It.IsAny<ApplicationUser>()), Times.Once);
        }

        [Test]
        public async Task FindUserByNameAsync_ValidInput()
        {
            _mockUserProvider.Setup(p => p.GetUserByNameAsync(It.IsAny<string>()).Result).Returns(user);

            var userFromService = await _userService.FindUserByNameAsync(userName);

            _mockUserProvider.Verify(p => p.GetUserByNameAsync(It.IsAny<string>()), Times.Once);
            Assert.IsNotNull(userFromService);
            Assert.AreEqual(userId, userFromService.Id);
        }

        [Test]
        public void FindUserByNameAsync_InputIsNull_ShouldThrowArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _userService.FindUserByNameAsync(null));
        }

        [Test]
        public void FindUserByNameAsync_InvalidInput_ShouldThrowNotFoundException()
        {
            _mockUserProvider.Setup(p => p.GetUserByNameAsync(It.IsAny<string>()).Result);

            Assert.ThrowsAsync<NotFoundException>(async () => await _userService.FindUserByNameAsync(userId));
            _mockUserProvider.Verify(p => p.GetUserByNameAsync(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task GetUsersByRoleAsync_ValidInput_ReturnList()
        {
            _mockUserProvider.Setup(p => p.GetUsersByRoleAsync(It.IsAny<string>()).Result).Returns(usersList);

            var users = await _userService.GetUsersByRoleAsync(roleName);

            _mockUserProvider.Verify(p => p.GetUsersByRoleAsync(It.IsAny<string>()), Times.Once);
            Assert.AreEqual(usersList.Count, users.Count);
        }

        [Test]
        public void GetUsersByRoleAsync_InvalidInput()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _userService.GetUsersByRoleAsync(null));
        }
    }
}
