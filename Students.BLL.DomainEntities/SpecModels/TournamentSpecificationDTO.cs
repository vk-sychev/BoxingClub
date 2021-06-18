using System.Collections.Generic;

namespace Students.BLL.DomainEntities.SpecModels
{
    public class TournamentSpecificationDTO
    {
        public int TournamentId { get; set; }

        public List<AgeGroupDTO> AgeGroups { get; set; }
    }
}
