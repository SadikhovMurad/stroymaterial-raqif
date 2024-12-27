using Core.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DtoS
{
    public class SubcategoryWithCategoryDto : IDto
    {
        public string SubcategoryName { get; set; }
        public string CategoryName { get; set; }

    }
}
