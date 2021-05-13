using BoxingClub.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingClub.DAL.Entities
{
    public class Category
    {
        public int Id { get; set; }

        public int? AgeWeightCategoryId { get; set; }

        public AgeWeightCategory AgeWeightCategory { get; set; }

        public Gender Gender { get; set; }
    }
}
