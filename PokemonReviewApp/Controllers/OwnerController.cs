using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repository;

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

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Owner>))]

        public IActionResult GetOwners()
        {
            var owners = mapper.Map<List<OwnerDto>>(ownerInterface.GetOwners());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(owners);
        }

        [HttpGet("{ownerid}")]
        [ProducesResponseType(200, Type = typeof(Owner))]
        [ProducesResponseType(400)]

        public IActionResult GetOwner(int ownerid)
        {
            if (!ownerInterface.OwnerExists(ownerid))
                return NotFound();

            var owner = mapper.Map<List<OwnerDto>>(ownerInterface.GetOwner(ownerid));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(owner);
        }

        [HttpGet("{ownerId}/pokemon")]
        [ProducesResponseType(200, Type = typeof(Owner))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonByOwner(int ownerId)
        {
            if (!ownerInterface.OwnerExists(ownerId))
            {
                return NotFound();
            }

            var owner = mapper.Map<List<PokemonDto>>(ownerInterface.GetPokemonByOwner(ownerId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(owner);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateOwner([FromQuery] int countryId, [FromBody] OwnerDto ownerCreate)
        {
            if (ownerCreate == null)
                return BadRequest(ModelState);

            var owner = ownerInterface.GetOwners().Where(c => c.Name.Trim().ToUpper() == ownerCreate.Name.TrimEnd().ToUpper()).FirstOrDefault();

            if (owner != null)
            {
                ModelState.AddModelError("", "Owner already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ownerMap = mapper.Map<Owner>(ownerCreate);
            ownerMap.Country = countryInterface.GetCountry(countryId);

            if (!ownerInterface.CreateOwner(ownerMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        [HttpPut("{ownerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateOwner(int ownerId, [FromBody] OwnerDto updatedowner)
        {
            if (updatedowner == null)
                return BadRequest(ModelState);
            if (ownerId != updatedowner.Id)
                return BadRequest(ModelState);
            if (!ownerInterface.OwnerExists(ownerId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();

            var ownerMap = this.mapper.Map<Owner>(updatedowner);

            if (!ownerInterface.UpdateOwner(ownerMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating owner.");
                return StatusCode(500, ModelState);

            }

            return NoContent();

        }


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
            }

            return NoContent();

        }







    }
}
