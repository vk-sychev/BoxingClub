using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BoxingClub.DAL.Entities
{
    public class Student
    {
/*        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]*/
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public DateTime BornDate { get; set; }

        public int Height { get; set; }

        public double Weight { get; set; }

        public DateTime DateOfEntry { get; set; }
    }
}
