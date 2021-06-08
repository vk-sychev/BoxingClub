using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingClub.BLL.DomainEntities.Models
{
    public class TournamentSpecificationModel
    {
        public int TournamentId { get; set; }

        public List<AgeGroupModel> AgeGroups { get; set; }= new List<AgeGroupModel>();
    }
}
