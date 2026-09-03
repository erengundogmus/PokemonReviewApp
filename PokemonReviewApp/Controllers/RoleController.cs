using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.InputDtos;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.OutputDtos;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : Controller
    {
        private readonly IRoleInterface _roleRepository;

        public RoleController(IRoleInterface roleRepository)
        {
            _roleRepository = roleRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _roleRepository.GetRoles();

            var roleDtos = roles.Select(r => new RoleOutputDto
            {
                Id = r.Id,
                Name = r.Name
            }).ToList();

            return Ok(roleDtos);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] RoleDto request)
        {
            if (await _roleRepository.RoleExists(request.Name))
                return BadRequest("This role already exists.");

            var roleMap = new Role
            {
                Name = request.Name
            };

            var success = await _roleRepository.CreateRole(roleMap);

            if (!success)
                return StatusCode(500, "An error occurred while creating the role.");

            return Ok(new { message = "Role successfully created." });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(int id, [FromBody] RoleDto request)
        {
            var role = await _roleRepository.GetRole(id);
            if (role == null)
                return NotFound("Role not found.");

            if (await _roleRepository.RoleExists(request.Name))
                return BadRequest("A role with this name already exists.");

            role.Name = request.Name;

            var success = await _roleRepository.UpdateRole(role);

            if (!success)
                return StatusCode(500, "An error occurred while updating the role.");

            return Ok(new { message = "Role successfully updated." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var role = await _roleRepository.GetRole(id);
            if (role == null)
                return NotFound("Role not found.");

            var success = await _roleRepository.DeleteRole(role);

            if (!success)
                return StatusCode(500, "An error occurred while deleting the role.");

            return Ok(new { message = "Role successfully deleted." });
        }
    }
}