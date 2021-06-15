using System.Collections.Generic;

namespace BoxingClub.DAL.Entities
{
    public class BoxingGroup
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CoachId { get; set; }

        public List<Student> Students { get; set; } = new List<Student>();
    }
}
