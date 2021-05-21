using System.Collections.Generic;

namespace BoxingClub.DAL.Entities
{
    public class WeightCategory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int StartWeight { get; set; }

        public int? EndWeight { get; set; }

        public List<AgeCategory> AgeCategories { get; set; }

        public List<AgeWeightCategory> AgeWeightCategories { get; set; }

    }
}
