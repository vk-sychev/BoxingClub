using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BoxingClub.Infrastructure.Enums;

namespace HttpClients.Models
{
    public class StudentFullModel
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

        public Gender Gender { get; set; }

        public List<MedicalCertificateModel> MedicalCertificates { get; set; } = new List<MedicalCertificateModel>();
        
        [DisplayName("MedExamination")]
        public bool IsMedicalCertificateValid { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Date of Entry")]
        public DateTime DateOfEntry { get; set; }

        [DisplayName("Boxing Group")]
        public int BoxingGroupId { get; set; }

        public BoxingGroupLiteModel BoxingGroup { get; set; }
    }
}
