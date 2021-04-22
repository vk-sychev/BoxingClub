using AutoMapper;
using BoxingClub.BLL.DTO;
using BoxingClub.BLL.Interfaces;
using BoxingClub.BLL.Services;
using BoxingClub.DAL.EF;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using BoxingClub.Web.Mapping;
using BoxingClub.WEB.Mapping;
using Moq;
using Moq.Language;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BoxingClub.Infrastructure.Exceptions;
using ArgumentNullException = BoxingClub.Infrastructure.Exceptions.ArgumentNullException;

namespace BoxingClub.BLL.UnitTests
{
    [TestFixture]
    class StudentServiceTests
    {
        private IMapper _mapper;
        private Mock<IStudentRepository> _mockRepository;
        private Mock<IUnitOfWork> _mockUoW;
        private IStudentService _studentService;

        [OneTimeSetUp]
        public void SetUp()
        {
            var mapperProfiles = new List<Profile>() { new BoxingGroupProfile(), new ResultProfile(), new RoleProfile(), new StudentProfile(), new UserProfile() };
            var mapperConfig = new MapperConfiguration(mc => mc.AddProfiles(mapperProfiles));
            _mapper = mapperConfig.CreateMapper();

            _mockRepository = new Mock<IStudentRepository>();
            _mockUoW = new Mock<IUnitOfWork>();
            _studentService = new StudentService(_mockUoW.Object, _mapper);
        }

        [Test]
        public async Task CreateStudent_ValidInput()
        {
            var student = new StudentFullDTO()
            {
                Id = 1,
                Name = "Test",
                Surname = "Test"
            };

            _mockRepository.Setup(repo => repo.CreateAsync(It.IsAny<Student>()));
            _mockUoW.Setup(uow => uow.Students).Returns(_mockRepository.Object);

            await _studentService.CreateStudentAsync(student);

            _mockUoW.Verify(uow => uow.Students, Times.Once);
            _mockRepository.Verify(repo => repo.CreateAsync(It.IsAny<Student>()), Times.Once);
        }

        [Test]
        public void CreateAsync_InputNull_ShouldThrowArgumentNullException()
        {
            /*            _mockRepository.Setup(repo => repo.CreateAsync(null));
                        _mockUoW.Setup(uow => uow.Students).Returns(_mockRepository.Object);*/

            Assert.ThrowsAsync<ArgumentNullException>(async () => await _studentService.CreateStudentAsync(null));
        }

        [Test]
        public async Task GetStudentsAsync_ReturnList()
        {
            var studentsList = new List<Student>() { new Student { Id = 1 }, new Student { Id = 2 }, new Student { Id = 3 } };
            _mockRepository.Setup(repo => repo.GetAllAsync().Result).Returns(studentsList);
            _mockUoW.Setup(uow => uow.Students).Returns(_mockRepository.Object);

            var list = await _studentService.GetStudentsAsync();

            Assert.AreEqual(studentsList.Count, list.Count);
        }

        [Test]
        public async Task GetStudentByIdAsync_ValidInput()
        {
            var studentId = 1;
            _mockRepository.Setup(repo => repo.GetByIdAsync(studentId).Result).Returns(new Student { Id = studentId });
            _mockUoW.Setup(uow => uow.Students).Returns(_mockRepository.Object);

            var student = await _studentService.GetStudentByIdAsync(studentId);

            Assert.IsNotNull(student);
            Assert.AreEqual(studentId, student.Id);
        }

        [Test]
        public void GetStudentByIdAsync_InputIsNull_ShouldThrowArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _studentService.GetStudentByIdAsync(null));
        }

        [Test]
        public void GetStudentByIdAsync_InvalidInput_ShouldThrowNotFoundException()
        {
            int studentId = 20000;
            _mockRepository.Setup(repo => repo.GetByIdAsync(studentId).Result);
            _mockUoW.Setup(uow => uow.Students).Returns(_mockRepository.Object);

            Assert.ThrowsAsync<NotFoundException>(async () => await _studentService.GetStudentByIdAsync(studentId));
        }


        [Test]
        public async Task DeleteStudentAsync_ValidInput()
        {

        }


    }
}
