using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxingClub.Web.Models
{
    public class AgeCategoryViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int StartAge { get; set; }

        public int EndAge { get; set; }

        public string FullName { get { return $"{Name} {StartAge} - {EndAge}"; } } 

        public bool IsSelected { get; set; }
    }
}
