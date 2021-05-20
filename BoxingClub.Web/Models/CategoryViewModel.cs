using BoxingClub.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxingClub.Web.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        public int AgeWeightCategoryId { get; set; }

        public AgeWeightCategoryViewModel AgeWeightCategory { get; set; }

        public Gender Gender { get; set; }

        public bool IsSelected { get; set; }

        public string Name { get { return (AgeWeightCategory != null) ? $"{AgeWeightCategory.AgeCategory.Name} - {AgeWeightCategory.WeightCategory.Name} - {Gender}" : string.Empty; } }
    }
}
