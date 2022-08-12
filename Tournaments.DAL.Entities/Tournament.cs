using System;
using System.Collections.Generic;

namespace Tournaments.DAL.Entities
{
    public class Tournament
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public DateTime Date { get; set; }

        public bool IsMedCertificateRequired { get; set; }

        public List<TournamentRequest> TournamentRequests { get; set; } = new List<TournamentRequest>();
    }
}
