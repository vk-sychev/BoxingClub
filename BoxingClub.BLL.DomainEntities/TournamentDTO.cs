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

        public List<AgeCategoryDTO> AgeCategories { get; set; } = new List<AgeCategoryDTO>();

        public List<WeightCategoryDTO> WeightCategories { get; set; } = new List<WeightCategoryDTO>();

        public List<StudentFullDTO> Students { get; set; } = new List<StudentFullDTO>();
    }
}
