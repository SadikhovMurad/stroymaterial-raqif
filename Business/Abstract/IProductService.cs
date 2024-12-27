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
        IDataResult<List<Product>> GetAll();
        IDataResult<List<Product>> GetAllProductsByCategoryId(int categoryId);
        IDataResult<List<Product>> GetAllProductsByCategoryName(string categoryName);
        IDataResult<List<Product>> GetAllProductsBySubcategoryId(int subcategoryId);
        IDataResult<List<Product>> GetAllProductsBySubcategoryName(string subcategoryName);
        IDataResult<Product> GetById(Guid id);
        IDataResult<List<Product>> GetByFilter<T>(string propertyName,T value);
        IResult Add(ProductDto productDto);
        IResult Update(Guid id, Product? product);
        IResult Delete(Guid id);


    }
}
