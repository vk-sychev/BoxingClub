using BoxingClub.Web.Models;
using FluentValidation;

namespace BoxingClub.Web.Validations
{
    public class SignInViewModelValidator : AbstractValidator<SignInViewModel>
    {
        public SignInViewModelValidator()
        {
            RuleFor(x => x.UserName).NotNull().NotEmpty().WithName("Username");
            RuleFor(x => x.Password).NotNull().NotEmpty().MinimumLength(3);
        }
    }
}
