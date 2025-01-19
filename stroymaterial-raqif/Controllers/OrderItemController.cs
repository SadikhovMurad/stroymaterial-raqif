using Business.Abstract;
using Business.Concrete;
using Entity.DtoS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;

namespace stroymaterial_raqif.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {

        private readonly IOrderItemService _orderItemService;

        public OrderItemController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }


        [HttpGet("GetAllBasketItems")]
        public IActionResult GetAll() 
        {
            var result = _orderItemService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("AddToBasket")]
        public IActionResult AddOrderItemToShopBasket(OrderItemDto orderItemDto)
        {
            var result = _orderItemService.Add(orderItemDto);
            if(result.Success) 
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("AddQuantity/{id}")]
        public IActionResult AddOrderItemQuantity(int id)
        {
            var result = _orderItemService.AddQuantity(id);
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
