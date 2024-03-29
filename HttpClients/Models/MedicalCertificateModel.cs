﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BoxingClub.Infrastructure.Enums;

namespace HttpClients.Models
{
    public class MedicalCertificateModel
    {
        public int Id { get; set; }

        [DisplayName("Clinic Name")]
        public string ClinicName { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Date Of Issue")]
        public DateTime DateOfIssue { get; set; }

        public MedicalResult Result { get; set; }

        public int StudentId { get; set; }
    }
}
