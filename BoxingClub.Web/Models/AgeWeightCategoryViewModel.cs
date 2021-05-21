namespace BoxingClub.Web.Models
{
    public class AgeWeightCategoryViewModel
    {
        public int Id { get; set; }

        public int WeightCategoryId { get; set; }

        public WeightCategoryViewModel WeightCategory { get; set; }

        public int AgeCategoryId { get; set; }

        public AgeCategoryViewModel AgeCategory { get; set; }
    }
}