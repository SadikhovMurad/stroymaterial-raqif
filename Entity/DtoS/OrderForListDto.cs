using Core.Entity.Abstract;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DtoS
{
    public class OrderForListDto:IDto
    {
        public Guid OrderId { get; set; }
        public int CartId { get; set; }
        public List<CartItemDto> CartItemDtos { get; set; }
        public DateTime OrderDate { get; set; }
        public string Location { get; set; }
        public string UserEmail { get; set; }
        public string UserName { get; set; }
        public string UserPhoneNumber { get; set; }
        public string Notification { get; set; }
        public decimal TotalAmount { get; set; }
        public bool Success { get; set; }

    }
}
