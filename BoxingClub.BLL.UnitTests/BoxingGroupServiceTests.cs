using AutoMapper;
using BoxingClub.BLL.Interfaces;
using BoxingClub.BLL.Services;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using BoxingClub.Web.Mapping;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

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

        public void GetBoxingGroupsAsync_ValidInput()
        {
            var boxingGroupsList = new List<BoxingGroup>() { new BoxingGroup { Id = 1} }
            _mockRepository.Setup(repo => repo.GetAllAsync().Result).Returns
        }
    }
}
