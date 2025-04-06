using AssignmentDotNet.DTOs;
using AssignmentDotNet.Service.UserRoleService;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleService _userRoleService;

        public UserRoleController(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _userRoleService.GetAllRoles();
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid role ID.");
            }

            var role = await _userRoleService.GetRoleById(id);
            if (role == null)
            {
                return NotFound("Role not found.");
            }

            return Ok(role);
        }

        [HttpPost]
        public async Task<IActionResult> AddRole([FromBody] UserRoleDto roleDto)
        {
            if (roleDto == null)
            {
                return BadRequest("Invalid user role data.");
            }
            await _userRoleService.AddRole(roleDto);
            return Ok("User role added successfully.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(int id, [FromBody] UserRoleDto roleDto)
        {
            if (id <= 0 || roleDto == null || id != roleDto.Id)
            {
                return BadRequest("Invalid role data or ID mismatch.");
            }

            string result = await _userRoleService.UpdateRoleById(id, roleDto);

            if (result == "Role not found.")
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid role ID.");
            }
            await _userRoleService.DeleteRole(id);
            return Ok("User role deleted successfully.");
        }
    }
}
