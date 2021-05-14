using BoxingClub.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingClub.BLL.DomainEntities
{
    public class CategoryDTO
    {
        public int Id { get; set; }

        public int AgeWeightCategoryId { get; set; }

        public AgeWeightCategoryDTO AgeWeightCategory { get; set; }

        public Gender Gender { get; set; }
    }
}
