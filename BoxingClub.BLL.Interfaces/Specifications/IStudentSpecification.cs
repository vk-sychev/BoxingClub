using BoxingClub.BLL.DomainEntities;

namespace BoxingClub.BLL.Interfaces.Specifications
{
    public interface IStudentSpecification
    {
        bool Validate(StudentFullDTO student);
    }
}
