using AutoMapper;
using BoxingClub.BLL.DTO;
using BoxingClub.BLL.Interfaces;
using BoxingClub.BLL.Services;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using BoxingClub.Infrastructure.Exceptions;
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
    class BoxingGroupServiceTests
    {
        private IMapper _mapper;
        private Mock<IBoxingGroupRepository> _mockRepository;
        private Mock<IUnitOfWork> _mockUoW;
        private IBoxingGroupService _boxingGroupService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var mapperProfiles = new List<Profile>() { new BoxingGroupProfile() };
            var mapperConfig = new MapperConfiguration(mc => mc.AddProfiles(mapperProfiles));
            _mapper = mapperConfig.CreateMapper();
        }

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new Mock<IBoxingGroupRepository>();
            _mockUoW = new Mock<IUnitOfWork>();
            _boxingGroupService = new BoxingGroupService(_mapper, _mockUoW.Object);
        }

        [Test]
        public async Task GetBoxingGroupsAsync_ReturnList()
        {
            var boxingGroupsList = new List<BoxingGroup>() { new BoxingGroup { Id = 1 }, new BoxingGroup { Id = 2 }, new BoxingGroup { Id = 3 } };
            _mockRepository.Setup(repo => repo.GetAllAsync().Result).Returns(boxingGroupsList);
            _mockUoW.Setup(uow => uow.BoxingGroups).Returns(_mockRepository.Object);

            var boxingGroups = await _boxingGroupService.GetBoxingGroupsAsync();

            _mockRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
            Assert.AreEqual(boxingGroupsList.Count, boxingGroups.Count);
        }

        [Test]
        public async Task GetBoxingGroupByIdAsync_ValidInput()
        {
            var boxingGroupId = 1;
            _mockRepository.Setup(repo => repo.GetByIdAsync(boxingGroupId).Result).Returns(new BoxingGroup { Id = boxingGroupId });
            _mockUoW.Setup(uow => uow.BoxingGroups).Returns(_mockRepository.Object);

            var boxingGroup = await _boxingGroupService.GetBoxingGroupByIdAsync(boxingGroupId);

            _mockRepository.Verify(repo => repo.GetByIdAsync(boxingGroupId), Times.Once);
            Assert.IsNotNull(boxingGroup);
            Assert.AreEqual(boxingGroupId, boxingGroup.Id);
        }

        [Test]
        public void GetBoxingGroupByIdAsync_InvalidInput_ShouldThrowNotFoundException()
        {
            _mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()).Result);
            _mockUoW.Setup(uow => uow.BoxingGroups).Returns(_mockRepository.Object);

            Assert.ThrowsAsync<NotFoundException>(async () => await _boxingGroupService.GetBoxingGroupByIdAsync(It.IsAny<int>()));
        }

        [Test]
        public void GetBoxingGroupByIdAsync_InputIsNull_ShouldThrowArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _boxingGroupService.GetBoxingGroupByIdAsync(null));
        }

        [Test]
        public async Task UpdateBoxingGroupAsync_ValidInput()
        {
            var boxingGroup = new BoxingGroupDTO { Id = 1, Name = "Test" };

            _mockRepository.Setup(repo => repo.Update(It.IsAny<BoxingGroup>()));
            _mockUoW.Setup(uow => uow.BoxingGroups).Returns(_mockRepository.Object);

            await _boxingGroupService.UpdateBoxingGroupAsync(boxingGroup);

            _mockRepository.Verify(repo => repo.Update(It.IsAny<BoxingGroup>()), Times.Once);
            _mockUoW.Verify(uow => uow.SaveAsync(), Times.Once);
        }

        [Test]
        public void UpdateBoxingGroupAsync_InputIsNull_ShouldThrowArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _boxingGroupService.UpdateBoxingGroupAsync(null));
        }

        [Test]
        public async Task CreateBoxingGroupAsync_ValidInput()
        {
            var boxingGroup = new BoxingGroupDTO { Id = 1, Name = "Test" };

            _mockRepository.Setup(repo => repo.CreateAsync(It.IsAny<BoxingGroup>()));
            _mockUoW.Setup(uow => uow.BoxingGroups).Returns(_mockRepository.Object);

            await _boxingGroupService.CreateBoxingGroupAsync(boxingGroup);

            _mockRepository.Verify(repo => repo.CreateAsync(It.IsAny<BoxingGroup>()), Times.Once);
            _mockUoW.Verify(uow => uow.SaveAsync(), Times.Once);
        }

        [Test]
        public void CreateBoxingGroupAsync_InputIsNull_ShouldThrowArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _boxingGroupService.CreateBoxingGroupAsync(null));
        }

        [Test]
        public async Task DeleleBoxingGroupAsync_ValidInput()
        {
            var boxingGroup = new BoxingGroup { Id = 1, Name = "Test" };

            _mockRepository.Setup(repo => repo.GetByIdAsync(boxingGroup.Id).Result).Returns(boxingGroup);
            _mockRepository.Setup(repo => repo.Delete(It.IsAny<BoxingGroup>()));
            _mockUoW.Setup(uow => uow.BoxingGroups).Returns(_mockRepository.Object);

            await _boxingGroupService.DeleleBoxingGroupAsync(boxingGroup.Id);

            _mockRepository.Verify(repo => repo.GetByIdAsync(boxingGroup.Id), Times.Once);
            _mockRepository.Verify(repo => repo.Delete(It.IsAny<BoxingGroup>()), Times.Once);
            _mockUoW.Verify(uow => uow.SaveAsync(), Times.Once);
        }

        [Test]
        public void DeleleBoxingGroupAsync_InputIsNull_ShouldThrowArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _boxingGroupService.DeleleBoxingGroupAsync(null));
        }

        [Test]
        public void DeleleBoxingGroupAsync_InvalidInput_ShouldThrowNotFoundException()
        {
            _mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()).Result);
            _mockUoW.Setup(uow => uow.BoxingGroups).Returns(_mockRepository.Object);

            Assert.ThrowsAsync<NotFoundException>(async () => await _boxingGroupService.DeleleBoxingGroupAsync(It.IsAny<int>()));
        }

        [Test]
        public void GetBoxingGroupWithStudentsByIdAsync_ValidInput()
        {
            var boxingGroup = new BoxingGroup { Id = 1, Name = "Test" };

            _mockRepository.Setup(repo => repo.GetBoxingGroupWithStudentsByIdAsync(boxingGroup.Id).Result).Returns(boxingGroup);
            _mockUoW.Setup(uow => uow.BoxingGroups).Returns(_mockRepository.Object);

            var boxingGroupFromService = _boxingGroupService.GetBoxingGroupWithStudentsByIdAsync(boxingGroup.Id);

            _mockRepository.Verify(repo => repo.GetBoxingGroupWithStudentsByIdAsync(boxingGroup.Id), Times.Once);
            Assert.IsNotNull(boxingGroupFromService);
            Assert.AreEqual(boxingGroupFromService.Id, boxingGroup.Id);
        }


        [Test]
        public void GetBoxingGroupWithStudentsByIdAsync_InputIsNull_ShouldThrowArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _boxingGroupService.GetBoxingGroupWithStudentsByIdAsync(null));
        }

        [Test]
        public void GetBoxingGroupWithStudentsByIdAsync_InvalidInput_ShouldThrowNotFoundException()
        {
            _mockRepository.Setup(repo => repo.GetBoxingGroupWithStudentsByIdAsync(It.IsAny<int>()).Result);
            _mockUoW.Setup(uow => uow.BoxingGroups).Returns(_mockRepository.Object);

            Assert.ThrowsAsync<NotFoundException>(async () => await _boxingGroupService.GetBoxingGroupWithStudentsByIdAsync(It.IsAny<int>()));
        }

        [Test]
        public async Task GetBoxingGroupsByCoachIdAsync_ValidInput()
        {
            var coachId = "test";
            var boxingGroupsList = new List<BoxingGroup>() {
                new BoxingGroup { Id = 1, CoachId = coachId },
                new BoxingGroup { Id = 2, CoachId = coachId },
                new BoxingGroup { Id = 3, CoachId = coachId }
            };

            _mockRepository.Setup(repo => repo.GetBoxingGroupsByCoachIdAsync(coachId).Result).Returns(boxingGroupsList);
            _mockUoW.Setup(uow => uow.BoxingGroups).Returns(_mockRepository.Object);

            var boxingGroups = await _boxingGroupService.GetBoxingGroupsByCoachIdAsync(coachId);

            _mockRepository.Verify(repo => repo.GetBoxingGroupsByCoachIdAsync(coachId), Times.Once);
            Assert.AreEqual(boxingGroupsList.Count, boxingGroups.Count);
        }

        [Test]
        public void GetBoxingGroupsByCoachIdAsync_InputIsNull_ShouldThrowArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _boxingGroupService.GetBoxingGroupsByCoachIdAsync(null));
        }

    }
}
