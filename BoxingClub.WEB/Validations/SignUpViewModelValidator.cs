using BoxingClub.WEB.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxingClub.WEB.Validations
{
    public class SignUpViewModelValidator : AbstractValidator<SignUpViewModel>
    {
        public SignUpViewModelValidator()
        {
            RuleFor(x => x.UserName).NotNull().MinimumLength(5);
            RuleFor(x => x.Email).EmailAddress().NotNull().NotEmpty();
            RuleFor(x => x.Password).NotNull().MinimumLength(3);
            RuleFor(x => x.Password).Equal(x => x.ConfirmPassword).WithMessage("Password and confirmation password do not match.");

            var todaysDate = DateTime.Today;
            RuleFor(x => x.Name).NotNull();
            RuleFor(x => x.Surname).NotNull();
            RuleFor(x => x.BornDate).NotNull()
                                    .LessThan(todaysDate)
                                    .WithMessage($"Date of Birth must be less than today's date: { todaysDate }")
                                    .Must(x => x.Year > todaysDate.Year - 100)
                                    .WithMessage($"Year of Birth must be greater than {todaysDate.Year - 100}");

            RuleFor(x => x.Patronymic).Must(x => x == null || x.Length > 0);
        }
    }
}
