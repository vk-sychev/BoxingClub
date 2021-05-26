using System;
using System.Collections.Generic;
using System.Text;
using BoxingClub.BLL.DomainEntities;

namespace BoxingClub.BLL.Interfaces.Specifications
{
    public interface ICategorySpecification
    {
        bool IsValid(StudentFullDTO student, AgeGroup specification);
    }
}
