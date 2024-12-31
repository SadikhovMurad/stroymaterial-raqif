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
    public class EfCategoryDal : EfRepositoryBase<Category, ModelDbContext>, ICategoryDal
    {
        public List<CategoryWithSubcategoriesDto> categoryWithSubcategories()
        {
            using var context = new ModelDbContext();
            List<Category> categories = context.Categories.Include(c => c.SubCategories).ToList();
            List<CategoryWithSubcategoriesDto> categoryWithSubcategoriesDtos = new List<CategoryWithSubcategoriesDto>();

            foreach(var category in categories)
            {
                CategoryWithSubcategoriesDto dto = new CategoryWithSubcategoriesDto()
                {
                    CategoryName = category.Name,
                    SubCategories = category.SubCategories.Where(subCategory => subCategory.CategoryId == category.Id).Select(subCategory => subCategory.Name).ToList()
                };
                categoryWithSubcategoriesDtos.Add(dto);
            }
            return categoryWithSubcategoriesDtos;
        }
    }
}
