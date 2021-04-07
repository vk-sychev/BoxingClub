using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingClub.DAL.Entities
{
    public class BoxingGroup
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Coach Coach { get; set; }

        public List<Student> Students { get; set; } = new List<Student>();
    }
}
