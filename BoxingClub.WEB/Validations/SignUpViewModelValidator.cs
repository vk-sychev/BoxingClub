using BoxingClub.WEB.Models;
using FluentValidation;
using System;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;

namespace BoxingClub.WEB.Validations
{
    public class SignUpViewModelValidator : AbstractValidator<SignUpViewModel>
    {
        private readonly IConfiguration _configuration;
        public SignUpViewModelValidator(IConfiguration configuration)
        {
            _configuration = configuration;
            string pattern = @"^[a-zA-Z]+\b";
            var passwordLength = Convert.ToInt32(_configuration.GetSection("PasswordSettings").GetSection("RequiredLength").Value);

            RuleFor(x => x.UserName).NotNull().MinimumLength(5);
            RuleFor(x => x.Email).EmailAddress();

            RuleFor(x => x.Password).NotNull()
                                    .MinimumLength(passwordLength)
                                    .Matches(@"^\w+\b")
                                    .WithMessage("Password must contains only letters");

            RuleFor(x => x.Password).Equal(x => x.ConfirmPassword)
                                    .WithMessage("Password and confirmation password do not match.");

            var todaysDate = DateTime.Today;
            RuleFor(x => x.Name).NotNull()
                                .Matches(pattern)
                                .WithMessage("Name must contains only letters"); 

            RuleFor(x => x.Surname).NotNull()
                                   .Matches(pattern)
                                   .WithMessage("Surname must contains only letters");

            RuleFor(x => x.BornDate).NotNull()
                                    .LessThan(todaysDate)
                                    .WithMessage($"Date of Birth must be less than today's date: { todaysDate }")
                                    .Must(x => x.Year > todaysDate.Year - 100)
                                    .WithMessage($"Year of Birth must be greater than {todaysDate.Year - 100}");

            RuleFor(x => x.Patronymic).Must(x => x == null || x.Length > 0 && Regex.IsMatch(x, pattern))
                                      .WithMessage("Patronymic must contains only letters");
        }
    }
}
