    using Core.Entity.Abstract;
using Core.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class Order : IEntity
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public Cart Card { get; set; }
        public string Location { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string PhoneNumber { get; set; }
        public bool paymentMethodIsCart { get; set; }
        public string Notification { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsSuccess { get; set; } = false;

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
