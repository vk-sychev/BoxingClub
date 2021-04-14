using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BoxingClub.WEB.Models
{
    public class BoxingGroupLiteViewModel
    {
        public int Id { get; set; }

        [DisplayName("Group's Name")]
        public string Name { get; set; }

        public UserViewModel Coach { get; set; }

        [DisplayName("Coach")]
        public string CoachId { get; set; }
    }
}
