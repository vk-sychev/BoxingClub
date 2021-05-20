using System;

namespace BoxingClub.BLL.DomainEntities
{
    public class TournamentLiteDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public DateTime Date { get; set; }

        public bool IsMedCertificateNecessary { get; set; }
    }
}
