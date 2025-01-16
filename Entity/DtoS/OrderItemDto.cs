using Core.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DtoS
{
    public class OrderItemDto : IDto
    {
        public Guid ProductId { get; set; }
        public Guid? OrderId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
