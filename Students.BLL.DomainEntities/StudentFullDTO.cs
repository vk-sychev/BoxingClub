using System;
using System.Collections.Generic;
using BoxingClub.Infrastructure.Enums;
using Itenso.TimePeriod;

namespace Students.BLL.DomainEntities
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

        public List<TournamentDTO> Tournaments { get; set; } = new List<TournamentDTO>();
        
        public int GetStudentTrainingPeriod()
        {
            return new DateDiff(DateOfEntry, DateTime.Today).Years;
        }

        public int GetStudentTrainingPeriod(DateTime tournamentDate)
        {
            return new DateDiff(DateOfEntry, tournamentDate).Years;
        }

        public int GetStudentAge()
        {
            return new DateDiff(BornDate, DateTime.Today).Years;
        }

        public int GetMedicalCertificateDuration()
        {
            return new DateDiff(LastMedicalCertificate.DateOfIssue, DateTime.Today).Months;
        }

        public int GetMedicalCertificateDuration(DateTime tournamentDate)
        {
            return new DateDiff(LastMedicalCertificate.DateOfIssue, tournamentDate).Months;
        }
    }
}
