using System.Linq;
using BoxingClub.Infrastructure.Enums;
using Students.BLL.DomainEntities;
using Students.BLL.DomainEntities.SpecModels;
using Students.BLL.Interfaces.Specifications;
using ArgumentNullException = BoxingClub.Infrastructure.Exceptions.ArgumentNullException;

namespace Students.BLL.Implementation.Specifications
{
    public class CategorySpecification : ICategorySpecification
    {
        public bool IsValid(StudentFullDTO student, AgeGroupDTO ageGroup)
        {
            if (student == null)
            {
                throw new ArgumentNullException(nameof(student), "student is null");
            }

            if (ageGroup == null)
            {
                throw new ArgumentNullException(nameof(ageGroup), "ageGroup is null");
            }

            if (!IsAgeValid(student.GetStudentAge(), ageGroup.AgeCategory.StartAge, ageGroup.AgeCategory.EndAge))
            {
                return false;
            }

            if (!IsGenderValid(student.Gender, ageGroup.Gender))
            {
                return false;
            }

            if (ageGroup.WeightCategories.All(weight => !IsWeightValid(student.Weight, weight.StartWeight, weight.EndWeight)))
            {
                return false;
            }

            return true;
        }

        private bool IsAgeValid(int studentAge, int startAge, int endAge)
        {
            if (studentAge >= startAge && studentAge <= endAge)
            {
                return true;
            }

            return false;
        }

        private bool IsWeightValid(double studentWeight, double startWeight, double endWeight)
        {
            if (studentWeight >= startWeight && studentWeight <= endWeight)
            {
                return true;
            }

            return false;
        }

        private bool IsGenderValid(Gender studentGender, Gender gender)
        {
            if (studentGender == gender)
            {
                return true;
            }

            return false;
        }
    }
}
