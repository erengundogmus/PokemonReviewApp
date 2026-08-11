using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : Controller
    {
        private readonly IPokemonInterface pokemonInterface;

        public PokemonController(IPokemonInterface pokemonInterface)
        {
            this.pokemonInterface = pokemonInterface;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]

        public IActionResult GetPokemons()
        {
            var pokemons = pokemonInterface.GetPokemons();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pokemons);
        }
    }
}
