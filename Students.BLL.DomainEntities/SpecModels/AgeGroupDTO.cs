using System.Collections.Generic;
using BoxingClub.Infrastructure.Enums;

namespace Students.BLL.DomainEntities.SpecModels
{
    public class AgeGroupDTO
    {
        public AgeCategoryDTO AgeCategory { get; set; }

        public List<WeightCategoryDTO> WeightCategories { get; set; }

        public Gender Gender { get; set; }
    }
}
