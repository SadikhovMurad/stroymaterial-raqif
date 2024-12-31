using Core.DataAccess;
using Entity.Concrete;
using Entity.DtoS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ISubcategoryDal:IRepositoryBase<SubCategory>
    {
        List<SubcategoryWithCategoryDto> GetAllWithBaseCategory();
    }
}
