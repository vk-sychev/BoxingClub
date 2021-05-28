using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingClub.BLL.Implementation.HttpSpecificationClient.Models
{
    public class AgeCategoryModelFromServer
    {
        public int MinAge { get; set; }

        public int MaxAge { get; set; }

        public int MinYear { get; set; }

        public int MaxYear { get; set; }
    }
}
