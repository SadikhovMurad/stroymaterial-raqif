using Core.Entity.Abstract;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DtoS
{
    public class CartDto:IDto
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string email { get; set; }
        public string PhoneNumber { get; set; }
        public decimal TotalPrice { get; set; }
        public bool isOrdered { get; set; }
        public bool cartItemsIsNull { get; set; }
        public List<CartAndCartItemDto>? CartItems { get; set; }
    }
}
