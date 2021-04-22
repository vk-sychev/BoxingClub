using BoxingClub.Web.Models;
using FluentValidation;

namespace BoxingClub.Web.Validations
{
    public class BoxingGroupLiteViewModelValidator : AbstractValidator<BoxingGroupLiteViewModel>
    {
        public BoxingGroupLiteViewModelValidator()
        {
            RuleFor(x => x.CoachId).NotNull().NotEmpty().WithMessage("Coach has to be selected!");
            RuleFor(x => x.Name).NotNull().NotEmpty();
        }
    }
}
