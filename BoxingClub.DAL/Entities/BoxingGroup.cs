using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BoxingClub.DAL.Entities
{
    public class BoxingGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public int CoachId { get; set; }

        public Coach Coach { get; set; }

        public List<Student> Students { get; set; } = new List<Student>();
    }
}
