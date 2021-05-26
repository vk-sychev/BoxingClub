using System;
using System.Collections.Generic;
using System.Text;
using BoxingClub.Infrastructure.Enums;

namespace BoxingClub.BLL.DomainEntities
{
    public class AgeGroup
    {
        public AgeCategoryDTO AgeCategory { get; set; }

        public List<WeightCategoryDTO> WeightCategories { get; set; }

        public Gender Gender { get; set; }
    }
}
