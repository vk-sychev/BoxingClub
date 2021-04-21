using BoxingClub.WEB.Models;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using System;

namespace BoxingClub.WEB.Validations
{
    public class SignInViewModelValidator : AbstractValidator<SignInViewModel>
    {
        private readonly IConfiguration _configuration;
        public SignInViewModelValidator(IConfiguration configuration)
        {
            _configuration = configuration;
            var passwordLength = Convert.ToInt32(_configuration.GetSection("PasswordSettings").GetSection("RequiredLength").Value);
            var passwordUserNamePattern = @"^\w+\b";

            RuleFor(x => x.UserName).NotNull()
                                    .MinimumLength(5)
                                    .Matches(passwordUserNamePattern)
                                    .WithName("Username");

            RuleFor(x => x.Password).NotNull()
                                    .NotEmpty()
                                    .MinimumLength(passwordLength)
                                    .Matches(passwordUserNamePattern);
        }
    }
}
