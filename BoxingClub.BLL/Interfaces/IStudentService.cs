using BoxingClub.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingClub.BLL.Interfaces
{
    public interface IStudentService
    {
        void Dispose();
        StudentLiteDTO GetStudent(int? id);
        IEnumerable<StudentLiteDTO> GetStudents();
        void CreateStudent(CreateStudentDTO studentDTO);

    }
}
