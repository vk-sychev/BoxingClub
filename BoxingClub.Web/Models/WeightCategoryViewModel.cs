using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxingClub.Web.Models
{
    public class WeightCategoryViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int StartWeight { get; set; }

        public int EndWeight { get; set; }
    }
}
