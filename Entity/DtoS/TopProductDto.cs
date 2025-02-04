using Core.Entity.Abstract;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DtoS
{
    public class TopProductDto:IDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Marka { get; set; }
        public string CategoryName { get; set; }
        public string SubcategoryName { get; set; }
        public decimal Price { get; set; }
        public int SaleCount { get; set; }
        public int Quantity { get; set; }
        public bool hasStock { get; set; }
    }
}
