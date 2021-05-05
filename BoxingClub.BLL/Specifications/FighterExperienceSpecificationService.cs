using BoxingClub.BLL.DomainEntities;
using BoxingClub.BLL.Interfaces.Specifications;
using BoxingClub.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoxingClub.BLL.Implementation.Specifications
{
    public class FighterExperienceSpecificationService : IStudentSpecification
    {
        private readonly IUnitOfWork _database;

        public FighterExperienceSpecificationService(IUnitOfWork uow)
        {
            _database = uow;
        }

        public async Task<bool> IsValidAsync(StudentFullDTO student)
        {
            int specId = 1; // по-хорошему, надо бы это в параметрах передавать, но нужно будет менять интерфейс.
                            // Возможно, стоит создать общий интерфейс для спеков и отнаследовать от них другие интерфейсы, более конкретные
            var spec = await _database.FighterExperienceSpecifications.GetByIdAsync(specId);

            var diff = GetStudentTrainingPerod(student.DateOfEntry);

            var durationRule = diff >= spec.TrainingPeriod;
            var fightsRule = student.NumberOfFights >= spec.NumberOfFights;

            var result = durationRule && fightsRule;

            return result;
        }

        private int GetStudentTrainingPerod(DateTime dateOfEntry)
        {
            var dateOfEntryYear = dateOfEntry.Year;
            var currentYear = DateTime.Now.Year;
            var diff = currentYear - dateOfEntryYear;
            return diff;
        }
    }
}
