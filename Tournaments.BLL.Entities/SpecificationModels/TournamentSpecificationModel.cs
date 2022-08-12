using System;
using System.Collections.Generic;
using System.Text;

namespace Tournaments.BLL.Entities.SpecificationModels
{
    public class TournamentSpecificationModel
    {
        public int TournamentId { get; set; }

        public List<AgeGroupModel> AgeGroups { get; set; }= new List<AgeGroupModel>();
    }
}
