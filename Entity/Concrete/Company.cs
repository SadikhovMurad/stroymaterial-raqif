using Core.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class Company : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string VOEN { get; set; }
        public string CorporateEmail { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string? InstagramAddress { get; set; }
        public string? FacebookAddress { get; set; }
        public string? TiktokAddress { get; set; }


    }
}
