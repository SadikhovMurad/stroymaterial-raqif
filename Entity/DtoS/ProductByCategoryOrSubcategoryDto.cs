using Core.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DtoS
{
    public class ProductByCategoryOrSubcategoryDto:IDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public bool HasStock { get; set; }
        public string ImageUrl { get; set; }
    }
}
