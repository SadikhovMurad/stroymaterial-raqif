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
        public List<ProductForListDto> GetAllProducts()
        {
            using var context = new ModelDbContext();
            var products = context.Products.
                Include(p => p.Category)
                .Include(p => p.SubCategory).ToList();

            var dtoList = new List<ProductForListDto>();

            foreach (var product in products)
            {
                dtoList.Add(new ProductForListDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Marka = product.Marka,
                    CategoryId = product.CategoryId,
                    CategoryName = product.Category.Name,
                    SubcategoryId = product.SubCategoryId,
                    SubcategoryName = product.SubCategory.Name,
                    Price = product.Price,
                    Quantity = product.Quantity,
                    SaleCount = product.SaleCount,
                    Rating = product.Rating,
                    HasStock = product.hasStock,
                    ImageUrl = product.ImageUrl
                });
            }
            return dtoList;
        }
        public List<ProductByCategoryOrSubcategoryDto> GetProductsByCategory(int categoryId)
        {
            using var context = new ModelDbContext();

            var products = context.Products.Include(x => x.Category).Include(x=>x.SubCategory).Where(p=>p.CategoryId == categoryId).ToList();
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
        public List<ProductByCategoryOrSubcategoryDto> GetProductsBySubCategory(int subCategoryId)
        {
            using var context = new ModelDbContext();

            var products = context.Products.Include(x => x.Category).Include(x=>x.SubCategory).Where(p=>p.SubCategoryId == subCategoryId).ToList();
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
