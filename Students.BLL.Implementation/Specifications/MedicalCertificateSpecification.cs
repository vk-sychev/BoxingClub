using BoxingClub.Infrastructure.Constants.SpecRules;
using BoxingClub.Infrastructure.Enums;
using Students.BLL.DomainEntities;
using Students.BLL.Interfaces.Specifications;
using ArgumentNullException = BoxingClub.Infrastructure.Exceptions.ArgumentNullException;

namespace Students.BLL.Implementation.Specifications
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

        public bool Validate(StudentFullDTO student, TournamentDTO tournament)
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
