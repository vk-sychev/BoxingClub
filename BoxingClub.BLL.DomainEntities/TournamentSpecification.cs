using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingClub.BLL.DomainEntities
{
    public class TournamentSpecification
    {
        public int TournamentId { get; set; }

        public List<AgeGroup> AgeGroups { get; set; }
    }
}
