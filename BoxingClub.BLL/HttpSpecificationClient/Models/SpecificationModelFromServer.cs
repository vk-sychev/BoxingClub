using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingClub.BLL.Implementation.HttpSpecificationClient.Models
{
    class SpecificationModelFromServer
    {
        public int TournamentId { get; set; }

        public List<AgeGroupModelFromServer> AgeGroups { get; set; }= new List<AgeGroupModelFromServer>();
    }
}
