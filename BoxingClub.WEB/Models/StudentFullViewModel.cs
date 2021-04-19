using System;
using System.ComponentModel.DataAnnotations;

namespace BoxingClub.WEB.Models
{
    public class StudentFullViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        [DataType(DataType.Date)]
        public DateTime BornDate { get; set; }

        public int Height { get; set; }

        public double Weight { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfEntry { get; set; }

        public int BoxingGroupId { get; set; }

        public BoxingGroupLiteViewModel BoxingGroup { get; set; }
    }
}
