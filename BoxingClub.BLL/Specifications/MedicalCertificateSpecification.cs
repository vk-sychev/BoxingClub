using BoxingClub.BLL.DomainEntities;
using BoxingClub.BLL.Interfaces.Specifications;
using BoxingClub.Infrastructure.Constants.SpecRules;
using Itenso.TimePeriod;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingClub.BLL.Implementation.Specifications
{
    class MedicalCertificateSpecification : IStudentSpecification
    {
        private readonly int ValidityPeriod = MedicalCertificateConstants.ValidityPeriod;

        public bool IsValid(StudentFullDTO student)
        {
            if (student == null)
            {
                throw new ArgumentNullException(nameof(student), "Student is null");
            }

            if (student.LastMedicalCertificate == null)
            {
                return false;
            }

            if (student.LastMedicalCertificate.Result == 0)
            {
                return false;
            }

            return GetMedicalCertificateDuration(student.LastMedicalCertificate.DateOfIssue) < ValidityPeriod; 
        }

        private int GetMedicalCertificateDuration(DateTime dateOfIssue)
        { 
            return new DateDiff(dateOfIssue, DateTime.Today).Months;
        }
    }
}
