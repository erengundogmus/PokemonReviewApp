using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.InputDtos;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : Controller
    {
        private readonly IPermissionInterface _permissionRepository;
        private readonly IRoleInterface _roleRepository;

        public PermissionController(IPermissionInterface permissionRepository, IRoleInterface roleRepository)
        {
            _permissionRepository = permissionRepository;
            _roleRepository = roleRepository;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePermission([FromBody] PermissionDto request)
        {
            if (await _permissionRepository.PermissionExists(request.Name))
                return BadRequest("This permission already exists.");

            var permission = new Permission { Name = request.Name };
            var success = await _permissionRepository.CreatePermission(permission);

            if (!success)
                return StatusCode(500, "Error creating permission.");

            return Ok(new { message = "Permission created successfully." });
        }

        [HttpPost("assigntorole")]
        public async Task<IActionResult> AssignPermission([FromBody] RolePermissionDto request)
        {
            if (!await _roleRepository.RoleExistsById(request.RoleId))
                return NotFound("Role not found.");

            if (!await _permissionRepository.PermissionExistsById(request.PermissionId))
                return NotFound("Permission not found.");

            if (await _permissionRepository.RoleHasPermission(request.RoleId, request.PermissionId))
                return BadRequest("Role already has this permission.");

            var success = await _permissionRepository.AssignPermissionToRole(request.RoleId, request.PermissionId);

            if (!success)
                return StatusCode(500, "Error assigning permission.");

            return Ok(new { message = "Permission assigned to role successfully." });
        }

        [HttpDelete("removefromrole")]
        public async Task<IActionResult> RemovePermission([FromBody] RolePermissionDto request)
        {
            if (!await _roleRepository.RoleExistsById(request.RoleId))
                return NotFound("Role not found.");

            if (!await _permissionRepository.PermissionExistsById(request.PermissionId))
                return NotFound("Permission not found.");

            if (!await _permissionRepository.RoleHasPermission(request.RoleId, request.PermissionId))
                return BadRequest("Role does not have this permission.");

            var success = await _permissionRepository.RemovePermissionFromRole(request.RoleId, request.PermissionId);

            if (!success)
                return StatusCode(500, "Error removing permission.");

            return Ok(new { message = "Permission removed from role successfully." });
        }
    }
}