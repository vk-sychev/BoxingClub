using BoxingClub.BLL.DomainEntities;
using BoxingClub.BLL.Implementation.Specifications;
using BoxingClub.BLL.Interfaces.Specifications;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingClub.BLL.UnitTests
{
    [TestFixture]
    class FighterExperienceSpecificationServiceTests
    {
        private IStudentSpecification _studentSpecification;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _studentSpecification = new FighterExperienceSpecificationService();
        }

        private static readonly object[] Students =
        {
            new object[] { true, new StudentFullDTO { Id = 1, NumberOfFights = 5, DateOfEntry = new DateTime(2015, 10, 25) } },
            new object[] { false, new StudentFullDTO { Id = 2, NumberOfFights = 1, DateOfEntry = new DateTime(2020, 5, 13) } }
        };


        [Test]
        [TestCaseSource(nameof(Students))]
        public void IsValid_ValidInput(bool result, StudentFullDTO student)
        {
            var resultFromService = _studentSpecification.IsValid(student);

            Assert.IsNotNull(result);
            Assert.AreEqual(result, resultFromService);
        }
    }
}
