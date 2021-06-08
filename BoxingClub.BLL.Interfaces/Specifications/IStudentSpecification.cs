using BoxingClub.BLL.DomainEntities;
using BoxingClub.DAL.Entities;

namespace BoxingClub.BLL.Interfaces.Specifications
{
    public interface IStudentSpecification
    {
        bool Validate(StudentFullDTO student);

        bool Validate(StudentFullDTO student, Tournament tournament);
    }
}
