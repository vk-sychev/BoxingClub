using BoxingClub.Web.Models;
using FluentValidation;

namespace BoxingClub.Web.Validations
{
    public class RoleViewModelValidator : AbstractValidator<RoleViewModel>
    {
        public RoleViewModelValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
        }
    }
}
