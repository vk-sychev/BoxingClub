using System.Collections.Generic;

namespace BoxingClub.BLL.DTO
{
    public class BoxingGroupDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CoachId { get; set; }

        public UserDTO Coach { get; set; }

        public List<StudentLiteDTO> Students { get; set; } = new List<StudentLiteDTO>();
    }
}
