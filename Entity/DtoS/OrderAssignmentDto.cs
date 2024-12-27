using Core.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DtoS
{
    public class OrderAssignmentDto:IDto
    {
        public Guid OrderId { get; set; }
        public int EmployeeId { get; set; }
        public bool Status { get; set; }
    }
}
