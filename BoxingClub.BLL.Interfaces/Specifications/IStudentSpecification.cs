using BoxingClub.BLL.DomainEntities;

namespace BoxingClub.BLL.Interfaces.Specifications
{
    public interface IStudentSpecification
    {
        bool IsValid(StudentFullDTO student);
    }
}
