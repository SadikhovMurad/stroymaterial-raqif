using Core.DataAccess;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entity.Concrete;
using Entity.DtoS;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfRepositoryBase<Product, ModelDbContext>, IProductDal
    {
        public List<ProductByCategoryOrSubcategoryDto> GetProductsByCategory()
        {
            using var context = new ModelDbContext();

            var products = context.Products.Include(x => x.Category).Include(x=>x.SubCategory).ToList();
            List<ProductByCategoryOrSubcategoryDto> productByCategoryOrSubcategories = new List<ProductByCategoryOrSubcategoryDto>();

            foreach (var item in products)
            {
                ProductByCategoryOrSubcategoryDto dto = new ProductByCategoryOrSubcategoryDto
                {
                    Id = item.Id,
                    Name= item.Name,
                    Description= item.Description,
                    Brand=item.Marka,
                    CategoryName=item.Category.Name,
                    SubCategoryName = item.SubCategory.Name,
                    Price=item.Price,
                    Quantity=item.Quantity,
                    HasStock=item.hasStock,
                    ImageUrl=item.ImageUrl,
                };
                productByCategoryOrSubcategories.Add(dto);
            }
            return productByCategoryOrSubcategories;
        }

        public List<ProductByCategoryOrSubcategoryDto> GetProductsBySubCategory()
        {
            using var context = new ModelDbContext();

            var products = context.Products.Include(x => x.SubCategory).ToList();
            List<ProductByCategoryOrSubcategoryDto> productByCategoryOrSubcategories = new List<ProductByCategoryOrSubcategoryDto>();

            foreach (var item in products)
            {
                ProductByCategoryOrSubcategoryDto dto = new ProductByCategoryOrSubcategoryDto
                {
                    Name = item.Name,
                    Description = item.Description,
                    Brand = item.Marka,
                    CategoryName = item.Category.Name,
                    SubCategoryName = item.SubCategory.Name,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    HasStock = item.hasStock,
                    ImageUrl = item.ImageUrl,
                };
                productByCategoryOrSubcategories.Add(dto);
            }
            return productByCategoryOrSubcategories;
        }
    }
}
