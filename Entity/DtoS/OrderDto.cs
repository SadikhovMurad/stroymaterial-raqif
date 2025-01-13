using Core.Entity.Abstract;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DtoS
{
    public class OrderDto : IDto
    {
        public DateTime OrderDate { get; set; }
        public string Location { get; set; }
        public string UserId { get; set; }
        public string PhoneNumber { get; set; }
        public bool paymentMethodIsCart { get; set; }
        public string Notification { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
