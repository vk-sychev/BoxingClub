using BoxingClub.DAL.Entities;
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
            modelBuilder.Entity<Student>().HasData(

                    new Student
                    {
                        Id = 1,
                        Name = "Vasiliy",
                        Surname = "Sychev",
                        Patronymic = "Konstantinovich",
                        BornDate = new DateTime(2020, 10, 10),
                        DateOfEntry = new DateTime(2020, 2, 20),
                        Height = 175,
                        Weight = 88
                    },

                    new Student
                    {
                        Id = 2,
                        Name = "Igor",
                        Surname = "Zhuravlev",
                        BornDate = new DateTime(1991, 5, 22),
                        DateOfEntry = new DateTime(2019, 1, 15),
                    },

                    new Student
                    {
                        Id = 3,
                        Name = "Ivan",
                        Surname = "Pavlov",
                        BornDate = new DateTime(2001, 10, 14),
                        DateOfEntry = new DateTime(2020, 02, 28),
                        Height = 175,
                        Weight = 81
                    });
        }
    }
}
