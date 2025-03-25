using Core.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class OrderItem:IEntity
    {
        public int Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImageUrl { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
        public decimal ItemTotalPrice { get; set; }
    }
}
