using BoxingClub.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingClub.BLL.Interfaces
{
    public interface IStudentService
    {
        //not sure it is correct name of this service. But this service (yet) has only to return the list of students
        void Dispose();
        IndexStudentDTO GetStudent(int? id);
        IEnumerable<IndexStudentDTO> GetStudents();

    }
}
