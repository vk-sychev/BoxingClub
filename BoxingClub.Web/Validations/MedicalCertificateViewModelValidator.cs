using BoxingClub.Web.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxingClub.Web.Validations
{
    public class MedicalCertificateViewModelValidator : AbstractValidator<MedicalCertificateViewModel>
    {
        public MedicalCertificateViewModelValidator()
        {
            var todaysDate = DateTime.Today;
            RuleFor(x => x.ClinicName).NotNull()
                                      .NotEmpty();

            RuleFor(x => x.DateOfIssue).NotNull()
                                       .Must(x => x <= todaysDate)
                                       .WithMessage($"Date of entry must be less or equal today's date: { todaysDate }");
        }
    }
}
