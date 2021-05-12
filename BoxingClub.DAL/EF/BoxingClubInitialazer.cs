using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Entities.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace BoxingClub.DAL.EF
{
    public static class BoxingClubInitialazer
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var adminsRoleId = "291c0120-8c27-47c5-83fe-9d7deb36f73c";
            var managersRoleId = "7bb8b4c7-de76-4b77-b5cf-ce4ef11d83a6";
            var coachsRoleId = "8da509ca-2005-457d-8ca3-105792f04013";
            var usersRoleId = "db460306-31c6-457a-989e-9e4317be99b9";

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = adminsRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },

                new IdentityRole
                {
                    Id = managersRoleId,
                    Name = "Manager",
                    NormalizedName = "MANAGER"
                },

                new IdentityRole
                {
                    Id = usersRoleId,
                    Name = "User",
                    NormalizedName = "USER"
                },

                new IdentityRole
                {
                    Id = coachsRoleId,
                    Name = "Coach",
                    NormalizedName = "COACH"
                });

            var manager1Id = "fda7dfec-9828-41b2-bd9c-53dccbef2bb8";
            var manager2Id = "2d4254a5-7782-4b9c-a987-42a83d30669a";
            var adminId = "7dc730f1-78ec-41f5-a079-7d5e5d6b39ef";
            var coach1Id = "19759de3-ce1d-4cfd-8340-4e64eb245eb4";
            var coach2Id = "060342c3-9dc3-4597-bae1-9f19c991ebe9";
            var coach3Id = "a50a06a5-df07-4728-b6a0-93173c2ce4cf";

            PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();



            var coach1 = new ApplicationUser
            {
                Id = coach1Id,
                UserName = "Coach1",
                Email = "Coach1@gmail.com",
                Name = "Pavel",
                Surname = "Dorochin",
                BornDate = new DateTime(1995, 5, 5),
                Patronymic = "Nikolayevich",
                Description = "CMS in boxing"
            };

            coach1.NormalizedEmail = coach1.Email.ToUpper();
            coach1.NormalizedUserName = coach1.UserName.ToUpper();
            coach1.PasswordHash = passwordHasher.HashPassword(coach1, "coach1");


            var coach2 = new ApplicationUser
            {
                Id = coach2Id,
                UserName = "Coach2",
                Email = "Coach2@gmail.com",
                Name = "Vlad",
                Surname = "Dorochin",
                Patronymic = "Nikolayevich",
                BornDate = new DateTime(1991, 07, 25),
                Description = "CMS in boxing"
            };

            coach2.NormalizedEmail = coach2.Email.ToUpper();
            coach2.NormalizedUserName = coach2.UserName.ToUpper();
            coach2.PasswordHash = passwordHasher.HashPassword(coach2, "coach2");


            var coach3 = new ApplicationUser
            {
                Id = coach3Id,
                UserName = "Coach3",
                Email = "Coach3@gmail.com",
                Name = "Sergey",
                Surname = "Goncharov",
                BornDate = new DateTime(1970, 11, 01),
                Description = "MS in boxing",
            };

            coach3.NormalizedEmail = coach3.Email.ToUpper();
            coach3.NormalizedUserName = coach3.UserName.ToUpper();
            coach3.PasswordHash = passwordHasher.HashPassword(coach3, "coach3");


            var admin = new ApplicationUser
            {
                Id = adminId,
                Name = "Vasya",
                Surname = "Sychev",
                Patronymic = "Konstantinovich",
                BornDate = new DateTime(2000, 10, 10),
                UserName = "Admin",
                Email = "Admin@gmail.com"
            };
            admin.NormalizedEmail = admin.Email.ToUpper();
            admin.NormalizedUserName = admin.UserName.ToUpper();
            admin.PasswordHash = passwordHasher.HashPassword(admin, "admin");

            var manager1 = new ApplicationUser
            {
                Id = manager1Id,
                UserName = "Manager1",
                Email = "Manager1@gmail.com"
            };

            manager1.NormalizedEmail = manager1.Email.ToUpper();
            manager1.NormalizedUserName = manager1.UserName.ToUpper();
            manager1.PasswordHash = passwordHasher.HashPassword(manager1, "user123");

            var manager2 = new ApplicationUser
            {
                Id = manager2Id,
                UserName = "Manager2",
                Email = "Manager2@gmail.com",
            };

            manager2.NormalizedEmail = manager2.Email.ToUpper();
            manager2.NormalizedUserName = manager2.UserName.ToUpper();
            manager2.PasswordHash = passwordHasher.HashPassword(manager2, "user234");

            modelBuilder.Entity<ApplicationUser>().HasData(
                manager1, manager2, admin, coach1, coach2, coach3);


            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>()
                {
                    RoleId = adminsRoleId,
                    UserId = adminId
                },

                new IdentityUserRole<string>()
                {
                    RoleId = managersRoleId,
                    UserId = manager1Id
                },

                new IdentityUserRole<string>()
                {
                    RoleId = managersRoleId,
                    UserId = manager2Id
                },

                new IdentityUserRole<string>()
                {
                    RoleId = coachsRoleId,
                    UserId = coach1Id
                },

                new IdentityUserRole<string>()
                {
                    RoleId = coachsRoleId,
                    UserId = coach2Id
                },

                new IdentityUserRole<string>()
                {
                    RoleId = coachsRoleId,
                    UserId = coach3Id
                });


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
                    DateOfEntry = new DateTime(2020, 2, 20),
                    Height = 175,
                    Weight = 88,
                    BoxingGroupId = 1,
                    NumberOfFights = 3
                },

                new Student
                {
                    Id = 2,
                    Name = "Igor",
                    Surname = "Zhuravlev",
                    BornDate = new DateTime(1991, 5, 22),
                    DateOfEntry = new DateTime(2019, 1, 15),
                    Height = 180,
                    Weight = 87,
                    BoxingGroupId = 2,
                    NumberOfFights = 5
                },

                new Student
                {
                    Id = 3,
                    Name = "Ivan",
                    Surname = "Pavlov",
                    BornDate = new DateTime(2001, 10, 14),
                    DateOfEntry = new DateTime(2020, 02, 28),
                    Height = 175,
                    Weight = 81,
                    BoxingGroupId = 1,
                    NumberOfFights = 2
                },

                new Student
                {
                    Id = 4,
                    Name = "Andrew",
                    Surname = "Solovyev",
                    Patronymic = "Sergeevich",
                    BornDate = new DateTime(2000, 04, 03),
                    DateOfEntry = new DateTime(2021, 02, 02),
                    Height = 176,
                    Weight = 73,
                    NumberOfFights = 10
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
                });


            var ageCategory15_16yearsOld = new AgeCategory
            {
                Id = 1,
                Name = "15-16 years old",
                StartAge = 15,
                EndAge = 16
            };

            var ageCategory17_18yearsOld = new AgeCategory
            {
                Id = 2,
                Name = "Juniors",
                StartAge = 17,
                EndAge = 18
            };

            var ageCategoryAdult = new AgeCategory
            {
                Id = 3,
                Name = "Adults",
                StartAge = 19,
                EndAge = 40
            };

            modelBuilder.Entity<AgeCategory>().HasData(ageCategory15_16yearsOld, ageCategory17_18yearsOld, ageCategoryAdult);


            //Weight Categories for adult males and Junior-boys

            var lightWeightCategoryForAdultMales = new WeightCategory
            {
                Id = 1,
                Name = "Lightweight",
                StartWeight = 56,
                EndWeight = 60
            };

            var middleWeightCategoryForAdultMales = new WeightCategory
            {
                Id = 2,
                Name = "Middleweight",
                StartWeight = 69,
                EndWeight = 75
            };

            var heavyWeightCategoryForAdultMales = new WeightCategory
            {
                Id = 3,
                Name = "Heavyweight",
                StartWeight = 81,
                EndWeight = 91
            };

            var superHeavyWeightCategoryForAdultMales = new WeightCategory
            {
                Id = 4,
                Name = "Super heavyweight",
                StartWeight = 91
            };



            //Weight categories for adult females and junios-girls

            var lightWeightCategoryForAdultFemales = new WeightCategory
            {
                Id = 5,
                Name = "Lightweight",
                StartWeight = 57,
                EndWeight = 60
            };

            var middleWeightCategoryForAdultFemales = new WeightCategory
            {
                Id = 6,
                Name = "Middleweight",
                StartWeight = 69,
                EndWeight = 75
            };

            var heavyWeightCategoryForAdultFemales = new WeightCategory
            {
                Id = 7,
                Name = "Heavyweight",
                StartWeight = 81,
            };


            //Weight categories for boys and girls 15-16 years old

            var lightWeightCategoryForBoysAndGirls15_16YearsOld = new WeightCategory
            {
                Id = 8,
                Name = "Lightweight",
                StartWeight = 60,
                EndWeight = 63
            };

            var middleWeightCategoryForBoysAndGirls15_16YearsOld = new WeightCategory
            {
                Id = 9,
                Name = "Middleweight",
                StartWeight = 70,
                EndWeight = 75
            };

            var heavyWeightCategoryForBoysAndGirls15_16YearsOld = new WeightCategory
            {
                Id = 10,
                Name = "Heavyweight",
                StartWeight = 80
            };

            //adding males and junior-boys's weight categories;
            modelBuilder.Entity<WeightCategory>().HasData(heavyWeightCategoryForAdultMales, 
                                                          lightWeightCategoryForAdultMales, 
                                                          middleWeightCategoryForAdultMales, 
                                                          superHeavyWeightCategoryForAdultMales);


            //adding females and junior-girls's weight categories;
            modelBuilder.Entity<WeightCategory>().HasData(heavyWeightCategoryForAdultFemales, 
                                                          lightWeightCategoryForAdultFemales, 
                                                          middleWeightCategoryForAdultFemales);


            //adding 15-16 years old boys and girls's categories;
            modelBuilder.Entity<WeightCategory>().HasData(heavyWeightCategoryForBoysAndGirls15_16YearsOld,
                                                          lightWeightCategoryForBoysAndGirls15_16YearsOld,
                                                          middleWeightCategoryForBoysAndGirls15_16YearsOld);



            //adding tournaments
            var moscowJuniorBoxingChampionship = new Tournament()
            {
                Id = 1,
                Name = "Moscow junior boxing championship",
                Country = "Russia",
                City = "Moscow",
                Date = new DateTime(2021, 06, 25),
                IsMedCertificateNecessary = true
            };

            var voronezhBoxingLeague = new Tournament()
            {
                Id = 2,
                Name = "Voronezh Boxing League",
                Country = "Russia",
                City = "Voronezh",
                Date = new DateTime(2021, 08, 10),
                IsMedCertificateNecessary = false
            };

            var internationalWomensBoxingCompetition = new Tournament()
            {
                Id = 3,
                Name = "International Women's Boxing Competition",
                Country = "Belarus",
                City = "Gomel",
                Date = new DateTime(2021, 07, 13),
                IsMedCertificateNecessary = true
            };

            var internationalBoxingTournamentCupOfTheGovernorOfStPetersburg = new Tournament()
            {
                Id = 4,
                Name = "International boxing tournament - Cup of the Governor of St. Petersburg",
                Country = "Russia",
                City = "St. Petersburg",
                Date = new DateTime(2021, 10, 17),
                IsMedCertificateNecessary = false
            };


            modelBuilder.Entity<Tournament>().HasData(moscowJuniorBoxingChampionship, voronezhBoxingLeague, 
                                                      internationalWomensBoxingCompetition, 
                                                      internationalBoxingTournamentCupOfTheGovernorOfStPetersburg);

            //adding categories to tournaments

            //Moscow junior boxing championship

            var juniorsBoysLightWeightMoscow = new Category
            {
                Id = 1,
                AgeCategoryId = 2,
                WeightCategoryId = 1,
                Name = "Lightweight - Boys-juniors",
                Gender = Gender.Male,
                TournamentId = 1
            };

            var juniorsBoysMiddleWeightMoscow = new Category
            {
                Id = 2,
                AgeCategoryId = 2,
                WeightCategoryId = 2,
                Name = "Middleweight - Boys-juniors",
                Gender = Gender.Male,
                TournamentId = 1
            };

            var juniorsBoysHeavyWeightMoscow = new Category
            {
                Id = 3,
                AgeCategoryId = 2,
                WeightCategoryId = 3,
                Name = "Heavyweight - Boys-juniors",
                Gender = Gender.Male,
                TournamentId = 1
            };

            modelBuilder.Entity<Category>().HasData(juniorsBoysLightWeightMoscow, juniorsBoysMiddleWeightMoscow, juniorsBoysHeavyWeightMoscow);

            //Voronezh Boxing League

            //15-16 years girls and boys
            var boys15_16YearsOldLightWeightVoronezh = new Category
            {
                Id = 4,
                AgeCategoryId = 1,
                WeightCategoryId = 8,
                Name = "Lightweight - Boys 15-16 years old",
                Gender = Gender.Male,
                TournamentId = 2
            };

            var boys15_16YearsOldMiddleWeightVoronezh = new Category
            {
                Id = 5,
                AgeCategoryId = 1,
                WeightCategoryId = 9,
                Name = "Middlweight - Boys 15-16 years old",
                Gender = Gender.Male,
                TournamentId = 2
            };

            var boys15_16YearsOldHeavyWeightVoronezh = new Category
            {
                Id = 6,
                AgeCategoryId = 1,
                WeightCategoryId = 10,
                Name = "Heavyweight - Boys 15-16 years old",
                Gender = Gender.Male,
                TournamentId = 2
            };


            var girls15_16YearsOldLightWeightVoronezh = new Category
            {
                Id = 7,
                AgeCategoryId = 1,
                WeightCategoryId = 8,
                Name = "Lightweight - Girls 15-16 years old",
                Gender = Gender.Female,
                TournamentId = 2
            };

            var girls15_16YearsOldMiddleWeightVoronezh = new Category
            {
                Id = 8,
                AgeCategoryId = 1,
                WeightCategoryId = 9,
                Name = "Middleweight - Girls 15-16 years old",
                Gender = Gender.Female,
                TournamentId = 2
            };

            var girls15_16YearsOldHeavyWeightVoronezh = new Category
            {
                Id = 9,
                AgeCategoryId = 1,
                WeightCategoryId = 10,
                Name = "Heavyweight - Girls 15-16 years old",
                Gender = Gender.Female,
                TournamentId = 2
            };


            var juniorsBoysLightWeightVoronezh = new Category
            {
                Id = 10,
                AgeCategoryId = 2,
                WeightCategoryId = 1,
                Name = "Lightweight - Boys-juniors",
                Gender = Gender.Male,
                TournamentId = 2
            };

            var juniorsBoysMiddleWeightVoronezh = new Category
            {
                Id = 11,
                AgeCategoryId = 2,
                WeightCategoryId = 2,
                Name = "Middleweight - Boys-juniors",
                Gender = Gender.Male,
                TournamentId = 2
            };

            var juniorsBoysHeavyWeightVoronezh = new Category
            {
                Id = 12,
                AgeCategoryId = 2,
                WeightCategoryId = 3,
                Name = "Heavyweight - Boys-juniors",
                Gender = Gender.Male,
                TournamentId = 2
            };


            var juniorsGirlsLightWeightVoronezh = new Category
            {
                Id = 13,
                AgeCategoryId = 2,
                WeightCategoryId = 1,
                Name = "Lightweight - Girls-juniors",
                Gender = Gender.Female,
                TournamentId = 2
            };

            var juniorsGirlsMiddleWeightVoronezh = new Category
            {
                Id = 14,
                AgeCategoryId = 2,
                WeightCategoryId = 2,
                Name = "Middleweight - Girls-juniors",
                Gender = Gender.Female,
                TournamentId = 2
            };

            var juniorsGirlsHeavyWeightVoronezh = new Category
            {
                Id = 15,
                AgeCategoryId = 2,
                WeightCategoryId = 3,
                Name = "Heavyweight - Girls-juniors",
                Gender = Gender.Female,
                TournamentId = 2
            };


            modelBuilder.Entity<Category>().HasData(boys15_16YearsOldLightWeightVoronezh, boys15_16YearsOldMiddleWeightVoronezh, boys15_16YearsOldHeavyWeightVoronezh,
                                                    girls15_16YearsOldLightWeightVoronezh, girls15_16YearsOldMiddleWeightVoronezh, girls15_16YearsOldHeavyWeightVoronezh,
                                                    juniorsBoysLightWeightVoronezh, juniorsBoysMiddleWeightVoronezh, juniorsBoysHeavyWeightVoronezh,
                                                    juniorsGirlsLightWeightVoronezh, juniorsGirlsMiddleWeightVoronezh, juniorsGirlsHeavyWeightVoronezh);

            //International boxing tournament - Cup of the Governor of St. Petersburg
            var adultMalesLightWeightStPetersburg = new Category
            {
                Id = 16,
                AgeCategoryId = 3,
                WeightCategoryId = 1,
                Name = "Lightweight - Males",
                Gender = Gender.Male,
                TournamentId = 4
            };

            var adultMalesMiddleWeightStPetersburg = new Category
            {
                Id = 17,
                AgeCategoryId = 3,
                WeightCategoryId = 2,
                Name = "Miidleweight - Males",
                Gender = Gender.Male,
                TournamentId = 4
            };

            var adultMalesHeavyWeightStPetersburg = new Category
            {
                Id = 18,
                AgeCategoryId = 3,
                WeightCategoryId = 3,
                Name = "Heavyweight - Males",
                Gender = Gender.Male,
                TournamentId = 4
            };

            var adultMalesSuperHeavyWeightStPetersburg = new Category
            {
                Id = 19,
                AgeCategoryId = 3,
                WeightCategoryId = 4,
                Name = "Super heavyweight - Males",
                Gender = Gender.Male,
                TournamentId = 4
            };


            modelBuilder.Entity<Category>().HasData(adultMalesLightWeightStPetersburg, adultMalesMiddleWeightStPetersburg, 
                                                    adultMalesHeavyWeightStPetersburg, adultMalesSuperHeavyWeightStPetersburg);



            //International Women's Boxing Competition

            var adultFemalesLightWeightGomel = new Category
            {
                Id = 20,
                AgeCategoryId = 3,
                WeightCategoryId = 5,
                Name = "Lightweight - Females",
                Gender = Gender.Female,
                TournamentId = 3
            };

            var adultFemalesMiddleWeightGomel = new Category
            {
                Id = 21,
                AgeCategoryId = 3,
                WeightCategoryId = 6,
                Name = "Miidleweight - Females",
                Gender = Gender.Female,
                TournamentId = 3
            };

            var adultFemalesHeavyWeightGomel = new Category
            {
                Id = 22,
                AgeCategoryId = 3,
                WeightCategoryId = 7,
                Name = "Heavyweight - Females",
                Gender = Gender.Female,
                TournamentId = 3
            };


            modelBuilder.Entity<Category>().HasData(adultFemalesLightWeightGomel, adultFemalesMiddleWeightGomel, adultFemalesHeavyWeightGomel);
        }
    }
}
