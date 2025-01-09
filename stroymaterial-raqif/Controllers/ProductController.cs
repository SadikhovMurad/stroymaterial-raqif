using Business.Abstract;
using Entity.Concrete;
using Entity.DtoS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace stroymaterial_raqif.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet("GetAllProducts")]
        public IActionResult GetAll()
        {
            var result = _productService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("AddProduct")]
        public IActionResult AddProduct(ProductDto dto)
        {
            var result = _productService.Add(dto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("AddQuantity/{id}/{count}")]
        public IActionResult AddQuantity(Guid id,int count)
        {
            var result = _productService.AddStock(count,id);
            if (result.Success) 
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetAllProductsByCategory/{id}")]
        public IActionResult GetAllProductsByCategoryId(int id) 
        {
            var result = _productService.GetAllProductsByCategoryId(id);
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetAllProductBySubcategory/{id}")]
        public IActionResult GetAllProductsBySubcategoryId(int id)
        {
            var result = _productService.GetAllProductsBySubcategoryId(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("Delete/{id}")]
        public IActionResult DeleteProduct(Guid id)
        {
            var result = _productService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("Update/{id}")]
        public IActionResult UpdateProduct(Guid id,Product product)
        {
            var result = _productService.Update(id,product);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
