using BoxingClub.Web.Models;
using FluentValidation;

namespace BoxingClub.Web.Validations
{
    public class BoxingGroupLiteViewModelValidator : AbstractValidator<BoxingGroupLiteViewModel>
    {
        public BoxingGroupLiteViewModelValidator()
        {
            string pattern = @"^[a-zA-Zа-яА-Я]+\b";
            RuleFor(x => x.CoachId).NotNull()
                                   .WithMessage("'Coach' has to be selected!");

            RuleFor(x => x.Name).NotNull()
                                .NotEmpty()
                                .Matches(pattern)
                                .WithMessage("Name must contain only letters");
        }
    }
}
