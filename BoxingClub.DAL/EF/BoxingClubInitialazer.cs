using BoxingClub.DAL.Entities;
using BoxingClub.Infrastructure.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace BoxingClub.DAL.EF
{
    public static class BoxingClubInitialazer
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
           //adding tournaments
            var moscowJuniorBoxingChampionship = new Tournament()
            {
                Id = 1,
                Name = "Moscow boxing championship",
                Country = "Russia",
                City = "Moscow",
                Date = new DateTime(2021, 06, 25),
                IsMedCertificateRequired = false
            };

            var voronezhBoxingLeague = new Tournament()
            {
                Id = 2,
                Name = "Voronezh Boxing League",
                Country = "Russia",
                City = "Voronezh",
                Date = new DateTime(2021, 08, 10),
                IsMedCertificateRequired = false
            };

            var internationalBoxingCompetition = new Tournament()
            {
                Id = 3,
                Name = "International Boxing Competition",
                Country = "Belarus",
                City = "Gomel",
                Date = new DateTime(2021, 07, 13),
                IsMedCertificateRequired = true
            };

            var internationalBoxingTournamentCupOfTheGovernorOfStPetersburg = new Tournament()
            {
                Id = 4,
                Name = "International boxing tournament - Cup of the Governor of St. Petersburg",
                Country = "Russia",
                City = "St. Petersburg",
                Date = new DateTime(2021, 10, 17),
                IsMedCertificateRequired = false
            };


            modelBuilder.Entity<Tournament>().HasData(moscowJuniorBoxingChampionship, voronezhBoxingLeague,
                                                      internationalBoxingCompetition,
                                                      internationalBoxingTournamentCupOfTheGovernorOfStPetersburg);
        }
    }
}
