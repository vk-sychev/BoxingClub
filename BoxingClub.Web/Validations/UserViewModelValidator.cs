using BoxingClub.WEB.Models;
using FluentValidation;
using System.Text.RegularExpressions;

namespace BoxingClub.Web.Validations
{
    public class UserViewModelValidator : AbstractValidator<UserViewModel>
    {
        public UserViewModelValidator()
        {
            string pattern = @"^[a-zA-Zа-яА-Я]+\b";
            var passwordUserNamePattern = @"^\w+\b";
            RuleFor(x => x.Name).NotNull()
                                .Matches(pattern)
                                .WithMessage("Name must contain only letters");

            RuleFor(x => x.Surname).NotNull()
                                   .Matches(pattern)
                                   .WithMessage("Surname must contain only letters");

            RuleFor(x => x.UserName).NotNull()
                                    .MinimumLength(5)
                                    .Matches(passwordUserNamePattern)
                                    .WithMessage("Username must comtain only English letters and/or digits");

            RuleFor(x => x.Patronymic).Must(x => x == null || (x.Length > 0 && Regex.IsMatch(x, pattern)))
                                      .WithMessage("Patronymic must contain only letters");

            RuleFor(x => x.Role.Id).NotNull()
                                   .WithMessage("'Role' has to be selected");
        }
    }
}
