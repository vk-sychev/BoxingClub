using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingClub.BLL.DomainEntities
{
    public class MedicalCertificateDTO
    {
        public int Id { get; set; }

        public string ClinicName { get; set; }

        public DateTime DateOfIssue { get; set; }

        public int Result { get; set; }

        public int StudentId { get; set; }
    }
}
