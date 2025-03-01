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
        public int SubcategoryId { get; set; }
        public string SubcategoryName { get; set; }

    }
}
