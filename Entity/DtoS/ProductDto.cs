using Core.Entity.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DtoS
{
    public class ProductDto : IDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public int CategoryId { get; set; }
        public int SubcategoryId { get; set; }
        public IFormFile file { get; set; }
        public string ImagePath { get; set; }

    }
}
