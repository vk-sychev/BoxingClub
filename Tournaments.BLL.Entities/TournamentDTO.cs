using System;
using System.Collections.Generic;

namespace Tournaments.BLL.Entities
{
    public class TournamentDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public DateTime Date { get; set; }

        public bool IsMedCertificateRequired { get; set; }
    }
}
