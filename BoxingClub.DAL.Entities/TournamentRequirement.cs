using BoxingClub.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingClub.DAL.Entities
{
    public class TournamentRequirement
    {
        public int Id { get; set; }

        public int? CategoryId { get; set; }

        public Category Category { get; set; }

        public int? TournamentId { get; set; }

        public Tournament Tournament { get; set; }
    }
}
