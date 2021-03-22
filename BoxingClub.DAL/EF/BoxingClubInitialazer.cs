using BoxingClub.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoxingClub.DAL.EF
{
    public static class BoxingClubInitialazer
    {
        public static void Initialize(BoxingClubContext context)
        {
            if (!context.Students.Any())
            {
                context.Students.AddRange(
                    new Student
                    {
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
                        Name = "Igor",
                        Surname = "Zhuravlev",
                        BornDate = new DateTime(1991, 5, 22),
                        DateOfEntry = new DateTime(2019, 1, 15),
                    },

                    new Student
                    {
                        Name = "Ivan",
                        Surname = "Pavlov",
                        BornDate = new DateTime(2001, 10, 14),
                        DateOfEntry = new DateTime(2020, 02, 28),
                        Height = 175,
                        Weight = 81
                    });
                context.SaveChanges();
            }
        }
    }
}
