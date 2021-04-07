using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxingClub.WEB.Models
{
    public class BoxingGroupViewModel
    {
        public string Name { get; set; }

        public CoachViewModel Coach { get; set; }

        public string CoachName { get { return Coach.Name; } }

        public string CoachSurname { get { return Coach.Surname; } }

        public string CoachPatronymic { get { return Coach.Patronymic; } }

        public List<StudentFullViewModel> Students { get; set; } = new List<StudentFullViewModel>();
    }
}
