using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Business.RelationshipHelper;
using Business.ValidationRules.FluentValidation;
using Core.Aspect.Validation;
using Core.Utilities.BusinessRules;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Concrete;
using Entity.DtoS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Business.Concrete
{
    public class SubcategoryManager : ISubcategoryService
    {
        private readonly ISubcategoryDal _subCategoryDal;
        private readonly ICategoryDal _categoryDal;
        private readonly IMapper _mapper;

        public SubcategoryManager(ISubcategoryDal subCategoryDal, IMapper mapper, ICategoryDal categoryDal)
        {
            _subCategoryDal = subCategoryDal;
            _mapper = mapper;
            _categoryDal = categoryDal;
        }

        [ValidationAspect(typeof(SubcategoryValidator))]
        public IResult Add(SubcategoryDto subcategoryDto)
        {
            var result = BusinessRules.Run(CheckIfSubCategoryNameExist(subcategoryDto.Name));
            if (result != null)
            {
                return result;
            }
            var category = _categoryDal.Get(c => c.Id == subcategoryDto.CategoryId);
            if (category == null)
            {
                return new ErrorResult("Kateqoriya tapilmadi");
            }
            var subCategory = _mapper.Map<SubCategory>(subcategoryDto);
            if (category.SubCategories == null)
            {
                category.SubCategories = new List<SubCategory>();
            }
            category.SubCategories.Add(subCategory);
            _subCategoryDal.Add(subCategory);
            return new SuccessResult(Messages.SubCategoryAdded);
        }

        public IResult Delete(int id)
        {
            var subCategory = _subCategoryDal.Get(x => x.Id == id);
            if (subCategory == null)
            {
                return new ErrorResult(Messages.IdNotEnteredSub);
            }
            _subCategoryDal.Delete(subCategory);
            return new SuccessResult(Messages.SubCategoryDeleted);
        }

        public IDataResult<List<SubCategory>> GetAll()
        {
            return new SuccessDataResult<List<SubCategory>>(_subCategoryDal.GetAll(), Messages.SubCategoryListed);
        }

        public IDataResult<SubCategory> GetById(int id)
        {
            return new SuccessDataResult<SubCategory>(_subCategoryDal.Get(x => x.Id == id));

        }

        public IDataResult<SubCategory> GetByName(string name)
        {
            return new SuccessDataResult<SubCategory>(_subCategoryDal.Get(x => x.Name == name));
        }

        public IDataResult<List<SubcategoryWithCategoryDto>> GetSubcategoryWithCategoryName()
        {
            throw new NotImplementedException();
        }

        [ValidationAspect(typeof(SubcategoryValidator))]
        public IResult Update(SubCategory? subCategory)
        {
            var result = BusinessRules.Run(CheckIfSubCategoryNameExistForUpdate(subCategory.Name, subCategory.Id));
            if (result != null)
            {
                return result;
            }
            _subCategoryDal.Update(subCategory);
            return new SuccessResult(Messages.SubCategoryUpdated);
        }

        #region Business Code
        private IResult CheckIfSubCategoryNameExist(string subCategoryName)
        {
            var result = _subCategoryDal.GetAll(x => x.Name == subCategoryName).Any();
            if (result)
            {
                return new ErrorResult(Messages.SubCategoryNameExisted);
            }
            return new SuccessResult();
        }

        private IResult CheckIfSubCategoryNameExistForUpdate(string subCategoryName, int id)
        {
            var result = _subCategoryDal.GetAll(x => x.Name == subCategoryName && x.Id != id).Any();
            if (result)
            {
                return new ErrorResult(Messages.SubCategoryNameExisted);
            }
            return new SuccessResult();
        }
        #endregion
    }
}
