using BoxingClub.WEB.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BoxingClub.Web.Validations
{
    public class UserViewModelValidator : AbstractValidator<UserViewModel>
    {
        public UserViewModelValidator()
        {
            string pattern = @"^[a-zA-Z]+\b";
            RuleFor(x => x.Name).NotNull()
                                .Matches(pattern)
                                .WithMessage("Name must contains only letters");

            RuleFor(x => x.Surname).NotNull()
                                   .Matches(pattern)
                                   .WithMessage("Surname must contains only letters");

            RuleFor(x => x.UserName).NotNull()
                                    .MinimumLength(5);

            RuleFor(x => x.Patronymic).Must(x => x == null || (x.Length > 0 && Regex.IsMatch(x, pattern)))
                                      .WithMessage("Patronymic must contains only letters");

            RuleFor(x => x.Role.Id).NotNull()
                                   .WithMessage("'Role' has to be selected");
        }
    }
}
