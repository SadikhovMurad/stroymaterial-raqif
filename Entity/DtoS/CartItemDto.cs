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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public decimal ProductPrice { get; set; }
        public bool IsOrderedItem { get; set; }
        public int Quantity { get; set; }
        public decimal ItemTotalPrice { get; set; }
    }
}
