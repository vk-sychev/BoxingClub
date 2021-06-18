using System.Collections.Generic;

namespace HttpClients.Models.SpecModels
{
    public class TournamentSpecification
    {
        public int TournamentId { get; set; }

        public List<AgeGroup> AgeGroups { get; set; }
    }
}
