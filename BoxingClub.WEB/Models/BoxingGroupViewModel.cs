using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BoxingClub.WEB.Models
{
    public class BoxingGroupViewModel
    {
        public int Id { get; set; }

        [DisplayName("Group's Name")]
        public string Name { get; set; }

        public CoachViewModel Coach { get; set; }

        [DisplayName("Coach's Name")]
        public string CoachName { get { return Coach.Name; } }

        [DisplayName("Coach's Surname")]
        public string CoachSurname { get { return Coach.Surname; } }

        [DisplayName("Coach's Patronymic")]
        public string CoachPatronymic { get { return Coach.Patronymic; } }

        public List<StudentFullViewModel> Students { get; set; } = new List<StudentFullViewModel>();
    }
}
