using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : Controller
    {
        private readonly IPokemonInterface pokemonInterface;
        private readonly IMapper mapper;

        public PokemonController(IPokemonInterface pokemonInterface, IMapper mapper)
        {
            this.pokemonInterface = pokemonInterface;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]

        public IActionResult GetPokemons()
        {
            var pokemons = mapper.Map<List<PokemonDto>>(pokemonInterface.GetPokemons());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pokemons);
        }


        [HttpGet("pokeid")]
        [ProducesResponseType(200, Type = typeof(Pokemon))]
        [ProducesResponseType(400)]

        public IActionResult GetPokemon(int pokeid)
        {
            if (!pokemonInterface.PokemonExists(pokeid))
                return NotFound();

            var pokemon = mapper.Map<PokemonDto>(pokemonInterface.GetPokemon(pokeid));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pokemon);
        }

        [HttpGet("pokeID/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonRating(int pokeID) 
        {
            if (!pokemonInterface.PokemonExists(pokeID))
                return NotFound();

            var rating = pokemonInterface.GetPokemonRating(pokeID);

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(rating);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePokemon([FromQuery] int ownerId, [FromQuery] int categoryId, [FromBody] PokemonDto pokemonCreate)
        {
            if (pokemonCreate == null)
                return BadRequest(ModelState);

            var pokemon = pokemonInterface.GetPokemons().Where(c => c.Name.Trim().ToUpper() == pokemonCreate.Name.TrimEnd().ToUpper()).FirstOrDefault();

            if (pokemon != null)
            {
                ModelState.AddModelError("", "Pokemon already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var PokemonMap = mapper.Map<Pokemon>(pokemonCreate);

            if (!pokemonInterface.CreatePokemon(ownerId, categoryId, PokemonMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }



    }

}
