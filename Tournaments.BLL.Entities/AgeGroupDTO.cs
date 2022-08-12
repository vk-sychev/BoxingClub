using System;
using System.Collections.Generic;
using System.Text;
using BoxingClub.Infrastructure.Enums;

namespace Tournaments.BLL.Entities
{
    public class AgeGroupDTO
    {
        public AgeCategoryDTO AgeCategory { get; set; }

        public List<WeightCategoryDTO> WeightCategories { get; set; }

        public Gender Gender { get; set; }
    }
}
