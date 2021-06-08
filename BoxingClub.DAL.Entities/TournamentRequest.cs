﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingClub.DAL.Entities
{
    public class TournamentRequest
    {
        public int Id { get; set; }

        public Tournament Tournament { get; set; }

        public int? TournamentId { get; set; }

        public Student Student { get; set; }

        public int? StudentId { get; set; }

        public int StudentWeight { get; set; }

        public int StudentHeight { get; set; } 
    }
}
