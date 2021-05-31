using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingClub.BLL.DomainEntities.Models
{
    public class AgeCategoryModel
    {
        public int MinAge { get; set; }

        public int MaxAge { get; set; }

        public int MinYear { get; set; }

        public int MaxYear { get; set; }
    }
}
