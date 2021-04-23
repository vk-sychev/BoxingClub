using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoxingClub.DAL.Entities
{
    public class BoxingGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string CoachId { get; set; }

        public ApplicationUser Coach { get; set; }

        public List<Student> Students { get; set; } = new List<Student>();
    }
}
