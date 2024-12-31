using Core.Utilities.Results;
using Entity.Concrete;
using Entity.DtoS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        IDataResult<List<Category>> GetAll();
        IDataResult<List<CategoryWithSubcategoriesDto>> GetAllCategoryWithSubcategories();
        IResult AddSubcategory(int categoryId,SubCategory subCategory);
        IDataResult<Category> GetById(int id);
        IDataResult<Category> GetByName(string name);
        IResult Add(CategoryDto categoryDto);
        IResult Update(int id, CategoryDto? categoryDto);
        IResult Delete(int id);

    }
}
