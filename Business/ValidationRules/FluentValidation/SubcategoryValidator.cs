﻿using Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class SubcategoryValidator:AbstractValidator<SubCategory>
    {
        public SubcategoryValidator()
        {
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Ad bos kecile bilmez");
        }
    }
}
