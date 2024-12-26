using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using stroymaterial_raqif.Identity;
using stroymaterial_raqif.Identity.JWT;
using stroymaterial_raqif.Models;

namespace stroymaterial_raqif.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private ITokenHelper _tokenHelper;

        public AuthController(UserManager<User> userManager, ITokenHelper tokenHelper)
        {
            _userManager = userManager;
            _tokenHelper = tokenHelper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new User
            {
                UserName = model.Email,
                Email = model.Email,
                Firstname = model.FirstName,
                Lastname = model.LastName
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
                return Ok(new { Message = "User registered successfully" });

            return BadRequest(result.Errors);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                return Unauthorized(new { Message = "Invalid credentials" });
            var roles = await _userManager.GetRolesAsync(user);
            var accessToken = _tokenHelper.CreateToken(user,roles.ToList());

            return Ok(new { Message = "Login successful",Token = accessToken.Token });
        }



        [HttpGet("getAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            List<GetAllUsersModel> users = new List<GetAllUsersModel>();
            foreach(var user in _userManager.Users)
            {
                var userModel = new GetAllUsersModel()
                {
                    Id = user.Id,
                    FirstName = user.Firstname,
                    LastName = user.Lastname,
                    Email = user.Email
                };
                users.Add(userModel);
            }
            return Ok(users);
        }


        [HttpGet("getRoles")]
        public async Task<IActionResult> GetRoles()
        {
            var user = await _userManager.FindByEmailAsync("murad@gmail.com");
            var role = await  _userManager.GetRolesAsync(user);
            string roleName="";
            foreach (var roleModel in role)
            {
                roleName = roleModel;
            }
            return Ok(roleName);
        }
    }
}
