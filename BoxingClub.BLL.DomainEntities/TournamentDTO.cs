using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingClub.BLL.DomainEntities
{
    public class TournamentDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public DateTime Date { get; set; }

        public bool IsMedCertificateNecessary { get; set; }

        public List<CategoryDTO> Categories { get; set; } = new List<CategoryDTO>();

        public List<StudentFullDTO> Students { get; set; } = new List<StudentFullDTO>();
    }
}
