using System.Collections.Generic;
using BoxingClub.Infrastructure.Enums;

namespace HttpClients.Models.SpecModels
{
    public class AgeGroup
    {
        public AgeCategory AgeCategory { get; set; }

        public List<WeightCategory> WeightCategories { get; set; }

        public Gender Gender { get; set; }
    }
}
