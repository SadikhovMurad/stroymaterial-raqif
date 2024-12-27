using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspect.Validation;
using Core.Utilities.BusinessRules;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Concrete;
using Entity.DtoS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal productDal;
        private readonly IMapper _mapper;

        public ProductManager(IProductDal productDal, IMapper mapper)
        {
            this.productDal = productDal;
            _mapper = mapper;
        }


        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(ProductDto productDto)
        {
            var result = BusinessRules.Run(CheckIfProductNameExist(productDto.Name));
            if (result != null)
            {
                return result;
            }
            var product = _mapper.Map<Product>(productDto);
            productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        public IResult Delete(Guid id)
        {
            var product = productDal.Get(x => x.Id == id);
            if (product == null)
            {
                return new ErrorResult("Bele id de mehsul yoxdur");
            }
            productDal.Delete(product);
            return new SuccessResult(Messages.ProductRemoved);
        }

        public IDataResult<List<Product>> GetAll()
        {

            return new SuccessDataResult<List<Product>>(productDal.GetAll(), Messages.CategoryListed);
        }

        public IDataResult<List<Product>> GetAllProductsByCategoryId(int categoryId)
        {
            return new SuccessDataResult<List<Product>>(productDal.GetAll(p => p.CategoryId == categoryId), Messages.ProductListedByCategoryId);
        }

        public IDataResult<List<Product>> GetAllProductsByCategoryName(string categoryName)
        {
            return new SuccessDataResult<List<Product>>(productDal.GetAll(p => p.CategoryName == categoryName), Messages.ProductListedByCategoryId);

        }

        public IDataResult<List<Product>> GetAllProductsBySubcategoryId(int subcategoryId)
        {
            return new SuccessDataResult<List<Product>>(productDal.GetAll(p => p.SubCategoryId == subcategoryId), Messages.ProductListedBySubcategoryId);
        }

        public IDataResult<List<Product>> GetAllProductsBySubcategoryName(string subcategoryName)
        {
            return new SuccessDataResult<List<Product>>(productDal.GetAll(p => p.SubCategoryName == subcategoryName), Messages.ProductListedBySubcategoryId);
        }

        public IDataResult<List<Product>> GetByFilter<T>(string propertyName, T value)
        {
            throw new NotImplementedException();
        }

        public IDataResult<Product> GetById(Guid id)
        {
            return new SuccessDataResult<Product>(productDal.Get(p => p.Id == id), Messages.GetProductById);
        }

        [ValidationAspect(typeof(ProductValidator))]
        public IResult Update(Guid id, Product? product)
        {
            var result = BusinessRules.Run(CheckIfProductNameExistForUpdate(product.Name, product.Id));
            if (result != null)
            {
                return result;
            }
            productDal.Update(product);
            return new SuccessResult(Messages.ProductUpdated);
        }

        #region Business Code
        private IResult CheckIfProductNameExist(string productName)
        {
            var result = productDal.GetAll(x => x.Name == productName).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductExists);
            }
            return new SuccessResult();
        }

        private IResult CheckIfProductNameExistForUpdate(string productName, Guid id)
        {
            var result = productDal.GetAll(x => x.Name == productName && x.Id != id).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductExists);
            }
            return new SuccessResult();
        }
        #endregion

    }
}
