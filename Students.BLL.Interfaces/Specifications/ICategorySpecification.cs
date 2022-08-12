using Students.BLL.DomainEntities;
using Students.BLL.DomainEntities.SpecModels;

namespace Students.BLL.Interfaces.Specifications
{
    public interface ICategorySpecification
    {
        bool IsValid(StudentFullDTO student, AgeGroupDTO specification);
    }
}
