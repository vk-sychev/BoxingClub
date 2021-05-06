using BoxingClub.BLL.DomainEntities;
using BoxingClub.BLL.Interfaces.Specifications;
using System;
using BoxingClub.BLL.Implementation.Specifications.SpecRules;
using BoxingClub.Infrastructure.Exceptions;
using ArgumentNullException = BoxingClub.Infrastructure.Exceptions.ArgumentNullException;

namespace BoxingClub.BLL.Implementation.Specifications
{
    public class FighterExperienceSpecification : IStudentSpecification
    {
        private static readonly int TrainingPeriod = FighterExperienceConstants.TrainingPeriod;
        private static readonly int NumberOfFights = FighterExperienceConstants.NumberOfFights;

        public bool IsValid(StudentFullDTO student)
        {
            if (student == null)
            {
                throw new ArgumentNullException(nameof(student), "Student is null");
            }
            //тест 4 кейса
            var diff = GetStudentTrainingPerod(student.DateOfEntry);

            var durationRule = diff >= TrainingPeriod;
            var fightsRule = student.NumberOfFights >= NumberOfFights;

            return durationRule && fightsRule;
        }

        private int GetStudentTrainingPerod(DateTime dateOfEntry)
        {
            var dateOfEntryYear = dateOfEntry.Year;
            var currentYear = DateTime.Now.Year;
            var diff = currentYear - dateOfEntryYear;
            return diff;
        }
    }
}
