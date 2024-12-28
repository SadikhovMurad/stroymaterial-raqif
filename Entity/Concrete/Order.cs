    using Core.Entity.Abstract;
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
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public DateTime OrderDate { get; set; }
        public string Location { get; set; }
        public string UserId { get; set; }
        public string PhoneNumber { get; set; }
        public bool paymentMethodIsCart { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsSuccess { get; set; }
    }
}
