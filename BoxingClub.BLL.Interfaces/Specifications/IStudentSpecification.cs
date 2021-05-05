using BoxingClub.BLL.DomainEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoxingClub.BLL.Interfaces.Specifications
{
    public interface IStudentSpecification
    {
        Task<bool> IsValidAsync(StudentFullDTO student);
    }
}
