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
using System.Text;
using System.Threading.Tasks;
using ArgumentNullException = BoxingClub.Infrastructure.Exceptions.ArgumentNullException;

namespace BoxingClub.BLL.UnitTests
{
    [TestFixture]
    class RoleServiceTests
    {
        private IMapper _mapper;
        private Mock<IRoleProvider> _mockRoleProvider;
        private IRoleService _roleService;

        private static readonly object[] CasesForAddRemoveFromRoleInvalidInput =
        {
            new object[] { null, "testRole", "User's id is null"},
            new object[] { "testId", null, "Role is null" }
        };

        private static readonly object[] CasesForIsInRole =
{
            new object[] { null, "testRole", "User is null"},
            new object[] { new UserDTO() { Id = "test" }, null, "Role is null" }
        };

        private static readonly IdentityResult[] CasesIdentityResult =
{
            new IdentityResult(),
            IdentityResult.Success
        };

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
            _mockRoleProvider = new Mock<IRoleProvider>();
            _roleService = new RoleService(_mockRoleProvider.Object, _mapper);
        }

        [Test]
        public async Task FindRoleByIdAsync_ValidInput()
        {
            var role = new IdentityRole() { Id = "test", Name = "test" };
            _mockRoleProvider.Setup(p => p.FindRoleByIdAsync(role.Id).Result).Returns(role);

            var roleFromService = await _roleService.FindRoleByIdAsync(role.Id);

            _mockRoleProvider.Verify(p => p.FindRoleByIdAsync(role.Id), Times.Once);
            Assert.IsNotNull(roleFromService);
            Assert.AreEqual(role.Id, roleFromService.Id);
        }

        [Test]
        public void FindRoleByIdAsync_InvalidInput_ShouldThrowNotFoundException()
        {
            var roleId = "test";

            _mockRoleProvider.Setup(p => p.FindRoleByIdAsync(roleId).Result);

            Assert.ThrowsAsync<NotFoundException>(async () => await _roleService.FindRoleByIdAsync(roleId));
        }

        [Test]
        public void FindRoleByIdAsync_InputIsNull_ShouldThrowArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _roleService.FindRoleByIdAsync(null));
        }

        [Test]
        public async Task GetRolesAsync_ReturnList()
        {
            var roleList = new List<IdentityRole>() { new IdentityRole { Id = "1" }, new IdentityRole { Id = "2" }, new IdentityRole { Id = "3" } };
            _mockRoleProvider.Setup(p => p.GetRolesAsync().Result).Returns(roleList);

            var roles = await _roleService.GetRolesAsync();

            _mockRoleProvider.Verify(p => p.GetRolesAsync(), Times.Once);
            Assert.AreEqual(roleList.Count, roles.Count);
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public async Task IsInRoleAsync_ValidInput_ReturnsTrue(bool inRole)
        {
            var user = new UserDTO() { Id = "test" };
            var roleName = "testRole";

            _mockRoleProvider.Setup(p => p.IsInRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()).Result).Returns(inRole);

            var result = await _roleService.IsInRoleAsync(user, roleName);

            _mockRoleProvider.Verify(p => p.IsInRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual(inRole, result);
        }

        [Test]
        [TestCaseSource(nameof(CasesForIsInRole))]
        public void IsInRoleAsync_InvalidInput_ShouldThrowArgumentNullException(UserDTO user, string roleName, string exceptionMessage)
        {
            var exception = Assert.ThrowsAsync<ArgumentNullException>(async () => await _roleService.IsInRoleAsync(user, roleName));
            Assert.AreEqual(exceptionMessage, exception.Message);
        }


        [Test]
        [TestCaseSource(nameof(CasesIdentityResult))]
        public async Task RemoveFromRoleAsync_ValidInput(IdentityResult identityResult) 
        {
            string userId = "test";
            string roleName = "testRole";

            _mockRoleProvider.Setup(p => p.RemoveFromRoleAsync(It.IsAny<string>(), It.IsAny<string>()).Result).Returns(identityResult);

            var result = await _roleService.RemoveFromRoleAsync(userId, roleName);

            _mockRoleProvider.Verify(p => p.RemoveFromRoleAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual(identityResult.Succeeded, result.Succeeded);
        }

        [Test]
        [TestCaseSource(nameof(CasesForAddRemoveFromRoleInvalidInput))]
        public void RemoveFromRoleAsync_InvalidInput_ShouldThrowArgumentNullException(string userId, string roleName, string exceptionMessage)
        {
            var exception = Assert.ThrowsAsync<ArgumentNullException>(async () => await _roleService.RemoveFromRoleAsync(userId, roleName));
            Assert.AreEqual(exceptionMessage, exception.Message);
        }

        [Test]
        [TestCaseSource(nameof(CasesIdentityResult))]
        public async Task AddToRoleAsync_ValidInput(IdentityResult identityResult)
        {
            string userId = "test";
            string roleName = "testRole";

            _mockRoleProvider.Setup(p => p.AddToRoleAsync(It.IsAny<string>(), It.IsAny<string>()).Result).Returns(identityResult);

            var result = await _roleService.AddToRoleAsync(userId, roleName);

            _mockRoleProvider.Verify(p => p.AddToRoleAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual(identityResult.Succeeded, result.Succeeded);
        }


        [Test]
        [TestCaseSource(nameof(CasesForAddRemoveFromRoleInvalidInput))]
        public void AddToRoleAsync_InvalidInput_ShouldThrowArgumentNullException(string userId, string roleName, string exceptionMessage)
        {
            var exception = Assert.ThrowsAsync<ArgumentNullException>(async () => await _roleService.AddToRoleAsync(userId, roleName));
            Assert.AreEqual(exceptionMessage, exception.Message);
        }
    }
}
