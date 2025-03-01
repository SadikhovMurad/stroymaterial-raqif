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

            // Bütün kategoriyaları və onların subkategoriyalarını çəkirik
            List<Category> categories = context.Categories
                .Include(c => c.SubCategories)
                .ToList();

            List<CategoryWithSubcategoriesDto> categoryWithSubcategoriesDtos = new List<CategoryWithSubcategoriesDto>();

            foreach (var category in categories)
            {
                var subdtos = new List<SubcategoryWithCategoryDto>(); // Hər yeni category üçün yeni list

                foreach (var subcategory in category.SubCategories)
                {
                    subdtos.Add(new SubcategoryWithCategoryDto()
                    {
                        SubcategoryId = subcategory.Id,
                        SubcategoryName = subcategory.Name
                    });
                }

                // Artıq bir dəfə category əlavə edilir və içində bütün subcategory-lər olur
                CategoryWithSubcategoriesDto dto = new CategoryWithSubcategoriesDto()
                {
                    CategoryId = category.Id,
                    CategoryName = category.Name,
                    SubCategories = subdtos
                };

                categoryWithSubcategoriesDtos.Add(dto);
            }

            return categoryWithSubcategoriesDtos;
        }
    }
}
