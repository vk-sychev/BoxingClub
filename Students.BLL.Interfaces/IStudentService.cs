using System.Threading.Tasks;
using Students.BLL.DomainEntities;

namespace Students.BLL.Interfaces
{
    public interface IStudentService 
    {
        Task<StudentFullDTO> GetStudentByIdAsync(int id);

        Task<PageModelDTO<StudentLiteDTO>> GetStudentsPaginatedAsync(SearchModelDTO searchDTO);

        Task CreateStudentAsync(StudentFullDTO studentDTO);

        Task DeleteStudentAsync(int id);

        Task UpdateStudentAsync(StudentFullDTO studentDTO);

        Task DeleteFromGroupAsync(int studentId);
    }
}
