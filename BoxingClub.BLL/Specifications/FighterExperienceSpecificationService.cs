using BoxingClub.BLL.DomainEntities;
using BoxingClub.BLL.Interfaces.Specifications;
using System;
using BoxingClub.BLL.Implementation.Specifications.SpecRules;

namespace BoxingClub.BLL.Implementation.Specifications
{
    public class FighterExperienceSpecificationService : IStudentSpecification
    {
        private static readonly int TrainingPeriod = FighterExperienceRule.TrainingPeriod;
        private static readonly int NumberOfFights = FighterExperienceRule.NumberOfFights;

        public bool IsValid(StudentFullDTO student)
        {
            var diff = GetStudentTrainingPerod(student.DateOfEntry);

            var durationRule = diff >= TrainingPeriod;
            var fightsRule = student.NumberOfFights >= NumberOfFights;

            var result = durationRule && fightsRule;

            return result;
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
