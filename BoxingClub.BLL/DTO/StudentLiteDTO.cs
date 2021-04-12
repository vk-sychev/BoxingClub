using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingClub.BLL.DTO
{
    public class StudentLiteDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public DateTime BornDate { get; set; }

        public int BoxingGroupId { get; set; }

        public BoxingGroupDTO BoxingGroup { get; set; }
    }
}
