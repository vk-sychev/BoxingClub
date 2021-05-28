using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingClub.BLL.Implementation.HttpSpecificationClient.Models
{
    class AgeGroupModelFromServer
    {
        public int Id { get; set; }

        public int Sex { get; set; }

        public AgeCategoryModelFromServer AgeCategory {get; set; }

        public List<WeightCategoryModelFromServer> WeightCategories { get; set; } = new List<WeightCategoryModelFromServer>();
    }
}
