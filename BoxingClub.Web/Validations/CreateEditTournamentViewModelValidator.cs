using BoxingClub.Web.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxingClub.Web.Validations
{
    public class CreateEditTournamentViewModelValidator : AbstractValidator<CreateEditTournamentViewModel>
    {
        public CreateEditTournamentViewModelValidator()
        {
            
        }
    }
}
