using BoxingClub.WEB.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
