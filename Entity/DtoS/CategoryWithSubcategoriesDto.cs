using Core.Entity.Abstract;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DtoS
{
    public class CategoryWithSubcategoriesDto : IDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<SubcategoryWithCategoryDto> SubCategories { get; set; }
    }
}
