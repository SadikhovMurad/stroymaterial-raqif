using Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class ReportValidator : AbstractValidator<Report>
    {
        public ReportValidator()
        {
            RuleFor(x => x.Date).NotEmpty().WithMessage("Tarix verilmeyib");
        }
    }
}
