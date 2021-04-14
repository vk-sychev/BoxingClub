using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BoxingClub.WEB.Models
{
    public class UserViewModel
    {
        //[DisplayName("Coach")]
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public string Description { get; set; }

        public string FullName { get { return $"{Surname} {Name} {Patronymic}"; } }
    }
}
