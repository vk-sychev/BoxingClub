using System;
using System.Collections.Generic;

namespace BoxingClub.DAL.Entities
{
    public class Tournament
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public DateTime Date { get; set; }

        public bool IsMedCertificateNecessary { get; set; }

        public List<TournamentRequirement> TournamentRequirements { get; set; } = new List<TournamentRequirement>();

        public List<Category> Categories { get; set; } = new List<Category>();

        public static void UpdateTournamentProperties(Tournament tournamentFromDb, Tournament updatedTournament)
        {
            tournamentFromDb.Name = updatedTournament.Name;
            tournamentFromDb.Date = updatedTournament.Date;
            tournamentFromDb.Country = updatedTournament.Country;
            tournamentFromDb.City = updatedTournament.City;
            tournamentFromDb.IsMedCertificateNecessary = updatedTournament.IsMedCertificateNecessary;
        }
    }
}
