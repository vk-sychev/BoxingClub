using BoxingClub.WEB.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxingClub.WEB.Validations
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
