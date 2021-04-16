using BoxingClub.WEB.Models;
using FluentValidation;

namespace BoxingClub.WEB.Validations
{
    public class RoleViewModelValidator : AbstractValidator<RoleViewModel>
    {
        public RoleViewModelValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
        }
    }
}
