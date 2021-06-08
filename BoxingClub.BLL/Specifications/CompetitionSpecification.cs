using BoxingClub.BLL.DomainEntities;
using BoxingClub.BLL.Interfaces.Specifications;
using BoxingClub.DAL.Entities;
using BoxingClub.Infrastructure.Constants.SpecRules;
using Itenso.TimePeriod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArgumentNullException = BoxingClub.Infrastructure.Exceptions.ArgumentNullException;

namespace BoxingClub.BLL.Implementation.Specifications
{
    class CompetitionSpecification : IStudentSpecification
    {
        private readonly int _duration = TournamentConstants.DurationBetweenTournamentsDays;

        public bool Validate(StudentFullDTO student, Tournament tournament)
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
