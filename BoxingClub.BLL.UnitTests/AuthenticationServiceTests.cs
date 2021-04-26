using AutoMapper;
using BoxingClub.BLL.DTO;
using BoxingClub.BLL.Implementation.Services;
using BoxingClub.BLL.Interfaces;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using BoxingClub.Web.Mapping;
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
    class AuthenticationServiceTests
    {
        private IMapper _mapper;
        private IAuthenticationService _authService;
        private Mock<IAuthenticationProvider> _mockAuthProvider;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var mapperProfiles = new List<Profile>() { new ResultProfile(), new UserProfile() };
            var mapperConfig = new MapperConfiguration(mc => mc.AddProfiles(mapperProfiles));
            _mapper = mapperConfig.CreateMapper();
        }


        [SetUp]
        public void SetUp()
        {
            _mockAuthProvider = new Mock<IAuthenticationProvider>();
            _authService = new AuthenticationService(_mockAuthProvider.Object, _mapper);
        }

        [Test]
        public async Task SignInAsync_ValidInput()
        {
            var userDTO = new UserDTO { Id = "1", Name = "Test" };
            _mockAuthProvider.Setup(p => p.SignInAsync(It.IsAny<SignIn>()).Result);

            await _authService.SignInAsync(userDTO);

            _mockAuthProvider.Verify(p => p.SignInAsync(It.IsAny<SignIn>()), Times.Once);
        }

        [Test]
        public void SignInAsync_InvalidInput_ShouldThrowArgimentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _authService.SignInAsync(null));
        }

        [Test]
        public async Task SignOutAsync()
        {
            _mockAuthProvider.Setup(p => p.SignOutAsync());

            await _authService.SignOutAsync();

            _mockAuthProvider.Verify(p => p.SignOutAsync(), Times.Once);
        }
    }
}
