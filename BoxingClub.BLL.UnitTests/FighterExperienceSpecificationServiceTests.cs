using BoxingClub.BLL.DomainEntities;
using BoxingClub.BLL.Implementation.Specifications;
using BoxingClub.BLL.Interfaces.Specifications;
using NUnit.Framework;
using System;
using ArgumentNullException = BoxingClub.Infrastructure.Exceptions.ArgumentNullException;

namespace BoxingClub.BLL.UnitTests
{
    [TestFixture]
    class FighterExperienceSpecificationServiceTests
    {
        private IStudentSpecification _studentSpecification;

        private static readonly object[] students =
        {
            new object[] { true, new StudentFullDTO { Id = 1, NumberOfFights = 5, DateOfEntry = new DateTime(2015, 10, 25) } },
            new object[] { false, new StudentFullDTO { Id = 2, NumberOfFights = 1, DateOfEntry = new DateTime(2020, 5, 13) } },
            new object[] { false, new StudentFullDTO { Id = 3, NumberOfFights = 2, DateOfEntry = new DateTime(2010, 3, 17) } },
            new object[] { false, new StudentFullDTO { Id = 4, NumberOfFights = 10, DateOfEntry = new DateTime(2019, 10, 10) } },
        };

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _studentSpecification = new FighterExperienceSpecification();
        }

        [Test]
        [TestCaseSource(nameof(students))]
        public void IsValid_ValidInput(bool result, StudentFullDTO student) 
        {
            var resultFromService = _studentSpecification.Validate(student);

            Assert.IsNotNull(result);
            Assert.AreEqual(result, resultFromService);
        }

        [Test]
        public void IsValid_InvalidInput_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _studentSpecification.Validate(null));
        }
    }
}
