using AssignmentDotNet.Service.JwtService;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JwtController : ControllerBase
    {
        private readonly IJwtService _jwtService;

        public JwtController(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            if (model.Username == "admin" && model.Password == "password")
            {
                var token = _jwtService.GenerateToken(model.Username);
                return Ok(new { Token = token });
            }
            return Unauthorized("Invalid credentials");
        }
    }
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
