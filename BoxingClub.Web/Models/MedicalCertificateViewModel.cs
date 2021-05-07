using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BoxingClub.Web.Models
{
    public class MedicalCertificateViewModel
    {
        public int Id { get; set; }

        [DisplayName("Clinic Name")]
        public string ClinicName { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Date Of Issue")]
        public DateTime DateOfIssue { get; set; }

        public int Result { get; set; }

        public int StudentId { get; set; }
    }
}
