using Core.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class OrderAssignment : IEntity
    {
        public int Id { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public int CourierId { get; set; }
        public Employee Employee { get; set; }
        public bool Status { get; set; }

    }
}
