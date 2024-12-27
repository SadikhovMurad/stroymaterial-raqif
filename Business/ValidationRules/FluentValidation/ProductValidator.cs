using Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class ProductValidator:AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p=>p.Name).NotEmpty().WithMessage("Mehsulun adi yazilmalidir");
            RuleFor(p => p.Description).NotEmpty().WithMessage("Aciglama bolmesi bos qoyula bilmez");
            RuleFor(p => p.CategoryId).NotEmpty().WithMessage("Kateqoriya bildirilmelidir");
            RuleFor(p => p.SubCategoryId).NotEmpty().WithMessage("Alt kateqoriyasi bildirilmelidir");
            RuleFor(p => p.Marka).NotEmpty().WithMessage("Marka adi bos qoyula bilmez");
            RuleFor(p => p.ImageUrl).NotEmpty().WithMessage("Wekil qoyulmalidir");

        }
    }
}
