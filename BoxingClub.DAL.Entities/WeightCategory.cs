using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingClub.DAL.Entities
{
    public class WeightCategory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int StartWeight { get; set; }

        public int? EndWeight { get; set; }
    }
}
