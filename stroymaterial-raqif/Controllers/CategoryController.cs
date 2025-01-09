using Business.Abstract;
using Entity.Concrete;
using Entity.DtoS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace stroymaterial_raqif.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("AllCategories")]
        public IActionResult GetAllCategories()
        {
            var result = _categoryService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("AllCategoriesWithSubcategories")]
        public IActionResult GetCategoryWithSubcategories() 
        {
            var result = _categoryService.GetAllCategoryWithSubcategories();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var result = _categoryService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("AddCategory")]
        public IActionResult AddCategory(CategoryDto category)
        {
            var result = _categoryService.Add(category);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("UpdateCategory/{id}")]
        public IActionResult Update(int id, CategoryDto categorydto)
        {
            var result = _categoryService.Update(id, categorydto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var result = _categoryService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
