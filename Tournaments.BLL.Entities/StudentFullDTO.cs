using BoxingClub.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using Itenso.TimePeriod;

namespace Tournaments.BLL.Entities
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

        public bool IsMedicalCertificateValid { get; set; }

        public List<TournamentDTO> Tournaments { get; set; } = new List<TournamentDTO>();        
    }
}
