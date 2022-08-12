using Tournaments.BLL.Entities;
using Tournaments.DAL.Entities;

namespace Tournaments.BLL.Interfaces.Specifications
{
    public interface IStudentSpecification
    {
        bool Validate(StudentFullDTO student);

        bool Validate(StudentFullDTO student, Tournament tournament);
    }
}
