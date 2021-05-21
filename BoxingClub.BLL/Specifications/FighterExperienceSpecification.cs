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
        private static readonly int TrainingPeriodYears = FighterExperienceConstants.TrainingPeriodYears;
        private static readonly int NumberOfFights = FighterExperienceConstants.NumberOfFights;

        public bool IsValid(StudentFullDTO student)
        {
            if (student == null)
            {
                throw new ArgumentNullException(nameof(student), "Student is null");
            }

            var diff = student.GetStudentTrainingPeriod();

            var durationRule = diff >= TrainingPeriodYears;
            var fightsRule = student.NumberOfFights >= NumberOfFights;

            return durationRule && fightsRule;
        }
    }
}
