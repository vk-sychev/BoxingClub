using BoxingClub.DAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingClub.DAL.Entities
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int AgeCategoryId { get; set; }

        public AgeCategory AgeCategory { get; set; }

        public int WeightCategoryId { get; set; }

        public WeightCategory WeightCategory { get; set; }

        public int TournamentId { get; set; }

        public Tournament Tournament { get; set; }

        public Gender Gender { get; set; }
    }
}
