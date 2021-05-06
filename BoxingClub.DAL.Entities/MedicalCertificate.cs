using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingClub.DAL.Entities
{
    public class MedicalCertificate
    {
        public int Id { get; set; }
            
        public string ClinicName { get; set; }

        public DateTime DateOfIssue { get; set; }

        public bool Result { get; set; } //enum

        public int StudentId { get; set; }

        public Student Student { get; set; }
    }
}
