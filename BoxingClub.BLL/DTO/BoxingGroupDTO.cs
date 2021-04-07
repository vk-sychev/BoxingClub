using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingClub.BLL.DTO
{
    public class BoxingGroupDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public CoachDTO Coach { get; set; }

        public List<StudentLiteDTO> Students { get; set; } = new List<StudentLiteDTO>();
    }
}
