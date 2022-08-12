using BoxingClub.BLL.Implementation.Specifications.SpecRules;
using Students.BLL.DomainEntities;
using Students.BLL.Interfaces.Specifications;
using ArgumentNullException = BoxingClub.Infrastructure.Exceptions.ArgumentNullException;

namespace Students.BLL.Implementation.Specifications
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

        public bool Validate(StudentFullDTO student, TournamentDTO tournament)
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
