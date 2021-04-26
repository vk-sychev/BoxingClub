using AutoMapper;
using BoxingClub.BLL.DTO;
using BoxingClub.BLL.Interfaces;
using BoxingClub.BLL.Services;
using BoxingClub.DAL.EF;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using BoxingClub.Web.Mapping;
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
        public void OneTimeSetUp()
        {
            var mapperProfiles = new List<Profile>() { new StudentProfile() };
            var mapperConfig = new MapperConfiguration(mc => mc.AddProfiles(mapperProfiles));
            _mapper = mapperConfig.CreateMapper();
        }

        [SetUp]
        public void SetUp()
        {
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

            _mockRepository.Verify(repo => repo.CreateAsync(It.IsAny<Student>()), Times.Once);
            _mockUoW.Verify(uow => uow.SaveAsync());
        }

        [Test]
        public void CreateAsync_InputNull_ShouldThrowArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _studentService.CreateStudentAsync(null));
        }

        [Test]
        public async Task GetStudentsAsync_ReturnList()
        {
            var studentsList = new List<Student>() { new Student { Id = 1 }, new Student { Id = 2 }, new Student { Id = 3 } };
            _mockRepository.Setup(repo => repo.GetAllAsync().Result).Returns(studentsList);
            _mockUoW.Setup(uow => uow.Students).Returns(_mockRepository.Object);

            var list = await _studentService.GetStudentsAsync();

            _mockRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
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
            _mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()).Result);
            _mockUoW.Setup(uow => uow.Students).Returns(_mockRepository.Object);

            Assert.ThrowsAsync<NotFoundException>(async () => await _studentService.GetStudentByIdAsync(It.IsAny<int>()));
        }


        [Test]
        public async Task DeleteStudentAsync_ValidInput()
        {
            var student = new Student()
            {
                Id = 1,
                Name = "Test",
                Surname = "Test"
            };

            _mockRepository.Setup(repo => repo.GetByIdAsync(student.Id).Result).Returns(student);
            _mockRepository.Setup(repo => repo.Delete(It.IsAny<Student>()));
            _mockUoW.Setup(uow => uow.Students).Returns(_mockRepository.Object);

            await _studentService.DeleteStudentAsync(student.Id);

            _mockRepository.Verify(repo => repo.GetByIdAsync(student.Id), Times.Once);
            _mockRepository.Verify(repo => repo.Delete(It.IsAny<Student>()), Times.Once);
            _mockUoW.Verify(uow => uow.SaveAsync(), Times.Once);
        }


        [Test]
        public void DeleteStudentAsync_InvalidInput_ShouldThrowNotFoundException()
        {
            _mockRepository.Setup(repo => repo.Delete(It.IsAny<Student>()));
            _mockUoW.Setup(uow => uow.Students).Returns(_mockRepository.Object);

            Assert.ThrowsAsync<NotFoundException>(async () => await _studentService.DeleteStudentAsync(It.IsAny<int>()));
        }

        [Test]
        public void DeleteStudentAsync_InvalidInput_ShouldThrowArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _studentService.DeleteStudentAsync(null));
        }

        [Test]
        public async Task UpdateStudentAsync_ValidInput()
        {
            var student = new StudentFullDTO()
            {
                Id = 1,
                Name = "Test",
                Surname = "Test"
            };

            _mockRepository.Setup(repo => repo.Update(It.IsAny<Student>()));
            _mockUoW.Setup(uow => uow.Students).Returns(_mockRepository.Object);

            await _studentService.UpdateStudentAsync(student);

            _mockRepository.Verify(repo => repo.Update(It.IsAny<Student>()), Times.Once);
            _mockUoW.Verify(uow => uow.SaveAsync(), Times.Once);
        }

        [Test]
        public void UpdateStudentAsync_InvalidInput_ShouldThrowArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _studentService.UpdateStudentAsync(null));
        }

        [Test]
        public async Task DeleteFromGroup_ValidInput()
        {
            var student = new Student()
            {
                Id = 1,
                Name = "Test",
                Surname = "Test"
            };

            _mockRepository.Setup(repo => repo.Update(It.IsAny<Student>()));
            _mockRepository.Setup(repo => repo.GetByIdAsync(student.Id).Result).Returns(student);
            _mockUoW.Setup(uow => uow.Students).Returns(_mockRepository.Object);

            await _studentService.DeleteFromGroupAsync(student.Id);

            _mockRepository.Verify(repo => repo.GetByIdAsync(It.IsAny<int>()), Times.Once);
            _mockRepository.Verify(repo => repo.Update(It.IsAny<Student>()), Times.Once);
            _mockUoW.Verify(uow => uow.SaveAsync(), Times.Once);
        }

        [Test]
        public void DeleteFromGroup_InvalidInput_ShouldThrowNotFoundException()
        {
            _mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()).Result);
            _mockUoW.Setup(uow => uow.Students).Returns(_mockRepository.Object);

            Assert.ThrowsAsync<NotFoundException>(async () => await _studentService.DeleteFromGroupAsync(It.IsAny<int>()));
        }

        [Test]
        public void DeleteFromGroup_InvalidInput_ArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _studentService.DeleteFromGroupAsync(null));
        }
    }
}
