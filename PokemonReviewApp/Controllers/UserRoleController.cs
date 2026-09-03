using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.InputDtos;
using PokemonReviewApp.Interfaces;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : Controller
    {
        private readonly IUserRoleInterface _userRoleRepository;

        public UserRoleController(IUserRoleInterface userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }

        [HttpPost("addrole")]
        public async Task<IActionResult> AssignRole([FromBody] UserRoleDto request)
        {
            if (!await _userRoleRepository.UserExists(request.UserId))
                return NotFound("User not found.");

            if (!await _userRoleRepository.RoleExists(request.RoleId))
                return NotFound("Such a role does not exist in the system.");

            if (await _userRoleRepository.UserHasRole(request.UserId, request.RoleId))
                return BadRequest("User already has this role.");

            var success = await _userRoleRepository.AssignRole(request.UserId, request.RoleId);

            if (!success)
                return StatusCode(500, "An error occurred while assigning the role.");

            return Ok(new { message = "Role successfully assigned." });
        }

        [HttpDelete("removerole")]
        public async Task<IActionResult> RemoveRole([FromBody] UserRoleDto request)
        {
            if (!await _userRoleRepository.UserExists(request.UserId))
                return NotFound("User not found.");

            if (!await _userRoleRepository.UserHasRole(request.UserId, request.RoleId))
                return BadRequest("User does not have this role.");

            var success = await _userRoleRepository.RevokeRole(request.UserId, request.RoleId);

            if (!success)
                return StatusCode(500, "An error occurred while removing the role.");

            return Ok(new { message = "Role successfully removed." });
        }
    }
}