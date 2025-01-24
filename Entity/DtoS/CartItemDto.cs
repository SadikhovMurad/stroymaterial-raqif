using Core.Entity.Abstract;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DtoS
{
    public class CartItemDto:IDto
    {
        public int CartId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal ItemTotalPrice { get; set; }
    }
}
