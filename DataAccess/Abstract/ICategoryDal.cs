using Core.DataAccess;
using DataAccess.Concrete.EntityFramework;
using Entity.Concrete;
using Entity.DtoS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ICategoryDal : IRepositoryBase<Category>
    {
        List<CategoryWithSubcategoriesDto> categoryWithSubcategories();
    }
}
