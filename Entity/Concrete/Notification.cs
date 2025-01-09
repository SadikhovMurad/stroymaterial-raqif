using Core.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class Notification : IEntity
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
    }
}
