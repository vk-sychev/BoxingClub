using BoxingClub.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingClub.BLL.Interfaces
{
    public interface IStudentService
    {
        void Dispose();

        CreateStudentDTO GetStudent(int? id);

        IEnumerable<StudentLiteDTO> GetStudents();

        void CreateStudent(CreateStudentDTO studentDTO);

        void DeleteStudent(int? id);

        void UpdateStudent(CreateStudentDTO studentDTO);

    }
}
