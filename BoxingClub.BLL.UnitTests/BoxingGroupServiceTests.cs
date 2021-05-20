using AutoMapper;
using BoxingClub.BLL.DomainEntities;
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
using System.Linq;
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

        private static readonly object[] CasesForConstructor =
{
            new object[] {null, new Mock<IUnitOfWork>().Object, "mapper is null"},
            new object[] {new Mock<IMapper>().Object, null, "uow is null"}
        };
        private static readonly string _coachId = "test";
        private static readonly List<BoxingGroup> _boxingGroupsList = new List<BoxingGroup>() {
                new BoxingGroup { Id = 1, CoachId = _coachId },
                new BoxingGroup { Id = 2, CoachId = _coachId },
                new BoxingGroup { Id = 3, CoachId = _coachId }
        };

        private static readonly SearchModelDTO _searchModel = new SearchModelDTO
        {
            PageIndex = 3,
            PageSize = 3
        };
        private static readonly int _count = 6;

        private static readonly object[] CasesForGetBoxingGroupsByCoachIdPaginatedAsync =
        {
            new object[] {_coachId, null, "SearchDTO is null" },
            new object[] {null, _searchModel, "Coach id is null" }
        };

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
        public void BoxingGroupServiceConstructor_ShouldConstruct()
        {
            var boxingGroupService = new BoxingGroupService(_mapper, _mockUoW.Object);

            Assert.IsNotNull(boxingGroupService);
        }

        [Test]
        [TestCaseSource(nameof(CasesForConstructor))]
        public void BoxingGroupServiceConstructor_ShouldThrowArgumentNullException(IMapper mapper, IUnitOfWork uow, string exceptionMessage)
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new BoxingGroupService(mapper, uow));

            Assert.AreEqual(exceptionMessage, exception.Message);
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
            _mockRepository.Setup(repo => repo.GetBoxingGroupsByCoachIdAsync(_coachId).Result).Returns(_boxingGroupsList);
            _mockUoW.Setup(uow => uow.BoxingGroups).Returns(_mockRepository.Object);

            var boxingGroups = await _boxingGroupService.GetBoxingGroupsByCoachIdAsync(_coachId);

            _mockRepository.Verify(repo => repo.GetBoxingGroupsByCoachIdAsync(_coachId), Times.Once);
            Assert.AreEqual(_boxingGroupsList.Count, boxingGroups.Count);
        }

        [Test]
        public void GetBoxingGroupsByCoachIdAsync_InputIsNull_ShouldThrowArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _boxingGroupService.GetBoxingGroupsByCoachIdAsync(null));
        }

        [Test]
        public async Task GetBoxingGroupsPaginatedAsync_ValidInput_ReturnList()
        {
            _mockRepository.Setup(repo => repo.GetBoxingGroupsPaginatedAsync(It.IsAny<int>(), It.IsAny<int>()).Result).Returns(_boxingGroupsList);
            _mockRepository.Setup(repo => repo.GetCountOfBoxingGroupsAsync().Result).Returns(_count);
            _mockUoW.Setup(uow => uow.BoxingGroups).Returns(_mockRepository.Object);

            var pageModel = await _boxingGroupService.GetBoxingGroupsPaginatedAsync(_searchModel);

            _mockRepository.Verify(repo => repo.GetBoxingGroupsPaginatedAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
            _mockRepository.Verify(repo => repo.GetCountOfBoxingGroupsAsync(), Times.Once);
            Assert.AreEqual(_boxingGroupsList.Count, pageModel.Items.Count());
            Assert.AreEqual(_count, pageModel.Count);
        }

        [Test]
        public void GetBoxingGroupsPaginatedAsync_InvalidInput_ShouldThrowArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _boxingGroupService.GetBoxingGroupsPaginatedAsync(null));
        }

        [Test]
        public async Task GetBoxingGroupsPaginatedAsync_PageIndexAndPageSizeIsNull_ShouldAssignValueToPageIndexAndPageSize()
        {
            var searchModel = new SearchModelDTO()
            {
                PageIndex = null,
                PageSize = null
            };

            _mockRepository.Setup(repo => repo.GetBoxingGroupsPaginatedAsync(It.IsAny<int>(), It.IsAny<int>()).Result).Returns(_boxingGroupsList);
            _mockRepository.Setup(repo => repo.GetCountOfBoxingGroupsAsync().Result).Returns(_count);
            _mockUoW.Setup(uow => uow.BoxingGroups).Returns(_mockRepository.Object);

            var pageModel = await _boxingGroupService.GetBoxingGroupsPaginatedAsync(searchModel);

            Assert.IsNotNull(searchModel.PageIndex);
            Assert.AreEqual(1, searchModel.PageIndex);
            Assert.IsNotNull(searchModel.PageSize);
            Assert.AreEqual(3, searchModel.PageSize);
        }

        [Test]
         public async Task GetBoxingGroupsPaginatedAsync_UsersOnGivenPageIndexDoesntExist_ShouldCallGetUsersTwice()
        {
            _mockRepository.Setup(repo => repo.GetBoxingGroupsPaginatedAsync(_searchModel.PageIndex.Value, _searchModel.PageSize.Value).Result).Returns(new List<BoxingGroup>());
            _mockRepository.Setup(repo => repo.GetBoxingGroupsPaginatedAsync(1, _searchModel.PageSize.Value).Result).Returns(_boxingGroupsList);
            _mockRepository.Setup(repo => repo.GetCountOfBoxingGroupsAsync().Result).Returns(_count);
            _mockUoW.Setup(uow => uow.BoxingGroups).Returns(_mockRepository.Object);

            var pageModel = await _boxingGroupService.GetBoxingGroupsPaginatedAsync(_searchModel);

            _mockRepository.Verify(repo => repo.GetBoxingGroupsPaginatedAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Exactly(2));
            _mockRepository.Verify(repo => repo.GetCountOfBoxingGroupsAsync(), Times.Once);
            Assert.AreEqual(_boxingGroupsList.Count, pageModel.Items.Count());
            Assert.AreEqual(_count, pageModel.Count);
        }

        [Test]
        public async Task GetBoxingGroupsByCoachIdPaginatedAsync_ValidInput_ReturnList()
        {
            _mockRepository.Setup(repo => repo.GetBoxingGroupsByCoachIdPaginatedAsync(_coachId, It.IsAny<int>(), It.IsAny<int>()).Result).Returns(_boxingGroupsList);
            _mockRepository.Setup(repo => repo.GetCountOfBoxingGroupsByCoachIdAsync(_coachId).Result).Returns(_count);
            _mockUoW.Setup(uow => uow.BoxingGroups).Returns(_mockRepository.Object);

            var pageModel = await _boxingGroupService.GetBoxingGroupsByCoachIdPaginatedAsync(_coachId, _searchModel);

            _mockRepository.Verify(repo => repo.GetBoxingGroupsByCoachIdPaginatedAsync(_coachId, It.IsAny<int>(), It.IsAny<int>()), Times.Once);
            _mockRepository.Verify(repo => repo.GetCountOfBoxingGroupsByCoachIdAsync(_coachId), Times.Once);
            Assert.AreEqual(_boxingGroupsList.Count, pageModel.Items.Count());
            Assert.AreEqual(_count, pageModel.Count);
        }

        [Test]
        [TestCaseSource(nameof(CasesForGetBoxingGroupsByCoachIdPaginatedAsync))]
        public void GetBoxingGroupsByCoachIdPaginatedAsync_InvalidInput_ShouldThrowArgumentNullException(string coachId, SearchModelDTO searchModel, string exceptionMessage)
        {
            var exception = Assert.ThrowsAsync<ArgumentNullException>(async () => await _boxingGroupService.GetBoxingGroupsByCoachIdPaginatedAsync(coachId, searchModel));

            Assert.AreEqual(exceptionMessage, exception.Message);
        }

        [Test]
        public async Task GetBoxingGroupsByCoachIdPaginatedAsync_PageIndexAndPageSizeIsNull_ShouldAssignValueToPageIndexAndPageSize()
        {
            var searchModel = new SearchModelDTO()
            {
                PageIndex = null,
                PageSize = null
            };

            _mockRepository.Setup(repo => repo.GetBoxingGroupsByCoachIdPaginatedAsync(_coachId, It.IsAny<int>(), It.IsAny<int>()).Result).Returns(_boxingGroupsList);
            _mockRepository.Setup(repo => repo.GetCountOfBoxingGroupsByCoachIdAsync(_coachId).Result).Returns(_count);
            _mockUoW.Setup(uow => uow.BoxingGroups).Returns(_mockRepository.Object);

            var pageModel = await _boxingGroupService.GetBoxingGroupsByCoachIdPaginatedAsync(_coachId, searchModel);

            Assert.IsNotNull(searchModel.PageIndex);
            Assert.AreEqual(1, searchModel.PageIndex);
            Assert.IsNotNull(searchModel.PageSize);
            Assert.AreEqual(3, searchModel.PageSize);
        }

        [Test]
        public async Task GetBoxingGroupsByCoachIdPaginatedAsync_UsersOnGivenPageIndexDoesntExist_ShouldCallGetUsersTwice()
        {
            var searchModel = new SearchModelDTO()
            {
                PageIndex = 3,
                PageSize = 3
            };

            _mockRepository.Setup(repo => repo.GetBoxingGroupsByCoachIdPaginatedAsync(_coachId, searchModel.PageIndex.Value, searchModel.PageSize.Value).Result).Returns(new List<BoxingGroup>());
            _mockRepository.Setup(repo => repo.GetBoxingGroupsByCoachIdPaginatedAsync(_coachId, 1, searchModel.PageSize.Value).Result).Returns(_boxingGroupsList);
            _mockRepository.Setup(repo => repo.GetCountOfBoxingGroupsByCoachIdAsync(_coachId).Result).Returns(_count);
            _mockUoW.Setup(uow => uow.BoxingGroups).Returns(_mockRepository.Object);

            var pageModel = await _boxingGroupService.GetBoxingGroupsByCoachIdPaginatedAsync(_coachId, searchModel);

            _mockRepository.Verify(repo => repo.GetBoxingGroupsByCoachIdPaginatedAsync(_coachId, It.IsAny<int>(), It.IsAny<int>()), Times.Exactly(2));
            _mockRepository.Verify(repo => repo.GetCountOfBoxingGroupsByCoachIdAsync(_coachId), Times.Once);
            Assert.AreEqual(_boxingGroupsList.Count, pageModel.Items.Count());
            Assert.AreEqual(_count, pageModel.Count);
        }
    }
}
