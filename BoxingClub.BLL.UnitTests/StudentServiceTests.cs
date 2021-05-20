using AutoMapper;
using BoxingClub.BLL.DomainEntities;
using BoxingClub.BLL.Interfaces;
using BoxingClub.BLL.Services;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using BoxingClub.Web.Mapping;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using BoxingClub.Infrastructure.Exceptions;
using ArgumentNullException = BoxingClub.Infrastructure.Exceptions.ArgumentNullException;
using System.Linq;
using System;
using BoxingClub.Infrastructure.Enums;

namespace BoxingClub.BLL.UnitTests
{
    [TestFixture]
    class StudentServiceTests
    {
        private IMapper _mapper;
        private Mock<IStudentRepository> _mockStudentRepository;
        private Mock<IBoxingGroupRepository> _mockBoxingGroupRepository;
        private Mock<IUnitOfWork> _mockUoW;
        private IStudentService _studentService;
        private static readonly StudentFullDTO _studentDTO = new StudentFullDTO()
        {
            Id = 1,
            Name = "Test",
            Surname = "Test",
            BoxingGroupId = 1
        };

        private static readonly Student _student = new Student()
        {
            Id = 1,
            Name = "Test",
            Surname = "Test"
        };

        private static readonly SearchModelDTO _searchModelWithoutFilters = new SearchModelDTO()
        {
            PageIndex = 1,
            PageSize = 4
        };

        private static readonly SearchModelDTO _searchModelWithExperiencedStudents = new SearchModelDTO()
        {
            PageIndex = 1,
            PageSize = 4,
            ExperienceFilter = (int)ExperienceOrder.Experienced
        };

        private static readonly SearchModelDTO _searchModelWithNewbiesStudents = new SearchModelDTO()
        {
            PageIndex = 1,
            PageSize = 4,
            ExperienceFilter = (int)ExperienceOrder.Newbies
        };

        private static readonly SearchModelDTO _searchModelWithMedExaminatedStudents = new SearchModelDTO()
        {
            PageIndex = 1,
            PageSize = 4,
            MedExaminationFilter = (int)MedExaminationOrder.Successed
        };

        private static readonly SearchModelDTO _searchModelWithMedFailedStudents = new SearchModelDTO()
        {
            PageIndex = 1,
            PageSize = 4,
            MedExaminationFilter = (int)MedExaminationOrder.Failed
        };

        private static readonly SearchModelDTO _searchModelWithExperiencedAndMedExaminatedStudents = new SearchModelDTO()
        {
            PageIndex = 1,
            PageSize = 4,
            ExperienceFilter = (int)ExperienceOrder.Experienced,
            MedExaminationFilter = (int)MedExaminationOrder.Successed
        };

        private static readonly SearchModelDTO _searchModelWithNewbiesAndMedExaminatedStudents = new SearchModelDTO()
        {
            PageIndex = 1,
            PageSize = 4,
            ExperienceFilter = (int)ExperienceOrder.Newbies,
            MedExaminationFilter = (int)MedExaminationOrder.Successed
        };

        private static readonly SearchModelDTO _searchModelWithExperiencedAndMedFailedStudents = new SearchModelDTO()
        {
            PageIndex = 1,
            PageSize = 4,
            ExperienceFilter = (int)ExperienceOrder.Experienced,
            MedExaminationFilter = (int)MedExaminationOrder.Failed
        };

        private static readonly SearchModelDTO _searchModelWithNewbiesAndMedFailedStudents = new SearchModelDTO()
        {
            PageIndex = 1,
            PageSize = 4,
            ExperienceFilter = (int)ExperienceOrder.Newbies,
            MedExaminationFilter = (int)MedExaminationOrder.Failed
        };

        private static readonly object[] CasesForGetStudentsAsync =
        {
            new object[] {_searchModelWithoutFilters, 4},
            new object[] {_searchModelWithExperiencedStudents, 2},
            new object[] {_searchModelWithNewbiesStudents, 2},
            new object[] {_searchModelWithMedExaminatedStudents, 2},
            new object[] {_searchModelWithMedFailedStudents, 2},
            new object[] {_searchModelWithExperiencedAndMedExaminatedStudents, 0},
            new object[] {_searchModelWithNewbiesAndMedExaminatedStudents, 2},
            new object[] {_searchModelWithNewbiesAndMedFailedStudents, 0},
            new object[] {_searchModelWithExperiencedAndMedFailedStudents, 2}
        };

        private static readonly List<MedicalCertificate> MedicalCertificatesForFirstStudent = new List<MedicalCertificate>()
        {
             new MedicalCertificate()
             {
                    Id = 1,
                    ClinicName = "Polyclinic 4",
                    DateOfIssue = new DateTime(2021, 03, 10),
                    Result = MedicalResult.Success,
                    StudentId = 1
             },

             new MedicalCertificate()
             {
                    Id = 2,
                    ClinicName = "Polyclinic 4",
                    DateOfIssue = new DateTime(2020, 05, 02),
                    Result = MedicalResult.Fail,
                    StudentId = 1
             },

             new MedicalCertificate()
             {
                    Id = 3,
                    ClinicName = "Polyclinic 13",
                    DateOfIssue = new DateTime(2019, 10, 04),
                    Result = MedicalResult.Success,
                    StudentId = 1
             }
        };

        private static readonly List<MedicalCertificate> MedicalCertificatesForSecondStudent = new List<MedicalCertificate>()
        {
            new MedicalCertificate()
            {
                    Id = 4,
                    ClinicName = "VODC",
                    DateOfIssue = new DateTime(2018, 07, 14),
                    Result = MedicalResult.Success,
                    StudentId = 2
            },

            new MedicalCertificate()
            {
                    Id = 5,
                    ClinicName = "VODC",
                    DateOfIssue = new DateTime(2021, 04, 14),
                    Result = MedicalResult.Fail,
                    StudentId = 2
            }
        };

        private static readonly List<MedicalCertificate> MedicalCertificatesForThirdStudent = new List<MedicalCertificate>()
        {
             new MedicalCertificate()
             {
                    Id = 6,
                    ClinicName = "Polyclinic 4",
                    DateOfIssue = new DateTime(2020, 12, 06),
                    Result = MedicalResult.Fail,
                    StudentId = 3
             },

             new MedicalCertificate()
             {
                    Id = 7,
                    ClinicName = "Polyclinic 1",
                    DateOfIssue = new DateTime(2021, 05, 04),
                    Result = MedicalResult.Success,
                    StudentId = 3
             }
        };


        private static readonly List<Student> _students = new List<Student>()
        {
            new Student
            {
                    Id = 1,
                    Name = "Vasiliy",
                    Surname = "Sychev",
                    Patronymic = "Konstantinovich",
                    BornDate = new DateTime(2000, 10, 10),
                    DateOfEntry = new DateTime(2017, 2, 20),
                    Height = 175,
                    Weight = 88,
                    BoxingGroupId = 1,
                    NumberOfFights = 3,
                    Gender = Gender.Male,
                    MedicalCertificates = MedicalCertificatesForFirstStudent
            },

            new Student
            {
                    Id = 2,
                    Name = "Igor",
                    Surname = "Zhuravlev",
                    BornDate = new DateTime(1991, 5, 22),
                    DateOfEntry = new DateTime(2014, 1, 15),
                    Height = 180,
                    Weight = 87,
                    BoxingGroupId = 2,
                    NumberOfFights = 5,
                    Gender = Gender.Male,
                    MedicalCertificates = MedicalCertificatesForSecondStudent
            },

            new Student
            {
                    Id = 3,
                    Name = "Ivan",
                    Surname = "Pavlov",
                    BornDate = new DateTime(2001, 10, 14),
                    DateOfEntry = new DateTime(2019, 02, 28),
                    Height = 175,
                    Weight = 81,
                    BoxingGroupId = 1,
                    NumberOfFights = 2,
                    Gender = Gender.Male,
                    MedicalCertificates = MedicalCertificatesForThirdStudent
            },

            new Student
            {
                    Id = 4,
                    Name = "Andrew",
                    Surname = "Solovyev",
                    Patronymic = "Sergeevich",
                    BornDate = new DateTime(2000, 04, 03),
                    DateOfEntry = new DateTime(2015, 02, 02),
                    Height = 176,
                    Weight = 73,
                    NumberOfFights = 10,
                    Gender = Gender.Male
            }

        };



        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var mapperProfiles = new List<Profile>() { new StudentProfile(), new MedicalCertificateProfile() };
            var mapperConfig = new MapperConfiguration(mc => mc.AddProfiles(mapperProfiles));
            _mapper = mapperConfig.CreateMapper();
        }

        [SetUp]
        public void SetUp()
        {
            _mockStudentRepository = new Mock<IStudentRepository>();
            _mockUoW = new Mock<IUnitOfWork>();
            _studentService = new StudentService(_mockUoW.Object, _mapper);
            _mockBoxingGroupRepository = new Mock<IBoxingGroupRepository>();
        }

        [Test]
        public async Task CreateStudent_ValidInput()
        {
            _mockStudentRepository.Setup(repo => repo.CreateAsync(It.IsAny<Student>()));
            _mockBoxingGroupRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()).Result).Returns(new BoxingGroup { Id = 1 });
            _mockUoW.Setup(uow => uow.Students).Returns(_mockStudentRepository.Object);
            _mockUoW.Setup(uow => uow.BoxingGroups).Returns(_mockBoxingGroupRepository.Object);

            await _studentService.CreateStudentAsync(_studentDTO);

            _mockStudentRepository.Verify(repo => repo.CreateAsync(It.IsAny<Student>()), Times.Once);
            _mockBoxingGroupRepository.Verify(repo => repo.GetByIdAsync(It.IsAny<int>()), Times.Once);
            _mockUoW.Verify(uow => uow.SaveAsync());
        }

        [Test]
        public void CreateStudentAsync_InputNull_ShouldThrowArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _studentService.CreateStudentAsync(null));
        }

        [Test]
        [TestCaseSource(nameof(CasesForGetStudentsAsync))]
        public async Task GetStudentsAsync_ValidInput_ReturnList(SearchModelDTO searchModel, int countOfValidStudents)
        {
            _mockStudentRepository.Setup(repo => repo.GetAllAsync().Result).Returns(_students);
            _mockUoW.Setup(uow => uow.Students).Returns(_mockStudentRepository.Object);

            var pageModel = await _studentService.GetStudentsAsync(searchModel);

            _mockStudentRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
            Assert.AreEqual(countOfValidStudents, pageModel.Items.Count());
        }


        [Test]
        public void GetStudentsAsync_InvalidInput_ShouldThrowArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _studentService.GetStudentsAsync(null));
        }


        [Test]
        public async Task GetStudentByIdAsync_ValidInput()
        {
            _mockStudentRepository.Setup(repo => repo.GetByIdAsync(_student.Id).Result).Returns(_student);
            _mockUoW.Setup(uow => uow.Students).Returns(_mockStudentRepository.Object);

            var student = await _studentService.GetStudentByIdAsync(_student.Id);

            _mockStudentRepository.Verify(repo => repo.GetByIdAsync(It.IsAny<int>()), Times.Once);
            Assert.IsNotNull(student);
            Assert.AreEqual(_student.Id, student.Id);
        }

        [Test]
        public void GetStudentByIdAsync_InputIsNull_ShouldThrowArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _studentService.GetStudentByIdAsync(null));
        }

        [Test]
        public void GetStudentByIdAsync_InvalidInput_ShouldThrowNotFoundException()
        {
            _mockStudentRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()).Result);
            _mockUoW.Setup(uow => uow.Students).Returns(_mockStudentRepository.Object);

            Assert.ThrowsAsync<NotFoundException>(async () => await _studentService.GetStudentByIdAsync(It.IsAny<int>()));
        }


        [Test]
        public async Task DeleteStudentAsync_ValidInput()
        {
            _mockStudentRepository.Setup(repo => repo.GetByIdAsync(_student.Id).Result).Returns(_student);
            _mockStudentRepository.Setup(repo => repo.Delete(It.IsAny<Student>()));
            _mockUoW.Setup(uow => uow.Students).Returns(_mockStudentRepository.Object);

            await _studentService.DeleteStudentAsync(_student.Id);

            _mockStudentRepository.Verify(repo => repo.GetByIdAsync(_student.Id), Times.Once);
            _mockStudentRepository.Verify(repo => repo.Delete(It.IsAny<Student>()), Times.Once);
            _mockUoW.Verify(uow => uow.SaveAsync(), Times.Once);
        }


        [Test]
        public void DeleteStudentAsync_InvalidInput_ShouldThrowNotFoundException()
        {
            _mockStudentRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()).Result);
            _mockUoW.Setup(uow => uow.Students).Returns(_mockStudentRepository.Object);

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
            _mockStudentRepository.Setup(repo => repo.Update(It.IsAny<Student>()));
            _mockUoW.Setup(uow => uow.Students).Returns(_mockStudentRepository.Object);

            await _studentService.UpdateStudentAsync(_studentDTO);

            _mockStudentRepository.Verify(repo => repo.Update(It.IsAny<Student>()), Times.Once);
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
            _mockStudentRepository.Setup(repo => repo.Update(It.IsAny<Student>()));
            _mockStudentRepository.Setup(repo => repo.GetByIdAsync(_student.Id).Result).Returns(_student);
            _mockUoW.Setup(uow => uow.Students).Returns(_mockStudentRepository.Object);

            await _studentService.DeleteFromGroupAsync(_student.Id);

            _mockStudentRepository.Verify(repo => repo.GetByIdAsync(It.IsAny<int>()), Times.Once);
            _mockStudentRepository.Verify(repo => repo.Update(It.IsAny<Student>()), Times.Once);
            _mockUoW.Verify(uow => uow.SaveAsync(), Times.Once);
        }

        [Test]
        public void DeleteFromGroup_InvalidInput_ShouldThrowNotFoundException()
        {
            _mockStudentRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()).Result);
            _mockUoW.Setup(uow => uow.Students).Returns(_mockStudentRepository.Object);

            Assert.ThrowsAsync<NotFoundException>(async () => await _studentService.DeleteFromGroupAsync(It.IsAny<int>()));
        }

        [Test]
        public void DeleteFromGroup_InvalidInput_ArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _studentService.DeleteFromGroupAsync(null));
        }
    }
}
