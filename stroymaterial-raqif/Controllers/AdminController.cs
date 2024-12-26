using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace stroymaterial_raqif.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        [HttpGet("HelloMessage")]
        public async Task<IActionResult> Get()
        {
            return Ok(new { Message = "Salam Admin!" });
        }
    }
}
