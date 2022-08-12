using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HttpClients.Models
{
    public class TournamentRequestModel
    {
        public int TournamentId { get; set; }

        public List<StudentFullModel> Students { get; set; } = new List<StudentFullModel>();
    }
}
