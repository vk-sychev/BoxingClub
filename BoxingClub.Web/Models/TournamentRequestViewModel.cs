using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxingClub.Web.Models
{
    public class TournamentRequestViewModel
    {
        public int TournamentId { get; set; }

        public List<StudentFullViewModel> Students { get; set; } = new List<StudentFullViewModel>();
    }
}
