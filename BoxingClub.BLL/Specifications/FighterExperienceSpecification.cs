using BoxingClub.BLL.DomainEntities;
using BoxingClub.BLL.Interfaces.Specifications;
using System;
using BoxingClub.BLL.Implementation.Specifications.SpecRules;
using BoxingClub.Infrastructure.Exceptions;
using ArgumentNullException = BoxingClub.Infrastructure.Exceptions.ArgumentNullException;
using BoxingClub.DAL.Entities;

namespace BoxingClub.BLL.Implementation.Specifications
{
    public class FighterExperienceSpecification : IStudentSpecification
    {
        private static readonly int TrainingPeriodYears = FighterExperienceConstants.TrainingPeriodYears;
        private static readonly int NumberOfFights = FighterExperienceConstants.NumberOfFights;

        public bool Validate(StudentFullDTO student)
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

        public bool Validate(StudentFullDTO student, Tournament tournament)
        {
            if (tournament == null)
            {
                throw new ArgumentNullException(nameof(tournament), "tournament is null");
            }

            if (student == null)
            {
                throw new ArgumentNullException(nameof(student), "Student is null");
            }

            var diff = student.GetStudentTrainingPeriod(tournament.Date);

            var durationRule = diff >= TrainingPeriodYears;
            var fightsRule = student.NumberOfFights >= NumberOfFights;

            return durationRule && fightsRule;
        }
    }
}
