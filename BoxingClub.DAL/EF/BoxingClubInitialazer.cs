using BoxingClub.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoxingClub.DAL.EF
{
    public static class BoxingClubInitialazer
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BoxingGroup>().HasData(
                new BoxingGroup
                {
                    Id = 1,
                    CoachId = 1,
                    Name = "Vityaz"
                },

                new BoxingGroup
                {
                    Id = 2,
                    CoachId = 1,
                    Name = "Warrior"
                },

                new BoxingGroup
                {
                    Id = 3,
                    CoachId = 2,
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
                    BoxingGroupId = 1
                },

                new Student
                {
                    Id = 2,
                    Name = "Igor",
                    Surname = "Zhuravlev",
                    BornDate = new DateTime(1991, 5, 22),
                    DateOfEntry = new DateTime(2019, 1, 15),
                    BoxingGroupId = 2
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
                    BoxingGroupId = 1
                },

                new Student
                {
                    Id = 4,
                    Name = "Andrew",
                    Surname = "Solovyev",
                    Patronymic = "Sergeevich",
                    BornDate = new DateTime(2000, 04, 03),
                    Height = 176,
                    Weight = 73
                });


            modelBuilder.Entity<Coach>().HasData(
                new Coach
                {
                    Id = 1,
                    Name = "Pavel",
                    Surname = "Dorochin",
                    BornDate = new DateTime(1995, 5, 5),
                    Patronymic = "Nikolayevich",
                    Description = "CMS in boxing"
                },

                new Coach
                {
                    Id = 2,
                    Name = "Vlad",
                    Surname = "Dorochin",
                    Patronymic = "Nikolayevich",
                    BornDate = new DateTime(1991, 07, 25),
                    Description = "CMS in boxing"
                },

                new Coach
                {
                    Id = 3,
                    Name = "Sergey",
                    Surname = "Goncharov",
                    BornDate = new DateTime(1970, 11, 01),
                    Description = "MS in boxing",
                });



            var adminsRoleId = Guid.NewGuid().ToString();
            var managersRoleId = Guid.NewGuid().ToString();

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
                });

            var manager1Id = Guid.NewGuid().ToString();
            var manager2Id = Guid.NewGuid().ToString();
            var adminId = Guid.NewGuid().ToString();
            PasswordHasher<IdentityUser> passwordHasher = new PasswordHasher<IdentityUser>();

            var admin = new IdentityUser
            {
                Id = adminId,
                UserName = "Admin",
                Email = "admin@gmail.com"
            };
            admin.NormalizedEmail = admin.Email.ToUpper();
            admin.NormalizedUserName = admin.UserName.ToUpper();
            admin.PasswordHash = passwordHasher.HashPassword(admin, "admin");

            var manager1 = new IdentityUser
            {
                Id = manager1Id,
                UserName = "Manager1",
                Email = "Manager1@gmail.com"
            };

            manager1.NormalizedEmail = manager1.Email.ToUpper();
            manager1.NormalizedUserName = manager1.UserName.ToUpper();
            manager1.PasswordHash = passwordHasher.HashPassword(manager1, "user123");

            var manager2 = new IdentityUser
            {
                Id = manager2Id,
                UserName = "Manager2",
                Email = "Manager2@gmail.com",
            };

            manager2.NormalizedEmail = manager2.Email.ToUpper();
            manager2.NormalizedUserName = manager2.UserName.ToUpper();
            manager2.PasswordHash = passwordHasher.HashPassword(manager2, "user234");

            modelBuilder.Entity<IdentityUser>().HasData(
                manager1, manager2, admin);



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
                });
        }
    }
}
