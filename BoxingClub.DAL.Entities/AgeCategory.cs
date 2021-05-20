using System.Collections.Generic;

namespace BoxingClub.DAL.Entities
{
    public class AgeCategory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int StartAge { get; set; }

        public int EndAge { get; set; }

        public List<WeightCategory> WeightCategories { get; set; }

        public List<AgeWeightCategory> AgeWeightCategories { get; set; }

    }
}
