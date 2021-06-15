using BoxingClub.DAL.Entities;
using BoxingClub.Infrastructure.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace BoxingClub.DAL.EF
{
    public static class BoxingClubInitialazer
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
           var coach1Id = "19759de3-ce1d-4cfd-8340-4e64eb245eb4";
            var coach2Id = "060342c3-9dc3-4597-bae1-9f19c991ebe9";
            var coach3Id = "a50a06a5-df07-4728-b6a0-93173c2ce4cf";

            modelBuilder.Entity<BoxingGroup>().HasData(
                new BoxingGroup
                {
                    Id = 1,
                    CoachId = coach1Id,
                    Name = "Vityaz"
                },

                new BoxingGroup
                {
                    Id = 2,
                    CoachId = coach1Id,
                    Name = "Warrior"
                },

                new BoxingGroup
                {
                    Id = 3,
                    CoachId = coach2Id,
                    Name = "Sarmat"
                });

            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    Id = 1,
                    Name = "Vasiliy",
                    Surname = "Sychev",
                    Patronymic = "Konstantinovich",
                    BornDate = new DateTime(2000, 10, 10),
                    DateOfEntry = new DateTime(2015, 2, 20),
                    Height = 175,
                    Weight = 88,
                    BoxingGroupId = 1,
                    NumberOfFights = 3,
                    Gender = Gender.Male
                },

                new Student
                {
                    Id = 2,
                    Name = "Igor",
                    Surname = "Zhuravlev",
                    Height = 183,
                    Weight = 70,
                    BornDate = new DateTime(1991, 5, 22),
                    DateOfEntry = new DateTime(2014, 1, 15),
                    BoxingGroupId = 2,
                    NumberOfFights = 5,
                    Gender = Gender.Male
                },

                new Student
                {
                    Id = 3,
                    Name = "Ivan",
                    Surname = "Pavlov",
                    Height = 170,
                    Weight = 66,
                    BornDate = new DateTime(2001, 10, 14),
                    DateOfEntry = new DateTime(2018, 02, 28),
                    BoxingGroupId = 1,
                    NumberOfFights = 2,
                    Gender = Gender.Male
                },

                new Student
                {
                    Id = 4,
                    Name = "Andrew",
                    Surname = "Solovyev",
                    Patronymic = "Sergeevich",
                    Height = 174,
                    Weight = 72,
                    BornDate = new DateTime(2000, 04, 03),
                    DateOfEntry = new DateTime(2013, 02, 02),
                    NumberOfFights = 10,
                    BoxingGroupId = 2,
                    Gender = Gender.Male
                },

                new Student
                {
                    Id = 5,
                    Name = "Vika",
                    Surname = "Zhukova",
                    Height = 165,
                    Weight = 55,
                    BornDate = new DateTime(1998, 01, 25),
                    DateOfEntry = new DateTime(2018, 05, 08),
                    BoxingGroupId = 3,
                    NumberOfFights = 4,
                    Gender = Gender.Female
                },

                new Student
                {
                    Id = 6,
                    Name = "Ivan",
                    Surname = "Shabanov",
                    Height = 172,
                    Weight = 66,
                    BornDate = new DateTime(2001, 04, 30),
                    DateOfEntry = new DateTime(2018, 05, 25),
                    BoxingGroupId = 2,
                    NumberOfFights = 3,
                    Gender = Gender.Male
                },

                new Student
                {
                    Id = 7,
                    Name = "Vlad",
                    Surname = "Safonov",
                    Patronymic = "Sergeevich",
                    Height = 175,
                    Weight = 74,
                    BornDate = new DateTime(2000, 10, 09),
                    DateOfEntry = new DateTime(2020, 06, 16),
                    BoxingGroupId = 3,
                    NumberOfFights = 1,
                    Gender = Gender.Male
                },

                new Student
                {
                    Id = 8,
                    Name = "Anastasia",
                    Surname = "Efimova",
                    Patronymic = "Antonovna",
                    Height = 173,
                    Weight = 60,
                    BornDate = new DateTime(2000, 05, 22),
                    DateOfEntry = new DateTime(2016, 01, 12),
                    BoxingGroupId = 1,
                    NumberOfFights = 6,
                    Gender = Gender.Female
                },

                new Student
                {
                    Id = 9,
                    Name = "Viktoria",
                    Surname = "Narkevich",
                    Height = 180,
                    Weight = 60,
                    BornDate = new DateTime(2000, 09, 04),
                    DateOfEntry = new DateTime(2020, 06, 25),
                    BoxingGroupId = 2,
                    NumberOfFights = 1,
                    Gender = Gender.Female,
                },

                new Student
                {
                    Id = 10,
                    Name = "Dmitry",
                    Surname = "Kustovinov",
                    Patronymic = "Dmitrievich",
                    Height = 178,
                    Weight = 69,
                    BornDate = new DateTime(2000, 04, 03),
                    DateOfEntry = new DateTime(2016, 10, 25),
                    BoxingGroupId = 2,
                    NumberOfFights = 4,
                    Gender = Gender.Male,
                },

                new Student
                {
                    Id = 11,
                    Name = "Alexey",
                    Surname = "Fedorov",
                    Height = 166,
                    Weight = 54,
                    BornDate = new DateTime(1989, 05, 23),
                    DateOfEntry = new DateTime(2012, 09, 25),
                    BoxingGroupId = 1,
                    NumberOfFights = 10,
                    Gender = Gender.Male,
                },

                new Student
                {
                    Id = 12,
                    Name = "Evgeniy",
                    Surname = "Baranin",
                    Height = 169,
                    Weight = 57,
                    BornDate = new DateTime(1995, 06, 04),
                    DateOfEntry = new DateTime(2017, 05, 15),
                    BoxingGroupId = 1,
                    NumberOfFights = 6,
                    Gender = Gender.Male,
                },

                new Student
                {
                    Id = 13,
                    Name = "Alexander",
                    Surname = "Kirillov",
                    Height = 170,
                    Weight = 74,
                    BornDate = new DateTime(2003, 05, 04),
                    DateOfEntry = new DateTime(2015, 06, 23),
                    BoxingGroupId = 3,
                    NumberOfFights = 5,
                    Gender = Gender.Male,
                },

                new Student
                {
                    Id = 14,
                    Name = "Nikolay",
                    Surname = "Leshev",
                    Height = 165,
                    Weight = 67,
                    BornDate = new DateTime(2003, 12, 25),
                    DateOfEntry = new DateTime(2012, 02, 04),
                    BoxingGroupId = 3,
                    NumberOfFights = 6,
                    Gender = Gender.Male,
                },

                new Student
                {
                    Id = 15,
                    Name = "Valeria",
                    Surname = "Malahova",
                    Height = 170,
                    Weight = 52,
                    BornDate = new DateTime(2004, 02, 17),
                    DateOfEntry = new DateTime(2016, 05, 01),
                    BoxingGroupId = 1,
                    NumberOfFights = 7,
                    Gender = Gender.Female,
                },

                new Student
                {
                    Id = 16,
                    Name = "Julia",
                    Surname = "Belikova",
                    Height = 180,
                    Weight = 63,
                    BornDate = new DateTime(2000, 04, 04),
                    DateOfEntry = new DateTime(2016, 03, 10),
                    BoxingGroupId = 2,
                    NumberOfFights = 5,
                    Gender = Gender.Female,
                },

                new Student
                {
                    Id = 17,
                    Name = "Tatyana",
                    Surname = "Lelikova",
                    Height = 174,
                    Weight = 57,
                    BornDate = new DateTime(1990, 07, 21),
                    DateOfEntry = new DateTime(2010, 07, 01),
                    BoxingGroupId = 3,
                    NumberOfFights = 10,
                    Gender = Gender.Female,
                });

            modelBuilder.Entity<MedicalCertificate>().HasData(
                new MedicalCertificate()
                {
                    Id = 1,
                    ClinicName = "Polyclinic 4",
                    DateOfIssue = new DateTime(2021, 03, 10),
                    Result = MedicalResult.Success,
                    StudentId = 1
                },

                new MedicalCertificate()
                {
                    Id = 2,
                    ClinicName = "Polyclinic 4",
                    DateOfIssue = new DateTime(2020, 05, 02),
                    Result = MedicalResult.Fail,
                    StudentId = 1
                },

                new MedicalCertificate()
                {
                    Id = 3,
                    ClinicName = "Polyclinic 13",
                    DateOfIssue = new DateTime(2019, 10, 04),
                    Result = MedicalResult.Success,
                    StudentId = 1
                },

                new MedicalCertificate()
                {
                    Id = 4,
                    ClinicName = "VODC",
                    DateOfIssue = new DateTime(2018, 07, 14),
                    Result = MedicalResult.Success,
                    StudentId = 2
                },

                new MedicalCertificate()
                {
                    Id = 5,
                    ClinicName = "VODC",
                    DateOfIssue = new DateTime(2021, 04, 14),
                    Result = MedicalResult.Fail,
                    StudentId = 2
                },

                new MedicalCertificate()
                {
                    Id = 6,
                    ClinicName = "Polyclinic 4",
                    DateOfIssue = new DateTime(2020, 12, 06),
                    Result = MedicalResult.Fail,
                    StudentId = 3
                },

                new MedicalCertificate()
                {
                    Id = 7,
                    ClinicName = "Polyclinic 1",
                    DateOfIssue = new DateTime(2021, 05, 04),
                    Result = MedicalResult.Success,
                    StudentId = 3
                },
                
                new MedicalCertificate()
                {
                    Id = 8,
                    ClinicName = "VODC",
                    DateOfIssue = new DateTime(2021, 05, 31),
                    Result = MedicalResult.Success,
                    StudentId = 4
                },

                new MedicalCertificate()
                {
                    Id = 9,
                    ClinicName = "VODC",
                    DateOfIssue = new DateTime(2021, 05, 31),
                    Result = MedicalResult.Success,
                    StudentId = 5
                },

                new MedicalCertificate()
                {
                    Id = 10,
                    ClinicName = "VODC",
                    DateOfIssue = new DateTime(2021, 05, 31),
                    Result = MedicalResult.Success,
                    StudentId = 6
                },

                new MedicalCertificate()
                {
                    Id = 11,
                    ClinicName = "VODC",
                    DateOfIssue = new DateTime(2021, 05, 31),
                    Result = MedicalResult.Success,
                    StudentId = 7
                },

                new MedicalCertificate()
                {
                    Id = 12,
                    ClinicName = "VODC",
                    DateOfIssue = new DateTime(2021, 05, 31),
                    Result = MedicalResult.Success,
                    StudentId = 8
                },

                new MedicalCertificate()
                {
                    Id = 13,
                    ClinicName = "VODC",
                    DateOfIssue = new DateTime(2021, 05, 31),
                    Result = MedicalResult.Success,
                    StudentId = 9
                },

                new MedicalCertificate()
                {
                    Id = 14,
                    ClinicName = "VODC",
                    DateOfIssue = new DateTime(2021, 05, 31),
                    Result = MedicalResult.Success,
                    StudentId = 10
                },

                new MedicalCertificate()
                {
                    Id = 15,
                    ClinicName = "VODC",
                    DateOfIssue = new DateTime(2021, 05, 31),
                    Result = MedicalResult.Success,
                    StudentId = 11
                },

                new MedicalCertificate()
                {
                    Id = 16,
                    ClinicName = "VODC",
                    DateOfIssue = new DateTime(2021, 05, 31),
                    Result = MedicalResult.Success,
                    StudentId = 12
                },

                new MedicalCertificate()
                {
                    Id = 17,
                    ClinicName = "VODC",
                    DateOfIssue = new DateTime(2021, 05, 31),
                    Result = MedicalResult.Success,
                    StudentId = 13
                },

                new MedicalCertificate()
                {
                    Id = 18,
                    ClinicName = "VODC",
                    DateOfIssue = new DateTime(2021, 05, 31),
                    Result = MedicalResult.Success,
                    StudentId = 14
                },

                new MedicalCertificate()
                {
                    Id = 19,
                    ClinicName = "VODC",
                    DateOfIssue = new DateTime(2021, 05, 31),
                    Result = MedicalResult.Success,
                    StudentId = 15
                },

                new MedicalCertificate()
                {
                    Id = 20,
                    ClinicName = "VODC",
                    DateOfIssue = new DateTime(2021, 05, 31),
                    Result = MedicalResult.Success,
                    StudentId = 16
                },

                new MedicalCertificate()
                {
                    Id = 21,
                    ClinicName = "VODC",
                    DateOfIssue = new DateTime(2021, 05, 31),
                    Result = MedicalResult.Success,
                    StudentId = 17
                });

            //adding tournaments
            var moscowJuniorBoxingChampionship = new Tournament()
            {
                Id = 1,
                Name = "Moscow boxing championship",
                Country = "Russia",
                City = "Moscow",
                Date = new DateTime(2021, 06, 25),
                IsMedCertificateRequired = false
            };

            var voronezhBoxingLeague = new Tournament()
            {
                Id = 2,
                Name = "Voronezh Boxing League",
                Country = "Russia",
                City = "Voronezh",
                Date = new DateTime(2021, 08, 10),
                IsMedCertificateRequired = false
            };

            var internationalBoxingCompetition = new Tournament()
            {
                Id = 3,
                Name = "International Boxing Competition",
                Country = "Belarus",
                City = "Gomel",
                Date = new DateTime(2021, 07, 13),
                IsMedCertificateRequired = true
            };

            var internationalBoxingTournamentCupOfTheGovernorOfStPetersburg = new Tournament()
            {
                Id = 4,
                Name = "International boxing tournament - Cup of the Governor of St. Petersburg",
                Country = "Russia",
                City = "St. Petersburg",
                Date = new DateTime(2021, 10, 17),
                IsMedCertificateRequired = false
            };


            modelBuilder.Entity<Tournament>().HasData(moscowJuniorBoxingChampionship, voronezhBoxingLeague,
                                                      internationalBoxingCompetition,
                                                      internationalBoxingTournamentCupOfTheGovernorOfStPetersburg);
        }
    }
}
