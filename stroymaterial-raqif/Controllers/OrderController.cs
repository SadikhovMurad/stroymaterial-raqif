using Business.Abstract;
using Entity.DtoS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace stroymaterial_raqif.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("AllOrders")]
        public IActionResult GetAll() 
        {
            var result = _orderService.GetAllOrderWithDetails();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetOrderById/{id}")]
        public IActionResult GetById(Guid id)
        {
            var result = _orderService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("CreateOrder/{userId}")]
        public IActionResult AddOrder(OrderDto orderDto, Guid userId)
        {
            var result = _orderService.Add(orderDto,userId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
