using Students.BLL.DomainEntities;

namespace Students.BLL.Interfaces.Specifications
{
    public interface IStudentSpecification
    {
        bool Validate(StudentFullDTO student);

        bool Validate(StudentFullDTO student, TournamentDTO tournament);
    }
}
