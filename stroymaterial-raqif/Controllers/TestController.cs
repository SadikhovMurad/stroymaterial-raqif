using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace stroymaterial_raqif.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("Hey")]
        public IActionResult test()
        {
            var user = HttpContext.User;
            if (user.Identity.IsAuthenticated)
            {
                return Ok(new { Message = "Authentication işləyir!", User = user.Identity.Name });
            }
            return Unauthorized(new { Message = "JWT Authentication işləmir!" });
        }
    }
}
