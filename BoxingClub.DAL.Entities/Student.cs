﻿using BoxingClub.Infrastructure.Enums;
using System;
using System.Collections.Generic;

namespace BoxingClub.DAL.Entities
{
    public class Student
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public DateTime BornDate { get; set; }

        public int NumberOfFights { get; set; }

        public DateTime DateOfEntry { get; set; }

        public int Height { get; set; }

        public double Weight { get; set; }

        public Gender Gender { get; set; }

        public int? BoxingGroupId { get; set; }

        public BoxingGroup BoxingGroup { get; set; }

        public List<MedicalCertificate> MedicalCertificates { get; set; }

        public List<TournamentRequest> TournamentRequests { get; set; } = new List<TournamentRequest>();

        public List<Tournament> Tournaments { get; set; } = new List<Tournament>();
    }
}
