using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using IdentityServer.DAL.Entities;

namespace IdentityServer.DAL.Implementation.EF
{
    public static class UserInitialazer
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var adminsRoleId = "291c0120-8c27-47c5-83fe-9d7deb36f73c";
            var managersRoleId = "7bb8b4c7-de76-4b77-b5cf-ce4ef11d83a6";
            var coachsRoleId = "8da509ca-2005-457d-8ca3-105792f04013";
            var usersRoleId = "db460306-31c6-457a-989e-9e4317be99b9";

            modelBuilder.Entity<ApplicationRole>().HasData(
                new ApplicationRole
                {
                    Id = adminsRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },

                new ApplicationRole
                {
                    Id = managersRoleId,
                    Name = "Manager",
                    NormalizedName = "MANAGER"
                },

                new ApplicationRole
                {
                    Id = usersRoleId,
                    Name = "User",
                    NormalizedName = "USER"
                },

                new ApplicationRole
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


            modelBuilder.Entity<ApplicationUserRole>().HasData(
                new ApplicationUserRole()
                {
                    RoleId = adminsRoleId,
                    UserId = adminId
                },

                new ApplicationUserRole()
                {
                    RoleId = managersRoleId,
                    UserId = manager1Id
                },

                new ApplicationUserRole()
                {
                    RoleId = managersRoleId,
                    UserId = manager2Id
                },

                new ApplicationUserRole()
                {
                    RoleId = coachsRoleId,
                    UserId = coach1Id
                },

                new ApplicationUserRole()
                {
                    RoleId = coachsRoleId,
                    UserId = coach2Id
                },

                new ApplicationUserRole()
                {
                    RoleId = coachsRoleId,
                    UserId = coach3Id
                });

        }
    }
}
