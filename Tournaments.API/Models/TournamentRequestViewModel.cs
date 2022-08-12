using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tournaments.BLL.Entities;

namespace Tournaments.API.Models
{
    public class TournamentRequestViewModel
    {
        public int TournamentId { get; set; }

        public List<StudentFullDTO> Students { get; set; } = new List<StudentFullDTO>();
    }
}
