using BoxingClub.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoxingClub.BLL.Interfaces
{
    public interface IStudentService 
    {
        Task<StudentFullDTO> GetStudentAsync(int? id);

        Task<List<StudentLiteDTO>> GetStudentsAsync();

        Task CreateStudentAsync(StudentFullDTO studentDTO);

        Task DeleteStudentAsync(int? id);

        Task UpdateStudentAsync(StudentFullDTO studentDTO);

        Task DeleteFromGroupAsync(int? id);
    }
}
