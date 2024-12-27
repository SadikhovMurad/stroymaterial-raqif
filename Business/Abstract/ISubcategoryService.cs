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
    public interface ISubcategoryService
    {
        IDataResult<List<SubCategory>> GetAll();
        IDataResult<List<SubcategoryWithCategoryDto>> GetSubcategoryWithCategoryName();
        IDataResult<SubCategory> GetById(int id);
        IDataResult<SubCategory> GetByName(string name);
        IResult Add(SubcategoryDto subcategoryDto);
        IResult Update(int id, SubCategory? subcategory);
        IResult Delete(int id);
    }
}
