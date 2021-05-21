using BoxingClub.Infrastructure.Enums;
using System.Collections.Generic;

namespace BoxingClub.DAL.Entities
{
    public class Category
    {
        public int Id { get; set; }

        public int? AgeWeightCategoryId { get; set; }

        public AgeWeightCategory AgeWeightCategory { get; set; }

        public Gender Gender { get; set; }

        public List<Tournament> Tournaments { get; set; } = new List<Tournament>();

        public List<TournamentRequirement> TournamentRequirements { get; set; } = new List<TournamentRequirement>();
    }
}
