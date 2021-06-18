using System.Collections.Generic;
using System.Threading.Tasks;
using Students.BLL.DomainEntities;
using Students.BLL.DomainEntities.SpecModels;

namespace Students.BLL.Interfaces
{
    public interface IStudentSelectionService
    {
        Task<List<StudentFullDTO>> GetStudentsBySpecification(TournamentDTO tournament, TournamentSpecificationDTO specification);

        Task<List<StudentFullDTO>> GetStudentsByIds(List<int> studentsIds);
    }
}
