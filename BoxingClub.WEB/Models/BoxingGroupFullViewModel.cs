using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BoxingClub.WEB.Models
{
    public class BoxingGroupFullViewModel
    {
        public int Id { get; set; }

        [DisplayName("Group's Name")]
        public string Name { get; set; }

        public CoachViewModel Coach { get; set; }

        public string FIO { get { return Coach.Surname + ' ' + Coach.Name + ' ' + Coach.Patronymic; } }

        [DisplayName("Coach")]
        public int CoachId { get; set; }

        public List<StudentLiteViewModel> Students { get; set; } = new List<StudentLiteViewModel>();
    }
}
