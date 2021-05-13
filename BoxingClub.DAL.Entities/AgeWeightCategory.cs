using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingClub.DAL.Entities
{
    public class AgeWeightCategory
    {
        public int Id { get; set; }

        public int? WeightCategoryId { get; set; }

        public WeightCategory WeightCategory { get; set; }

        public int? AgeCategoryId { get; set; }

        public AgeCategory AgeCategory { get; set; }
    }
}
