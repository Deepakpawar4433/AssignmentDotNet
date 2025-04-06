using AssignmentDotNet.DTOs;
using AssignmentDotNet.Service.UserService;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid user ID.");
            }

            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest("Invalid user data");
            }
            string result = await _userService.AddUser(userDto);
            if (result == "RoleId does not exist in the UserRole table." || result == "Email or PhoneNumber already exists.")
            {
                return BadRequest(result);
            }

            return Ok("User added successfully.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDto userDto)
        {
            if (id <= 0 || userDto == null || id != userDto.Id)
            {
                return BadRequest("Invalid user data or ID mismatch.");
            }
            string result = await _userService.UpdateUser(id, userDto);

            if (result == "User not found." || result == "RoleId does not exist in the UserRole table." || result == "Email or PhoneNumber already exists.")
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid user ID.");
            }
            await _userService.DeleteUser(id);
            return Ok("User deleted successfully");
        }
    }
}
