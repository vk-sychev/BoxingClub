using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingClub.DAL.Entities
{
    public class AgeCategory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int StartAge { get; set; }

        public int EndAge { get; set; }
    }
}
