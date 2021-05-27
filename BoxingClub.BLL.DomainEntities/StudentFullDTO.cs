using BoxingClub.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using Itenso.TimePeriod;

namespace BoxingClub.BLL.DomainEntities
{
    public class StudentFullDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public DateTime BornDate { get; set; }

        public int NumberOfFights { get; set; }

        public bool Experienced { get; set; }

        public DateTime DateOfEntry { get; set; }

        public int Height { get; set; }

        public double Weight { get; set; }

        public Gender Gender { get; set; }

        public int BoxingGroupId { get; set; }

        public BoxingGroupDTO BoxingGroup { get; set; }

        public List<MedicalCertificateDTO> MedicalCertificates { get; set; } = new List<MedicalCertificateDTO>();

        public MedicalCertificateDTO LastMedicalCertificate { get; set; }

        public bool IsMedicalCertificateValid { get; set; }
        
        public int GetStudentTrainingPeriod()
        {
            return new DateDiff(DateOfEntry, DateTime.Today).Years;
        }

        public int GetStudentAge()
        {
            return new DateDiff(BornDate, DateTime.Today).Years;
        }
    }
}
