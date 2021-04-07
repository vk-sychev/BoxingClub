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
            var todaysDate = DateTime.Today;
            var res = new DateTime(2020, 5, 5) < todaysDate;
            RuleFor(x => x.Name).NotNull();
            RuleFor(x => x.Surname).NotNull();
            RuleFor(x => x.BornDate).NotNull()
                                    .LessThan(todaysDate)
                                    .WithMessage($"Date of Birth must be less than today's date: { todaysDate }")
                                    .Must(x => x.Year > todaysDate.Year - 100)
                                    .WithMessage($"Year of Birth must be greater than {todaysDate.Year}");

            RuleFor(x => x.Patronymic).Must(x => x == null || x.Length > 0);

            RuleFor(x => x.DateOfEntry).NotNull()
                                       //.LessThanOrEqualTo(todaysDate);
                                       .LessThan(todaysDate)
                                       .WithMessage($"Date of entry must be less or equal today's date: { todaysDate }")
                                       .GreaterThan(x => x.BornDate)
                                       .WithMessage("Date of entry must be greater than the date of birth");

            RuleFor(x => x.Height).NotNull().NotEmpty().LessThanOrEqualTo(220);
            RuleFor(x => x.Weight).NotNull().NotEmpty().LessThanOrEqualTo(130);
        }
    }
}
