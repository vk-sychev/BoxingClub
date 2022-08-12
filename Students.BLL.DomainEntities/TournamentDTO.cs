using System;

namespace Students.BLL.DomainEntities
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
