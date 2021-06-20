using System;
using System.Collections.Generic;
using System.Text;

namespace Tournaments.BLL.Entities.SpecificationModels
{
    public class AgeGroupModel
    {
        public int Id { get; set; }

        public int Sex { get; set; }

        public AgeCategoryModel AgeCategory {get; set; }

        public List<WeightCategoryModel> WeightCategories { get; set; } = new List<WeightCategoryModel>();
    }
}
