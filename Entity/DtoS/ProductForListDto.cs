﻿using Core.Entity.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DtoS
{
    public class ProductForListDto : IDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Marka { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int SubcategoryId { get; set; }
        public string SubcategoryName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int SaleCount { get; set; }
        public float Rating { get; set; }
        public bool HasStock { get; set; }
        public string ImageUrl { get; set; }
    }
}
