using Core.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class OrderHistory : IEntity
    {
        public int Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public int CoruierId { get; set; }
        public bool status { get; set; }
    }
}
