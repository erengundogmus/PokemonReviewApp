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

        public OwnerController(IOwnerInterface ownerInterface, IMapper mapper)
        {
            this.ownerInterface = ownerInterface;
            this.mapper = mapper;
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




    }
}
