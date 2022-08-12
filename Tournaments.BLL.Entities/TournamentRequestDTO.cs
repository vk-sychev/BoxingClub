using System;
using System.Collections.Generic;
using System.Text;

namespace Tournaments.BLL.Entities
{
    public class TournamentRequestDTO
    {
        public int Id { get; set; }

        public TournamentDTO Tournament { get; set; }

        public int TournamentId { get; set; }

        public StudentFullDTO Student { get; set; }

        public int StudentId { get; set; }

        public double StudentWeight { get; set; }

        public int StudentHeight { get; set; }
    }
}
