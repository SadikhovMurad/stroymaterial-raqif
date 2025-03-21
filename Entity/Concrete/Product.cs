﻿using Core.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class Product : IEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Marka { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }
        public decimal Price { get; set; }
        public float Rating { get; set; }
        public int Quantity { get; set; }
        public int SaleCount { get; set; }
        public decimal CostPrice { get; set; }
        public bool hasStock { get; set; }
        public string? ImageUrl { get; set; }
        public ICollection<CartItem> CartItems { get; set; }

    }
}
