using BoxingClub.Web.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxingClub.Web.Validations
{
    public class TournamentFullViewModelValidator : AbstractValidator<TournamentFullViewModel>
    {
        public TournamentFullViewModelValidator()
        {
            string pattern = @"^[a-zA-Zа-яА-Я]+\b";
            var todaysDate = DateTime.Today;

            RuleFor(x => x.Name).NotNull();
            RuleFor(x => x.Date).NotNull()
                                .GreaterThan(todaysDate)
                                .WithMessage($"Tournament date must be greater then today's date {todaysDate}");

            RuleFor(x => x.Country).NotNull()
                                   .Matches(pattern);
            RuleFor(x => x.City).NotNull()
                                .Matches(pattern);

        }
    }
}
