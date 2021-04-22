using BoxingClub.Web.Models;
using FluentValidation;
using System;

namespace BoxingClub.Web.Validations
{
    public class StudentFullViewModelValidator : AbstractValidator<StudentFullViewModel>
    {
        public StudentFullViewModelValidator()
        {
            var todaysDate = DateTime.Today;
            RuleFor(x => x.Name).NotNull();
            RuleFor(x => x.Surname).NotNull();
            RuleFor(x => x.BornDate).NotEmpty()
                                    .WithMessage("Date of Birth must not be empty")
                                    .LessThan(todaysDate)
                                    .WithMessage($"Date of Birth must be less than today's date: { todaysDate }")
                                    .Must(x => x.Year > todaysDate.Year - 100)
                                    .WithMessage($"Year of Birth must be greater than {todaysDate.Year - 100}");

            RuleFor(x => x.Patronymic).Must(x => x == null || x.Length > 0);

            RuleFor(x => x.DateOfEntry).NotNull()
                                       .Must(x => x <= todaysDate)
                                       .WithMessage($"Date of entry must be less or equal today's date: { todaysDate }")
                                       .GreaterThan(x => x.BornDate)
                                       .WithMessage("Date of entry must be greater than the date of birth");

            RuleFor(x => x.Height).NotNull().NotEmpty().LessThanOrEqualTo(220);
            RuleFor(x => x.Weight).NotNull().NotEmpty().LessThanOrEqualTo(130);
        }
    }
}
