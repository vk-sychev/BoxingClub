using BoxingClub.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BoxingClub.DAL.Interfaces
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task<List<Student>> GetStudentsPaginatedAsync(int pageIndex, int pageSize, Expression<Func<Student, bool>> filter);

        Task<int> GetCountOfStudentsAsync(Expression<Func<Student, bool>> filter);
    }
}
