using BoxingClub.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingClub.BLL.DomainEntities
{
    public class CategoryDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int AgeCategoryId { get; set; }

        public AgeCategoryDTO AgeCategory { get; set; }

        public int WeightCategoryId { get; set; }

        public WeightCategoryDTO WeightCategory { get; set; }

        public int TournamentId { get; set; }

        public TournamentDTO Tournament { get; set; }

        public Gender Gender { get; set; }
    }
}
