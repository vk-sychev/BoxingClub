using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingClub.DAL.Entities
{
    public class FighterExperienceSpecification
    {
        public int Id { get; set; }

        public int TrainingPeriod { get; set; }

        public int NumberOfFights { get; set; }
    }
}
