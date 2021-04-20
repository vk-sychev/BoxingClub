using BoxingClub.WEB.Models;
using FluentValidation;

namespace BoxingClub.WEB.Validations
{
    public class BoxingGroupLiteViewModelValidator : AbstractValidator<BoxingGroupLiteViewModel>
    {
        public BoxingGroupLiteViewModelValidator()
        {
            //RuleFor(x => x.Coach).NotNull().NotEmpty().WithMessage("Coach has to be selected!");
            RuleFor(x => x.Name).NotNull().NotEmpty();
        }
    }
}
