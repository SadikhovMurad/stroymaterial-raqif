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
    public class EfSubcategoryDal : EfRepositoryBase<SubCategory, ModelDbContext>, ISubcategoryDal
    {
        public List<SubcategoryWithCategoryDto> GetAllWithBaseCategory()
        {
            using var context = new ModelDbContext();

            var subCategory = context.SubCategories.Include(x => x.Category).ToList();
            List<SubcategoryWithCategoryDto> subCategoryWithBaseCategories = new List<SubcategoryWithCategoryDto>();

            foreach (var item in subCategory)
            {
                SubcategoryWithCategoryDto scwd = new SubcategoryWithCategoryDto
                {
                    SubcategoryName = item.Name,
                    CategoryName = item.Category.Name
                };
                subCategoryWithBaseCategories.Add(scwd);
            }
            return subCategoryWithBaseCategories;
        }
        public SubcategoryWithCategoryDto GetByIdWithBaseCategory(int id)
        {
            using var context = new ModelDbContext();
            SubCategory subCategory = context.SubCategories.Include(x => x.Category).FirstOrDefault(x => x.Id == id);
            SubcategoryWithCategoryDto subCategoryWithBaseCategoryDto = new SubcategoryWithCategoryDto
            {
                SubcategoryName = subCategory.Name,
                CategoryName = subCategory.Category.Name
            };
            return subCategoryWithBaseCategoryDto;
        }
    }
}
