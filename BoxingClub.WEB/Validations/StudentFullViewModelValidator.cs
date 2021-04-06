using BoxingClub.WEB.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxingClub.WEB.Validations
{
    public class StudentFullViewModelValidator : AbstractValidator<StudentFullViewModel>
    {
        public StudentFullViewModelValidator()
        {
            RuleFor(x => x.Name).NotNull();
            RuleFor(x => x.Surname).NotNull();
            RuleFor(x => x.BornDate).Must(x => x.Year > DateTime.Today.Year - 100).WithMessage("Please, enter the correct date of Birth");
/*            RuleFor(x => x.Patronymic).Must(x => x.Length == 0 || x.Length > 0);
            RuleFor(x => x.DateOfEntry); //.Null().Empty(); // || x.Year > DateTime.Today.Year - 70).WithMessage("Please, enter the correct date of Birth");*/
        }
    }
}
