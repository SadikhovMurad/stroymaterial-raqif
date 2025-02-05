using Core.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DtoS
{
    public class CartAndCartItemDto:IDto
    {
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
        public decimal ItemTotalPrice { get; set; }
    }
}
