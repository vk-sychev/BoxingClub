using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HttpClients.Models
{
    public class StudentLiteModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public bool Experienced { get; set; }

        public bool IsMedicalCertificateValid { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime BornDate { get; set; }

        [DisplayName("Boxing Group")]
        public int BoxingGroupId { get; set; }

        public BoxingGroupLiteModel BoxingGroup { get; set; }
    }
}
