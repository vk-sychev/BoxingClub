using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingClub.BLL.DomainEntities
{
    public class TournamentRequestDTO
    {
        public int Id { get; set; }

        public TournamentDTO Tournament { get; set; }

        public int TournamentId { get; set; }

        public StudentFullDTO Student { get; set; }

        public int StudentId { get; set; }

        public int StudentWeight { get; set; }

        public int StudentHeight { get; set; }
    }
}
