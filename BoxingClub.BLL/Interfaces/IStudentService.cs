using BoxingClub.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoxingClub.BLL.Interfaces
{
    public interface IStudentService 
    {
        Task<StudentFullDTO> GetStudent(int? id);

        Task<IEnumerable<StudentLiteDTO>> GetStudents();

        Task CreateStudent(StudentFullDTO studentDTO);

        Task DeleteStudent(int? id);

        Task UpdateStudent(StudentFullDTO studentDTO);

    }
}
