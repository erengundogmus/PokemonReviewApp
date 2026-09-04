using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.OutputDtos;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonFoodController : Controller
    {
        private readonly IFoodInterface foodInterface;
        private readonly IPokemonInterface pokemonInterface;
        private readonly IMapper mapper;

        public PokemonFoodController(IFoodInterface foodInterface, IPokemonInterface pokemonInterface, IMapper mapper)
        {
            this.foodInterface = foodInterface;
            this.pokemonInterface = pokemonInterface;
            this.mapper = mapper;
        }

        [Authorize(Roles = "PokemonsMenu")]
        [HttpGet("pokemon/{pokemonId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<FoodOutputDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetFoodsByPokemon(int pokemonId)
        {
            if (!pokemonInterface.PokemonExists(pokemonId))
                return NotFound("Pokemon does not exist.");

            var foods = mapper.Map<List<FoodOutputDto>>(foodInterface.GetFoodsByPokemon(pokemonId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(foods);
        }

        [Authorize(Roles = "PokemonFoodAdd")]
        [HttpPost("{foodId}/pokemon/{pokemonId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        public IActionResult AddFoodToPokemon(int foodId, int pokemonId)
        {
            if (!foodInterface.FoodExists(foodId)) return NotFound("Food does not exist");
            if (!pokemonInterface.PokemonExists(pokemonId)) return NotFound("Pokemon does not exist");

            if (foodInterface.PokemonCanEatFood(pokemonId, foodId))
            {
                ModelState.AddModelError("", "This pokemon can already eat this food.");
                return StatusCode(422, ModelState);
            }

            if (!foodInterface.AddFoodToPokemon(pokemonId, foodId))
            {
                ModelState.AddModelError("", "Something went wrong while linking.");
                return StatusCode(500, ModelState);
            }

            return Ok("Food successfully added to pokemon's menu");
        }

        [Authorize(Roles = "PokemonFoodRemove")]
        [HttpDelete("{foodId}/pokemon/{pokemonId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult RemoveFoodFromPokemon(int foodId, int pokemonId)
        {
            if (!foodInterface.FoodExists(foodId))
                return NotFound("Food does not exist");

            if (!pokemonInterface.PokemonExists(pokemonId))
                return NotFound("Pokemon does not exist");

            if (!foodInterface.PokemonCanEatFood(pokemonId, foodId))
            {
                ModelState.AddModelError("", "This pokemon does not have this food in its menu");
                return StatusCode(404, ModelState);
            }

            if (!foodInterface.RemoveFoodFromPokemon(pokemonId, foodId))
            {
                ModelState.AddModelError("", "Something went wrong while deleting food from pokemon");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}