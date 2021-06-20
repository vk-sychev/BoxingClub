using BoxingClub.Infrastructure.Enums;
using System;

namespace Tournaments.BLL.Entities
{
    public class MedicalCertificateDTO
    {
        public int Id { get; set; }

        public string ClinicName { get; set; }

        public DateTime DateOfIssue { get; set; }

        public MedicalResult Result { get; set; }

        public int StudentId { get; set; }
    }
}
