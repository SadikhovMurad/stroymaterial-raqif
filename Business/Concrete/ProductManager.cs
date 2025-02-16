using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspect.Validation;
using Core.Storage;
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
        private readonly ICategoryDal categoryDal;
        private readonly ISubcategoryDal subcategoryDal;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IMapper _mapper;

        public ProductManager(IProductDal productDal, IMapper mapper, ICategoryDal categoryDal, ISubcategoryDal subcategoryDal, ICloudinaryService cloudinaryService)
        {
            this.productDal = productDal;
            _mapper = mapper;
            this.categoryDal = categoryDal;
            this.subcategoryDal = subcategoryDal;
            _cloudinaryService = cloudinaryService;
        }


        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(ProductDto productDto)
        {
            var result = BusinessRules.Run(CheckIfProductNameExist(productDto.Name));
            if (result != null)
            {
                return result;
            }
            var category = categoryDal.Get(c => c.Id == productDto.CategoryId);
            var subcategory = subcategoryDal.Get(sc=>sc.Id == productDto.SubcategoryId);
            if (category == null)
            {
                return new ErrorResult("Kateqoriya ve ya Alt kateqoriya tapilmadi");
            }
            productDto.ImageUrl = _cloudinaryService.Upload(productDto.file).ToString();
            var product = _mapper.Map<Product>(productDto);
            if (category.Products == null || subcategory.Products == null)
            {
                category.Products = new List<Product>();
                subcategory.Products = new List<Product>();


            }
            category.Products.Add(product);
            subcategory.Products.Add(product);
            if(productDto.file == null)
            {
                return new ErrorResult("Sekil bos kecile bilmez");
            }
            
            productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }
        public IResult AddStock(int count, Guid id)
        {
            var product = productDal.Get(p=>p.Id == id);
            if (product == null)
            {
                return new ErrorResult(Messages.ProductNotFound);
            }
            product.Quantity = product.Quantity + count;
            var addedStockProduct = new Product()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                CategoryId = product.CategoryId,
                Category = product.Category,
                SubCategoryId = product.SubCategoryId,
                SubCategory = product.SubCategory,
                Marka = product.Marka,
                Quantity = product.Quantity + count,
                hasStock = true,
                Price = product.Price,
                ImageUrl = product.ImageUrl
            };
            productDal.Update(product);
            return new SuccessResult(Messages.ProductQuantityAdded);
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
        public IDataResult<List<ProductByCategoryOrSubcategoryDto>> GetAll()
        {

            throw new NotImplementedException();
        }
        public IDataResult<List<ProductByCategoryOrSubcategoryDto>> GetAllProductsByCategoryId(int categoryId)
        {
            return new SuccessDataResult<List<ProductByCategoryOrSubcategoryDto>>(productDal.GetProductsByCategory(categoryId),"Mehsullar kateqoriyaya gore siralandi");
        }
        public IDataResult<List<ProductByCategoryOrSubcategoryDto>> GetAllProductsBySubcategoryId(int subcategoryId)
        {
            return new SuccessDataResult<List<ProductByCategoryOrSubcategoryDto>>(productDal.GetProductsBySubCategory(subcategoryId), "Mehsullar podkateqoriyaya gore siralandi");

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
