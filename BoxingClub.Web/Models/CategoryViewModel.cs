using BoxingClub.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxingClub.Web.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int AgeCategoryId { get; set; }

        public AgeCategoryViewModel AgeCategory { get; set; }

        public int WeightCategoryId { get; set; }

        public WeightCategoryViewModel WeightCategory { get; set; }

        public int TournamentId { get; set; }

        public TournamentViewModel Tournament { get; set; }

        public Gender Gender { get; set; }
    }
}
