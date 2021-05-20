using AutoMapper;
using BoxingClub.BLL.DomainEntities;
using BoxingClub.BLL.Implementation.Services;
using BoxingClub.BLL.Interfaces;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using BoxingClub.Infrastructure.Exceptions;
using BoxingClub.Web.Mapping;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
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
        private static readonly List<ApplicationUser> _usersList = new List<ApplicationUser>()
        {
            new ApplicationUser { Id = "test1" },
            new ApplicationUser { Id = "test2" },
            new ApplicationUser { Id = "test3" }
        };

        private static readonly object[] CasesForConstructor =
        {
            new object[] {null, new Mock<IRoleProvider>().Object, new Mock<IAuthenticationProvider>().Object, new Mock<IMapper>().Object, "userProvider is null" },
            new object[] {new Mock<IUserProvider>().Object, null, new Mock<IAuthenticationProvider>().Object, new Mock<IMapper>().Object, "roleProvider is null" },
            new object[] {new Mock<IUserProvider>().Object, new Mock<IRoleProvider>().Object, null, new Mock<IMapper>().Object, "authenticationProvider is null" },
            new object[] {new Mock<IUserProvider>().Object, new Mock<IRoleProvider>().Object, new Mock<IAuthenticationProvider>().Object, null, "mapper is null" }
        };

        private static readonly IdentityRole _role = new IdentityRole() { Id = "testId", Name = "Test" };
        private static readonly ApplicationUser _user = new ApplicationUser() { Id = "testId", UserName = "Test" };
        private static readonly string _roleName = _role.Name;
        private static readonly string _roleId = _role.Id;
        private static readonly string _userId = _user.Id;
        private static readonly string _userName = _user.UserName;
        private static readonly UserDTO _userDTO = new UserDTO { Id = "test", Name = "Test" };
        private static readonly string _password = "test";


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
        public void UserServiceConstructor_ShouldConstruct()
        {
            var userService = new UserService(_mockUserProvider.Object, _mockRoleProvider.Object, _mockAuthProvider.Object, _mapper);

            Assert.IsNotNull(userService);
        }

        [Test]
        [TestCaseSource(nameof(CasesForConstructor))]
        public void UserServiceConstructor_ShouldThrowArgumentNullException(IUserProvider userProvider, IRoleProvider roleProvider,
                                                                            IAuthenticationProvider authProvider, IMapper mapper,
                                                                            string exceptionMessage)
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new UserService(userProvider, roleProvider, authProvider, mapper));

            Assert.AreEqual(exceptionMessage, exception.Message);
        }

        [Test]
        public async Task FindUserByIdAsync_ValidInput()
        {
            _mockUserProvider.Setup(p => p.FindUserByIdAsync(It.IsAny<string>()).Result).Returns(_user);
            _mockRoleProvider.Setup(p => p.GetUserRole(It.IsAny<ApplicationUser>()).Result).Returns(_role.Name);
            _mockRoleProvider.Setup(p => p.FindRoleByNameAsync(It.IsAny<string>()).Result).Returns(_role);

            var userFromService = await _userService.FindUserByIdAsync(_user.Id);

            _mockUserProvider.Verify(p => p.FindUserByIdAsync(It.IsAny<string>()), Times.Once);
            _mockRoleProvider.Verify(p => p.GetUserRole(It.IsAny<ApplicationUser>()), Times.Once);
            _mockRoleProvider.Verify(p => p.FindRoleByNameAsync(It.IsAny<string>()), Times.Once);
            Assert.IsNotNull(userFromService);
            Assert.AreEqual(_user.Id, userFromService.Id);
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
            var exceptionMessage = $"Role for user = {_user.UserName} isn't found";

            _mockUserProvider.Setup(p => p.FindUserByIdAsync(It.IsAny<string>()).Result).Returns(_user);
            _mockRoleProvider.Setup(p => p.GetUserRole(It.IsAny<ApplicationUser>()).Result);

            var exception = Assert.ThrowsAsync<NotFoundException>(async () => await _userService.FindUserByIdAsync(_user.Id));

            _mockUserProvider.Verify(p => p.FindUserByIdAsync(It.IsAny<string>()), Times.Once);
            _mockRoleProvider.Verify(p => p.GetUserRole(It.IsAny<ApplicationUser>()), Times.Once);
            Assert.AreEqual(exceptionMessage, exception.Message);
        }


        [Test]
        public void FindUserByIdAsync_ValidInput_RoleObjectIsNull_ShouldThrowNotFoundException()
        {
            var exceptionMessage = $"Role with name = {_roleName} isn't found";

            _mockUserProvider.Setup(p => p.FindUserByIdAsync(It.IsAny<string>()).Result).Returns(_user);
            _mockRoleProvider.Setup(p => p.GetUserRole(It.IsAny<ApplicationUser>()).Result).Returns(_roleName);
            _mockRoleProvider.Setup(p => p.FindRoleByNameAsync(It.IsAny<string>()).Result);

            var exception = Assert.ThrowsAsync<NotFoundException>(async () => await _userService.FindUserByIdAsync(_user.Id));

            _mockUserProvider.Verify(p => p.FindUserByIdAsync(It.IsAny<string>()), Times.Once);
            _mockRoleProvider.Verify(p => p.GetUserRole(It.IsAny<ApplicationUser>()), Times.Once);
            _mockRoleProvider.Verify(p => p.FindRoleByNameAsync(It.IsAny<string>()), Times.Once);
            Assert.AreEqual(exceptionMessage, exception.Message);
        }


        [Test]
        public async Task GetUsersAsync_ReturnList()
        {
            _mockUserProvider.Setup(p => p.GetUsersAsync().Result).Returns(_usersList);
            _mockRoleProvider.Setup(p => p.GetUserRole(It.IsAny<ApplicationUser>()).Result).Returns(_role.Name);
            _mockRoleProvider.Setup(p => p.FindRoleByNameAsync(It.IsAny<string>()).Result).Returns(_role);

            var users = await _userService.GetUsersAsync();

            _mockUserProvider.Verify(p => p.GetUsersAsync(), Times.Once);
            _mockRoleProvider.Verify(p => p.GetUserRole(It.IsAny<ApplicationUser>()), Times.Exactly(_usersList.Count));
            _mockRoleProvider.Verify(p => p.FindRoleByNameAsync(It.IsAny<string>()), Times.Exactly(_usersList.Count));
            Assert.AreEqual(_usersList.Count, users.Count);
        }

        [Test]
        public void GetUserAsync_RoleNameIsNull_ShouldThrowNotFoundException()
        {
            var firstUser = _usersList.First();
            var exceptionMessage = $"Role for user = {firstUser.UserName} isn't found";

            _mockUserProvider.Setup(p => p.GetUsersAsync().Result).Returns(_usersList);
            _mockRoleProvider.Setup(p => p.GetUserRole(It.IsAny<ApplicationUser>()).Result);

            var exception = Assert.ThrowsAsync<NotFoundException>(async () => await _userService.GetUsersAsync());
            _mockUserProvider.Verify(p => p.GetUsersAsync(), Times.Once);
            _mockRoleProvider.Verify(p => p.GetUserRole(It.IsAny<ApplicationUser>()), Times.Once);
            Assert.AreEqual(exceptionMessage, exception.Message);
        }


        [Test]
        public void GetUserAsync_RoleObjectIsNull_ShouldThrowNotFoundException()
        {
            var exceptionMessage = $"Role with name = {_roleName} isn't found";

            _mockUserProvider.Setup(p => p.GetUsersAsync().Result).Returns(_usersList);
            _mockRoleProvider.Setup(p => p.GetUserRole(It.IsAny<ApplicationUser>()).Result).Returns(_roleName);
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

            _mockRoleProvider.Setup(p => p.FindRoleByNameAsync(It.IsAny<string>()).Result).Returns(_role);
            _mockUserProvider.Setup(p => p.CreateUserAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()).Result).Returns(identityResult);
            _mockRoleProvider.Setup(p => p.AddToRoleAsync(It.IsAny<string>(), It.IsAny<string>()).Result);
            _mockAuthProvider.Setup(p => p.SignInAsync(It.IsAny<ApplicationUser>(), false));

            var result = await _userService.SignUpAsync(_userDTO, _password);

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

            _mockRoleProvider.Setup(p => p.FindRoleByNameAsync(It.IsAny<string>()).Result).Returns(_role);
            _mockUserProvider.Setup(p => p.CreateUserAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()).Result).Returns(identityResult);

            var result = await _userService.SignUpAsync(_userDTO, _password);

            _mockRoleProvider.Verify(p => p.FindRoleByNameAsync(It.IsAny<string>()), Times.Once);
            _mockUserProvider.Verify(p => p.CreateUserAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual(identityResult.Succeeded, result.Succeeded);
        }

        [Test]
        public void SignUpAsync_UserIsNull_ShouldThrowArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _userService.SignUpAsync(null, _password));
        }

        [Test]
        public void SignUpAsync_ValidInput_RoleNotFound_ShouldThrowInvalidOperationException()
        {
            _mockRoleProvider.Setup(p => p.FindRoleByNameAsync(It.IsAny<string>()).Result);

            Assert.ThrowsAsync<InvalidOperationException>(async () => await _userService.SignUpAsync(_userDTO, _password));
            _mockRoleProvider.Verify(p => p.FindRoleByNameAsync(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task DeleteUserByIdAsync_ValidInput()
        {
            var identityResult = IdentityResult.Success;

            _mockUserProvider.Setup(p => p.FindUserByIdAsync(It.IsAny<string>()).Result).Returns(_user);
            _mockUserProvider.Setup(p => p.DeleteUserAsync(It.IsAny<ApplicationUser>()).Result).Returns(identityResult);

            await _userService.DeleteUserByIdAsync(_userId);

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

            Assert.ThrowsAsync<NotFoundException>(async () => await _userService.DeleteUserByIdAsync(_userId));
        }

        [Test]
        public void DeleteUserByIdAsync_ValidInput_ErrorDelete_ShouldThrowInvalidOperationException()
        {
            var identityResult = new IdentityResult();

            _mockUserProvider.Setup(p => p.FindUserByIdAsync(It.IsAny<string>()).Result).Returns(_user);
            _mockUserProvider.Setup(p => p.DeleteUserAsync(It.IsAny<ApplicationUser>()).Result).Returns(identityResult);

            Assert.ThrowsAsync<InvalidOperationException>(async () => await _userService.DeleteUserByIdAsync(_userId));

            _mockUserProvider.Verify(p => p.FindUserByIdAsync(It.IsAny<string>()), Times.Once);
            _mockUserProvider.Verify(p => p.DeleteUserAsync(It.IsAny<ApplicationUser>()), Times.Once);
        }

        [Test]
        public async Task FindUserByNameAsync_ValidInput()
        {
            _mockUserProvider.Setup(p => p.GetUserByNameAsync(It.IsAny<string>()).Result).Returns(_user);

            var userFromService = await _userService.FindUserByNameAsync(_userName);

            _mockUserProvider.Verify(p => p.GetUserByNameAsync(It.IsAny<string>()), Times.Once);
            Assert.IsNotNull(userFromService);
            Assert.AreEqual(_userId, userFromService.Id);
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

            Assert.ThrowsAsync<NotFoundException>(async () => await _userService.FindUserByNameAsync(_userId));
            _mockUserProvider.Verify(p => p.GetUserByNameAsync(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task GetUsersByRoleAsync_ValidInput_ReturnList()
        {
            _mockUserProvider.Setup(p => p.GetUsersByRoleAsync(It.IsAny<string>()).Result).Returns(_usersList);

            var users = await _userService.GetUsersByRoleAsync(_roleName);

            _mockUserProvider.Verify(p => p.GetUsersByRoleAsync(It.IsAny<string>()), Times.Once);
            Assert.AreEqual(_usersList.Count, users.Count);
        }

        [Test]
        public void GetUsersByRoleAsync_InvalidInput()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _userService.GetUsersByRoleAsync(null));
        }

        [Test]
        public async Task UpdateUserAsync_ValidInput()
        {
            var newRoleDTO = new RoleDTO { Id = "newTest", Name = "newTest" };
            var newRole = new IdentityRole { Id = "newTest", Name = "newTest" };
            var userDTO = new UserDTO { Id = "test", Name = "Test", Role = newRoleDTO };
            var identityResult = IdentityResult.Success;

            _mockUserProvider.Setup(p => p.FindUserByIdAsync(It.IsAny<string>()).Result).Returns(_user);
            _mockRoleProvider.Setup(p => p.GetUserRole(It.IsAny<ApplicationUser>()).Result).Returns(_role.Name);
            _mockRoleProvider.Setup(p => p.FindRoleByIdAsync(It.IsAny<string>()).Result).Returns(newRole);
            _mockRoleProvider.Setup(p => p.RemoveFromRoleAsync(It.IsAny<string>(), It.IsAny<string>()).Result).Returns(identityResult);
            _mockRoleProvider.Setup(p => p.AddToRoleAsync(It.IsAny<string>(), It.IsAny<string>()).Result).Returns(identityResult);
            _mockUserProvider.Setup(p => p.UpdateUserAsync(It.IsAny<ApplicationUser>()).Result).Returns(identityResult);

            var result = await _userService.UpdateUserAsync(userDTO);

            _mockUserProvider.Verify(p => p.FindUserByIdAsync(It.IsAny<string>()), Times.Once);
            _mockRoleProvider.Verify(p => p.GetUserRole(It.IsAny<ApplicationUser>()), Times.Once);
            _mockRoleProvider.Verify(p => p.FindRoleByIdAsync(It.IsAny<string>()), Times.Once);
            _mockRoleProvider.Verify(p => p.RemoveFromRoleAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _mockRoleProvider.Verify(p => p.AddToRoleAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _mockUserProvider.Verify(p => p.UpdateUserAsync(It.IsAny<ApplicationUser>()), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual(identityResult.Succeeded, result.Succeeded);
        }


        [Test]
        public void UpdateUserAsync_InputIsNull_ShouldThrowArgumentNullException()
        {
            var exceptionMessage = "User is null";

            var exception = Assert.ThrowsAsync<ArgumentNullException>(async () => await _userService.UpdateUserAsync(null));
            Assert.AreEqual(exceptionMessage, exception.Message);
        }

        [Test]
        public void UpdateUserAsync_InvalidInput_ShouldThrowNotFoundException()
        {
            var exceptionMessage = $"User with id = {_userDTO.Id} isn't found";
            _mockUserProvider.Setup(p => p.FindUserByIdAsync(It.IsAny<string>()).Result);

            var exception = Assert.ThrowsAsync<NotFoundException>(async () => await _userService.UpdateUserAsync(_userDTO));

            _mockUserProvider.Verify(p => p.FindUserByIdAsync(It.IsAny<string>()), Times.Once);
            Assert.AreEqual(exceptionMessage, exception.Message);
        }


        [Test]
        public void UpdateUserAsync_ValidInput_RoleNameNotFound_ShouldThrowNotFoundException()
        {
            var newRoleDTO = new RoleDTO { Id = "newTest", Name = "newTest" };
            var userDTO = new UserDTO { Id = "test", Name = "Test", UserName = "TestUserName", Role = newRoleDTO };
            var exceptionMessage = $"Role for user = {userDTO.UserName} isn't found";

            _mockUserProvider.Setup(p => p.FindUserByIdAsync(It.IsAny<string>()).Result).Returns(_user);
            _mockRoleProvider.Setup(p => p.GetUserRole(It.IsAny<ApplicationUser>()).Result);

            var exception = Assert.ThrowsAsync<NotFoundException>(async () => await _userService.UpdateUserAsync(userDTO));

            _mockUserProvider.Verify(p => p.FindUserByIdAsync(It.IsAny<string>()), Times.Once);
            _mockRoleProvider.Verify(p => p.GetUserRole(It.IsAny<ApplicationUser>()), Times.Once);
            Assert.AreEqual(exceptionMessage, exception.Message);
        }

        [Test]
        public void UpdateUserAsync_ValidInput_newRoleIdIsNull_ShouldThrowArgumentNullException()
        {
            var newRoleDTO = new RoleDTO { Id = null, Name = "newTest" };
            var userDTO = new UserDTO { Id = "test", Name = "Test", UserName = "TestUserName", Role = newRoleDTO };
            var exceptionMessage = "newRoleId is null";

            _mockUserProvider.Setup(p => p.FindUserByIdAsync(It.IsAny<string>()).Result).Returns(_user);
            _mockRoleProvider.Setup(p => p.GetUserRole(It.IsAny<ApplicationUser>()).Result).Returns(_role.Name);

            var exception = Assert.ThrowsAsync<ArgumentNullException>(async () => await _userService.UpdateUserAsync(userDTO));

            _mockUserProvider.Verify(p => p.FindUserByIdAsync(It.IsAny<string>()), Times.Once);
            _mockRoleProvider.Verify(p => p.GetUserRole(It.IsAny<ApplicationUser>()), Times.Once);
            Assert.AreEqual(exceptionMessage, exception.Message);
        }

        [Test]
        public void UpdateUserAsync_ValidInput_newRoleNotFound_ShouldThrowNotFoundException()
        {
            var newRoleDTO = new RoleDTO { Id = "newTest", Name = "newTest" };
            var userDTO = new UserDTO { Id = "test", Name = "Test", UserName = "TestUserName", Role = newRoleDTO };
            var exceptionMessage = $"Role for user = {_userId} isn't found";

            _mockUserProvider.Setup(p => p.FindUserByIdAsync(It.IsAny<string>()).Result).Returns(_user);
            _mockRoleProvider.Setup(p => p.GetUserRole(It.IsAny<ApplicationUser>()).Result).Returns(_role.Name);
            _mockRoleProvider.Setup(p => p.FindRoleByIdAsync(It.IsAny<string>()).Result);

            var exception = Assert.ThrowsAsync<NotFoundException>(async () => await _userService.UpdateUserAsync(userDTO));

            _mockUserProvider.Verify(p => p.FindUserByIdAsync(It.IsAny<string>()), Times.Once);
            _mockRoleProvider.Verify(p => p.GetUserRole(It.IsAny<ApplicationUser>()), Times.Once);
            _mockRoleProvider.Verify(p => p.FindRoleByIdAsync(It.IsAny<string>()), Times.Once);
            Assert.AreEqual(exceptionMessage, exception.Message);
        }

        [Test]
        public async Task UpdateUserAsync_ValidInput_SameOldRoleAndNewRole()
        {
            var newRoleDTO = new RoleDTO { Id = "testId", Name = "Test" };
            var newRole = new IdentityRole { Id = "testId", Name = "Test" };
            var userDTO = new UserDTO { Id = "test", Name = "Test", Role = newRoleDTO };
            var identityResult = IdentityResult.Success;

            _mockUserProvider.Setup(p => p.FindUserByIdAsync(It.IsAny<string>()).Result).Returns(_user);
            _mockRoleProvider.Setup(p => p.GetUserRole(It.IsAny<ApplicationUser>()).Result).Returns(_role.Name);
            _mockRoleProvider.Setup(p => p.FindRoleByIdAsync(It.IsAny<string>()).Result).Returns(newRole);
            _mockUserProvider.Setup(p => p.UpdateUserAsync(It.IsAny<ApplicationUser>()).Result).Returns(identityResult);

            var result = await _userService.UpdateUserAsync(userDTO);

            _mockUserProvider.Verify(p => p.FindUserByIdAsync(It.IsAny<string>()), Times.Once);
            _mockRoleProvider.Verify(p => p.GetUserRole(It.IsAny<ApplicationUser>()), Times.Once);
            _mockRoleProvider.Verify(p => p.FindRoleByIdAsync(It.IsAny<string>()), Times.Once);
            _mockUserProvider.Verify(p => p.UpdateUserAsync(It.IsAny<ApplicationUser>()), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual(identityResult.Succeeded, result.Succeeded);
        }

        [Test]
        public async Task UpdateUserAsync_ValidInput_ErrorOnChangingRole()
        {
            var newRoleDTO = new RoleDTO { Id = "newTest", Name = "newTest" };
            var newRole = new IdentityRole { Id = "newTest", Name = "newTest" };
            var userDTO = new UserDTO { Id = "test", Name = "Test", UserName = "TestUserName", Role = newRoleDTO };
            var identityResult = new IdentityResult();

            _mockUserProvider.Setup(p => p.FindUserByIdAsync(It.IsAny<string>()).Result).Returns(_user);
            _mockRoleProvider.Setup(p => p.GetUserRole(It.IsAny<ApplicationUser>()).Result).Returns(_role.Name);
            _mockRoleProvider.Setup(p => p.FindRoleByIdAsync(It.IsAny<string>()).Result).Returns(newRole);
            _mockRoleProvider.Setup(p => p.RemoveFromRoleAsync(It.IsAny<string>(), It.IsAny<string>()).Result).Returns(identityResult);

            var result = await _userService.UpdateUserAsync(userDTO);

            _mockUserProvider.Verify(p => p.FindUserByIdAsync(It.IsAny<string>()), Times.Once);
            _mockRoleProvider.Verify(p => p.GetUserRole(It.IsAny<ApplicationUser>()), Times.Once);
            _mockRoleProvider.Verify(p => p.FindRoleByIdAsync(It.IsAny<string>()), Times.Once);
            _mockRoleProvider.Verify(p => p.RemoveFromRoleAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);

            Assert.IsNotNull(result);
            Assert.AreEqual(identityResult.Succeeded, result.Succeeded);
        }

        [Test]
        public async Task GetUsersPaginatedAsync_ValidInput_ReturnList()
        {
            var searchModel = new SearchModelDTO
            {
                PageIndex = 1,
                PageSize = 3
            };
            var count = 6;

            _mockUserProvider.Setup(p => p.GetUsersPaginatedAsync(It.IsAny<int>(), It.IsAny<int>()).Result).Returns(_usersList);
            _mockUserProvider.Setup(p => p.GetCountOfUsersAsync().Result).Returns(count);
            _mockRoleProvider.Setup(p => p.GetUserRole(It.IsAny<ApplicationUser>()).Result).Returns(_role.Name);
            _mockRoleProvider.Setup(p => p.FindRoleByNameAsync(It.IsAny<string>()).Result).Returns(_role);

            var pageModel = await _userService.GetUsersPaginatedAsync(searchModel);

            _mockUserProvider.Verify(p => p.GetUsersPaginatedAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
            _mockRoleProvider.Verify(p => p.GetUserRole(It.IsAny<ApplicationUser>()), Times.Exactly(_usersList.Count));
            _mockRoleProvider.Verify(p => p.FindRoleByNameAsync(It.IsAny<string>()), Times.Exactly(_usersList.Count));
            Assert.AreEqual(_usersList.Count, pageModel.Items.Count());
            Assert.AreEqual(count, pageModel.Count);
        }


        [Test]
        public void GetUsersPaginatedAsync_InvalidInput_ShouldThrowArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _userService.GetUsersPaginatedAsync(null));
        }

        [Test]
        public async Task GetUsersPaginatedAsync_PageIndexAndPageSizeIsNull_ShouldAssignValueToPageIndexAndPageSize()
        {
            var searchModel = new SearchModelDTO
            {
                PageIndex = null,
                PageSize = null
            };
            var count = 6;

            _mockUserProvider.Setup(p => p.GetUsersPaginatedAsync(It.IsAny<int>(), It.IsAny<int>()).Result).Returns(_usersList);
            _mockUserProvider.Setup(p => p.GetCountOfUsersAsync().Result).Returns(count);
            _mockRoleProvider.Setup(p => p.GetUserRole(It.IsAny<ApplicationUser>()).Result).Returns(_role.Name);
            _mockRoleProvider.Setup(p => p.FindRoleByNameAsync(It.IsAny<string>()).Result).Returns(_role);

            var pageModel = await _userService.GetUsersPaginatedAsync(searchModel);

            Assert.IsNotNull(searchModel.PageIndex);
            Assert.AreEqual(1, searchModel.PageIndex);
            Assert.IsNotNull(searchModel.PageSize);
            Assert.AreEqual(3, searchModel.PageSize);
        }


        [Test]
        public async Task GetUsersPaginatedAsync_UsersOnGivenPageIndexDoesntExist_ShouldCallGetUsersTwice()
        {
            var searchModel = new SearchModelDTO
            {
                PageIndex = 3,
                PageSize = 3
            };
            var count = 6;

            _mockUserProvider.Setup(p => p.GetUsersPaginatedAsync(searchModel.PageIndex.Value, searchModel.PageSize.Value).Result).Returns(new List<ApplicationUser>());
            _mockUserProvider.Setup(p => p.GetUsersPaginatedAsync(1, searchModel.PageSize.Value).Result).Returns(_usersList);
            _mockUserProvider.Setup(p => p.GetCountOfUsersAsync().Result).Returns(count);
            _mockRoleProvider.Setup(p => p.GetUserRole(It.IsAny<ApplicationUser>()).Result).Returns(_role.Name);
            _mockRoleProvider.Setup(p => p.FindRoleByNameAsync(It.IsAny<string>()).Result).Returns(_role);

            var pageModel = await _userService.GetUsersPaginatedAsync(searchModel);

            _mockUserProvider.Verify(p => p.GetUsersPaginatedAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Exactly(2));
            _mockRoleProvider.Verify(p => p.GetUserRole(It.IsAny<ApplicationUser>()), Times.Exactly(_usersList.Count));
            _mockRoleProvider.Verify(p => p.FindRoleByNameAsync(It.IsAny<string>()), Times.Exactly(_usersList.Count));
            Assert.AreEqual(_usersList.Count, pageModel.Items.Count());
            Assert.AreEqual(count, pageModel.Count);
        }
    }
}
