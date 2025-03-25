using Business.Abstract;
using Entity.Concrete;
using Entity.DtoS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace stroymaterial_raqif.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("AddItemToCart/{userId}")]
        public IActionResult AddItemToCart(Guid userId, Guid productId, int count = 1)
        {
            var result = _cartService.AddItemToCart(userId, productId, count);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("delete/{id}")]
        public IActionResult DeleteItemFromCart(int id)
        {
            var result = _cartService.DeleteItemFromCart(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _cartService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetCartByUserId/{userId}")]
        public IActionResult GetCartByUserId(Guid userId)
        {
            var result = _cartService.GetByUserId(userId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetAllCartItems/{userId}")]
        public IActionResult GetAllCartItemsByUserId(Guid userId)
        {
            var result = _cartService.GetAllCartItemsByUserId(userId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
