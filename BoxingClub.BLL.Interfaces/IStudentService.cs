using BoxingClub.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoxingClub.BLL.Interfaces
{
    public interface IStudentService 
    {
        Task<StudentFullDTO> GetStudentByIdAsync(int? id);

        Task<List<StudentLiteDTO>> GetStudentsAsync();

        Task CreateStudentAsync(StudentFullDTO studentDTO);

        Task DeleteStudentAsync(int? id);

        Task UpdateStudentAsync(StudentFullDTO studentDTO);

        Task DeleteFromGroupAsync(int? studentId);
    }
}
