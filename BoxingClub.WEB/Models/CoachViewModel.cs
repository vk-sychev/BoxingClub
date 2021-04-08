using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BoxingClub.WEB.Models
{
    public class CoachViewModel
    {
        [DisplayName("Coach")]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public string Description { get; set; }

        public string FIO { get { return Surname + ' ' + Name + ' ' + Patronymic; } }
    }
}
