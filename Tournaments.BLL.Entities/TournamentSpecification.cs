using System;
using System.Collections.Generic;
using System.Text;

namespace Tournaments.BLL.Entities
{
    public class TournamentSpecification
    {
        public int TournamentId { get; set; }

        public List<AgeGroupDTO> AgeGroups { get; set; }
    }
}
