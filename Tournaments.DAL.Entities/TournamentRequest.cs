using System;
using System.Collections.Generic;
using System.Text;

namespace Tournaments.DAL.Entities
{
    public class TournamentRequest
    {
        public int Id { get; set; }

        public Tournament Tournament { get; set; }

        public int? TournamentId { get; set; }

        public int? StudentId { get; set; }

        public int StudentWeight { get; set; }

        public int StudentHeight { get; set; } 
    }
}
