namespace Students.BLL.DomainEntities
{
    public class SearchModelDTO
    {
        public int? PageIndex { get; set; }

        public int? PageSize { get; set; }

        public int? ExperienceFilter { get; set; }

        public int? MedExaminationFilter { get; set; }
    }
}
