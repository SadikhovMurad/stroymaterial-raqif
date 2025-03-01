using Core.Entity.Abstract;
using Core.Entity.Concrete;
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
        public string Location { get; set; }
        public string PhoneNumber { get; set; }
        public string Notification { get; set; }
    }
}
