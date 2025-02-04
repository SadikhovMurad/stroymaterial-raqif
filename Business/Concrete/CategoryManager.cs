using AutoMapper;
using Business.Abstract;
using Business.Aspects;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspect.Validation;
using Core.Utilities.BusinessRules;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Concrete;
using Entity.DtoS;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal categoryDal;
        private readonly ISubcategoryDal subcategoryDal;
        private readonly IMapper _mapper;

        public CategoryManager(ICategoryDal categoryDal, IMapper mapper, ISubcategoryDal subcategoryDal)
        {
            this.categoryDal = categoryDal;
            _mapper = mapper;
            this.subcategoryDal = subcategoryDal;
        }

        [ValidationAspect(typeof(CategoryValidator))]
        [SecuredOperation("Admin")]
        public IResult Add(CategoryDto categoryDto)
        {
            var result = BusinessRules.Run(CheckIfCategoryNameExist(categoryDto.Name));
            if (result != null)
            {
                return result;
            }
            var category = _mapper.Map<Category>(categoryDto);

            categoryDal.Add(category);
            return new SuccessResult(Messages.CategoryAdded);
        }

        public IResult AddSubcategory(int categoryId, SubCategory subCategory)
        {
            var category = categoryDal.Get(c => c.Id == categoryId);
            category.SubCategories = new List<SubCategory>();
            category.SubCategories.Add(subCategory);
            subcategoryDal.Add(subCategory);
            return new SuccessResult(Messages.SubCategoryAdded);
        }

        public IResult Delete(int id)
        {
            var category = categoryDal.Get(x => x.Id == id);
            if (category == null)
            {
                return new ErrorResult(Messages.IdNotEntered);
            }
            categoryDal.Delete(category);
            return new SuccessResult(Messages.CategoryDeleted);
        }

        public IDataResult<List<Category>> GetAll()
        {
            return new SuccessDataResult<List<Category>>(categoryDal.GetAll(), Messages.CategoryListed);
        }

        public IDataResult<List<CategoryWithSubcategoriesDto>> GetAllCategoryWithSubcategories()
        {
            return new SuccessDataResult<List<CategoryWithSubcategoriesDto>>(categoryDal.categoryWithSubcategories(),Messages.CategoryListedWithSubcategories);
        }

        public IDataResult<Category> GetById(int id)
        {
            return new SuccessDataResult<Category>(categoryDal.Get(x => x.Id == id));
        }

        public IDataResult<Category> GetByName(string name)
        {
            return new SuccessDataResult<Category>(categoryDal.Get(x => x.Name == name));
        }


        [ValidationAspect(typeof(CategoryValidator))]
        public IResult Update(int id, CategoryDto? categoryDto)
        {
            if (id == null) return new ErrorResult(Messages.IdNullCategory);
            var dbcat = categoryDal.Get(x => x.Id == id);
            if (dbcat == null) return new ErrorResult(Messages.IdNotEntered);

            if (categoryDto == null) return new ErrorResult(Messages.IdNotEntered);

            var result = BusinessRules.Run(CheckIfCategoryNameExistForUpdate(dbcat.Id, categoryDto.Name));
            if (result != null)
            {
                return result;
            }

            dbcat.Name = categoryDto.Name;
            var category = _mapper.Map<Category>(categoryDto);
            category.Id = id;
            categoryDal.Update(category);
            return new SuccessResult(Messages.CategoryUpdated);     
        }


        #region Business Some Methods
        private IResult CheckIfCategoryNameExist(string categoryName)
        {
            var result = categoryDal.GetAll(x => x.Name == categoryName).Any();
            if (result)
            {
                return new ErrorResult(Messages.CategoryNameExisted);
            }
            return new SuccessResult();
        }

        private IResult CheckIfCategoryNameExistForUpdate(int id, string categoryName)
        {
            var result = categoryDal.GetAll(x => x.Name == categoryName && x.Id != id).Any();
            if (result)
            {
                return new ErrorResult(Messages.CategoryNameExisted);
            }
            return new SuccessResult();
        }

        #endregion
    }
}
