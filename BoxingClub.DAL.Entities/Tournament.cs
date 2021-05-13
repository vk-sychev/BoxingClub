using System;
using System.Collections.Generic;
using System.Text;

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

        public List<TournamentRequirement> Categories { get; set; } = new List<TournamentRequirement>();
    }
}
