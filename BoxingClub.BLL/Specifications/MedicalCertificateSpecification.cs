using BoxingClub.BLL.DomainEntities;
using BoxingClub.BLL.Interfaces.Specifications;
using BoxingClub.DAL.Entities;
using BoxingClub.Infrastructure.Constants.SpecRules;
using BoxingClub.Infrastructure.Enums;
using Itenso.TimePeriod;
using System;
using System.Collections.Generic;
using System.Text;
using ArgumentNullException = BoxingClub.Infrastructure.Exceptions.ArgumentNullException;

namespace BoxingClub.BLL.Implementation.Specifications
{
    class MedicalCertificateSpecification : IStudentSpecification
    {
        private readonly int ValidityPeriodMonthes = MedicalCertificateConstants.ValidityPeriodMonthes;

        public bool Validate(StudentFullDTO student)
        {
            if (student == null)
            {
                throw new ArgumentNullException(nameof(student), "Student is null");
            }

            if (student.LastMedicalCertificate == null)
            {
                return false;
            }

            if (student.LastMedicalCertificate.Result == MedicalResult.Fail)
            {
                return false;
            }

            return student.GetMedicalCertificateDuration() < ValidityPeriodMonthes; 
        }

        public bool Validate(StudentFullDTO student, Tournament tournament)
        {
            if (tournament == null)
            {
                throw new ArgumentNullException(nameof(tournament), "tournament is null");
            }

            if (student == null)
            {
                throw new ArgumentNullException(nameof(student), "Student is null");
            }

            if (student.LastMedicalCertificate == null)
            {
                return false;
            }

            if (student.LastMedicalCertificate.Result == MedicalResult.Fail)
            {
                return false;
            }

            return student.GetMedicalCertificateDuration(tournament.Date) < ValidityPeriodMonthes;
        }
    }
}
