using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoxingClub.DAL.Entities
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public DateTime BornDate { get; set; }

        public int Height { get; set; }

        public double Weight { get; set; }

        public DateTime DateOfEntry { get; set; }

        public int? BoxingGroupId { get; set; }

        public BoxingGroup BoxingGroup { get; set; }
    }
}
