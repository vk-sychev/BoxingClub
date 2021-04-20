using System;
using System.ComponentModel.DataAnnotations;

namespace BoxingClub.WEB.Models
{
    public class StudentLiteViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime BornDate { get; set; }

        public BoxingGroupLiteViewModel BoxingGroup { get; set; }
    }
}
