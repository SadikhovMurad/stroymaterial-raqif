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
    public interface IProductService
    {
        IDataResult<List<ProductByCategoryOrSubcategoryDto>> GetAll();
        IDataResult<List<ProductByCategoryOrSubcategoryDto>> GetAllProductsByCategoryId(int categoryId);
        IDataResult<List<ProductByCategoryOrSubcategoryDto>> GetAllProductsBySubcategoryId(int subcategoryId);
        IDataResult<Product> GetById(Guid id);
        IDataResult<List<Product>> GetByFilter<T>(string propertyName,T value);
        IResult Add(ProductDto productDto);
        IResult Update(Guid id, Product? product);
        IResult Delete(Guid id);


    }
}
