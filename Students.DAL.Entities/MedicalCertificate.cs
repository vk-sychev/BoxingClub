using BoxingClub.Infrastructure.Enums;
using System;

namespace Students.DAL.Entities
{
    public class MedicalCertificate
    {
        public int Id { get; set; }
            
        public string ClinicName { get; set; }

        public DateTime DateOfIssue { get; set; }

        public MedicalResult Result { get; set; } 

        public int StudentId { get; set; }

        public Student Student { get; set; }
    }
}
