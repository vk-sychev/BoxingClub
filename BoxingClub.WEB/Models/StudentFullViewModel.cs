using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BoxingClub.Web.Models
{
    public class StudentFullViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Date of Birth")]
        public DateTime BornDate { get; set; }

        public int Height { get; set; }

        public double Weight { get; set; }

        public int NumberOfFights { get; set; }

        public bool Experienced { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Date of Entry")]
        public DateTime DateOfEntry { get; set; }

        [DisplayName("Boxing Group")]
        public int BoxingGroupId { get; set; }

        public BoxingGroupLiteViewModel BoxingGroup { get; set; }
    }
}
