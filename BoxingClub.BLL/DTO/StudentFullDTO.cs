using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingClub.BLL.DTO
{
    public class StudentFullDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public DateTime BornDate { get; set; }

        public int Height { get; set; }

        public double Weight { get; set; }

        public DateTime DateOfEntry { get; set; }
    }
}
