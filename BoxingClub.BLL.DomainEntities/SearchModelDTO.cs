using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingClub.BLL.DomainEntities
{
    public class SearchModelDTO
    {
        public int? PageIndex { get; set; }

        public int? PageSize { get; set; }

        public int? Filter { get; set; }
    }
}
