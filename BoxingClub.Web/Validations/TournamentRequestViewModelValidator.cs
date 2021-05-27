using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoxingClub.Web.Models;
using FluentValidation;

namespace BoxingClub.Web.Validations
{
    public class TournamentRequestViewModelValidator : AbstractValidator<TournamentRequestViewModel>
    {
        public TournamentRequestViewModelValidator()
        {
            RuleFor(x => x.Students).Must(x => x.Count>0);
        }
    }
}
