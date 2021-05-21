namespace BoxingClub.BLL.DomainEntities
{
    public class AgeWeightCategoryDTO
    {
        public int Id { get; set; }

        public int WeightCategoryId { get; set; }

        public WeightCategoryDTO WeightCategory { get; set; }

        public int AgeCategoryId { get; set; }

        public AgeCategoryDTO AgeCategory { get; set; }
    }
}