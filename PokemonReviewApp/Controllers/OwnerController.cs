using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.InputDtos;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.OutputDtos;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : Controller
    {
        private readonly IOwnerInterface ownerInterface;
        private readonly IMapper mapper;
        private readonly ICountryInterface countryInterface;

        public OwnerController(IOwnerInterface ownerInterface, IMapper mapper, ICountryInterface countryInterface)
        {
            this.ownerInterface = ownerInterface;
            this.mapper = mapper;
            this.countryInterface = countryInterface;
        }

        [Authorize(Roles = "OwnerList")]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<OwnerOutputDto>))]
        public IActionResult GetOwners()
        {
            var owners = mapper.Map<List<OwnerOutputDto>>(ownerInterface.GetOwners());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(owners);
        }

        [Authorize(Roles = "OwnerDetail")]
        [HttpGet("{ownerId}")]
        [ProducesResponseType(200, Type = typeof(OwnerOutputDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetOwner(int ownerId)
        {
            if (!ownerInterface.OwnerExists(ownerId))
                return NotFound();

            var owner = mapper.Map<OwnerOutputDto>(ownerInterface.GetOwner(ownerId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(owner);
        }

        [Authorize(Roles = "OwnerDetail")]
        [HttpGet("{ownerId}/pokemon")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PokemonOutputDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetPokemonByOwner(int ownerId)
        {
            if (!ownerInterface.OwnerExists(ownerId))
            {
                return NotFound("Owner does not exist.");
            }

            var ownersPokemons = mapper.Map<List<PokemonOutputDto>>(ownerInterface.GetPokemonByOwner(ownerId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(ownersPokemons);
        }

        [Authorize(Roles = "OwnerCreate")]
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult CreateOwner([FromBody] OwnerInputDto ownerCreate)
        {
            if (ownerCreate == null)
                return BadRequest(ModelState);

            var owner = ownerInterface.GetOwners().Where(c => c.Name.Trim().ToUpper() == ownerCreate.Name.Trim().ToUpper()).FirstOrDefault();

            if (owner != null)
            {
                ModelState.AddModelError("", "Owner already exists");
                return StatusCode(422, ModelState);
            }

            if (!countryInterface.CountryExists(ownerCreate.CountryId))
            {
                ModelState.AddModelError("", "Country does not exist.");
                return StatusCode(404, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ownerMap = mapper.Map<Owner>(ownerCreate);
            ownerMap.CountryId = ownerCreate.CountryId;
            if (!ownerInterface.CreateOwner(ownerMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [Authorize(Roles = "OwnerUpdate")]
        [HttpPut("{ownerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateOwner(int ownerId, [FromBody] OwnerInputDto updatedowner)
        {
            if (updatedowner == null)
                return BadRequest(ModelState);

            if (!ownerInterface.OwnerExists(ownerId))
                return NotFound();

            var existingOwner = ownerInterface.GetOwners()
            .Where(o => o.Name.Trim().ToUpper() == updatedowner.Name.Trim().ToUpper() && o.Id != ownerId).FirstOrDefault();

            if (existingOwner != null)
            {
                ModelState.AddModelError("", "Owner already exist.");
                return StatusCode(422, ModelState);
            }

            if (!countryInterface.CountryExists(updatedowner.CountryId))
            {
                ModelState.AddModelError("", "Country does not exist.");
                return NotFound(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest();

            var ownerMap = this.mapper.Map<Owner>(updatedowner);
            ownerMap.Id = ownerId;
            ownerMap.CountryId = updatedowner.CountryId;

            if (!ownerInterface.UpdateOwner(ownerMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating owner.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [Authorize(Roles = "OwnerDelete")]
        [HttpDelete("{ownerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteOwner(int ownerId)
        {
            if (!ownerInterface.OwnerExists(ownerId))
            {
                return NotFound();
            }

            var ownerToDelete = ownerInterface.GetOwner(ownerId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!ownerInterface.DeleteOwner(ownerToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting owner.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}