using System;
using System.Linq;
using BoxingClub.Infrastructure.Constants.SpecRules;
using Itenso.TimePeriod;
using Students.BLL.DomainEntities;
using Students.BLL.Interfaces.Specifications;
using ArgumentNullException = BoxingClub.Infrastructure.Exceptions.ArgumentNullException;

namespace Students.BLL.Implementation.Specifications
{
    class CompetitionSpecification : IStudentSpecification
    {
        private readonly int _duration = TournamentConstants.DurationBetweenTournamentsDays;

        public bool Validate(StudentFullDTO student, TournamentDTO tournament)
        {
            if (tournament == null)
            {
                throw new ArgumentNullException(nameof(tournament), "tournament is null");
            }

            if (student == null)
            {
                throw new ArgumentNullException(nameof(student), "student is null");
            }


            if (!student.Tournaments.Any())
            {
                return true;
            }

            foreach (var tour in student.Tournaments)
            {
                if (!(Math.Abs(new DateDiff(tour.Date, tournament.Date).Days) >= _duration))
                {
                    return false;
                }
            }

            return true;
        }

        public bool Validate(StudentFullDTO student)
        {
            if (student == null)
            {
                throw new ArgumentNullException(nameof(student), "student is null");
            }

            return true;
        }
    }
}
