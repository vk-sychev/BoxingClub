using System;
using System.Collections.Generic;
using System.Text;

namespace Tournaments.BLL.Entities.SpecificationModels
{
    public class WeightCategoryModel
    {
        public int Id { get; set; }

        public decimal MinValue { get; set; }

        public decimal MaxValue { get; set; }

        public string Title { get; set; }
    }
}
