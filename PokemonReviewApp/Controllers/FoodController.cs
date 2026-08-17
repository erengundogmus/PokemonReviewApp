using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.InputDtos;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.OutputDtos;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : Controller
    {
        private readonly IFoodInterface foodInterface;
        private readonly IPokemonInterface pokemonInterface;
        private readonly IMapper mapper;

        public FoodController(IFoodInterface foodInterface, IPokemonInterface pokemonInterface, IMapper mapper)
        {
            this.foodInterface = foodInterface;
            this.pokemonInterface = pokemonInterface;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<FoodOutputDto>))]
        public IActionResult GetFoods()
        {
            var foods = mapper.Map<List<FoodOutputDto>>(foodInterface.GetFoods());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(foods);
        }

        [HttpGet("{foodId}")]
        [ProducesResponseType(200, Type = typeof(FoodOutputDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetFood(int foodId)
        {
            if (!foodInterface.FoodExists(foodId))
                return NotFound();

            var food = mapper.Map<FoodOutputDto>(foodInterface.GetFood(foodId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(food);
        }

        [HttpGet("pokemon/{pokeId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<FoodOutputDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetFoodsByPokemon(int pokeId)
        {
            if (!pokemonInterface.PokemonExists(pokeId))
                return NotFound("Pokemon does not exist.");

            var foods = mapper.Map<List<FoodOutputDto>>(foodInterface.GetFoodsByPokemon(pokeId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(foods);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        public IActionResult CreateFood([FromBody] FoodInputDto foodCreate)
        {
            if (foodCreate == null)
                return BadRequest(ModelState);

            var food = foodInterface.GetFoods()
                .Where(f => f.Name.Trim().ToUpper() == foodCreate.Name.Trim().ToUpper()).FirstOrDefault();

            if (food != null)
            {
                ModelState.AddModelError("", "Food already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);
            var foodMap = mapper.Map<Food>(foodCreate);

            if (!foodInterface.CreateFood(foodMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created.");
        }

        [HttpPost("{foodId}/pokemon/{pokeId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        public IActionResult AddFoodToPokemon(int foodId, int pokeId)
        {
            if (!foodInterface.FoodExists(foodId)) return NotFound("Food does not exist");
            if (!pokemonInterface.PokemonExists(pokeId)) return NotFound("Pokemon does not exist");

            if (foodInterface.PokemonCanEatFood(pokeId, foodId))
            {
                ModelState.AddModelError("", "This pokemon can already eat this food.");
                return StatusCode(422, ModelState);
            }

            if (!foodInterface.AddFoodToPokemon(pokeId, foodId))
            {
                ModelState.AddModelError("", "Something went wrong while linking.");
                return StatusCode(500, ModelState);
            }

            return Ok("Food successfully added to pokemon's menu");
        }

        [HttpPut("{foodId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateFood(int foodId, [FromBody] FoodInputDto updatedFood)
        {
            if (updatedFood == null) return BadRequest(ModelState);
            if (!foodInterface.FoodExists(foodId)) return NotFound();

            var existingFood = foodInterface.GetFoods()
                .Where(f => f.Name.Trim().ToUpper() == updatedFood.Name.Trim().ToUpper() && f.Id != foodId).FirstOrDefault();

            if (existingFood != null)
            {
                ModelState.AddModelError("", "Food already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest();

            var foodMap = this.mapper.Map<Food>(updatedFood);
            foodMap.Id = foodId;

            if (!foodInterface.UpdateFood(foodMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating food");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{foodId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteFood(int foodId)
        {
            if (!foodInterface.FoodExists(foodId))
                return NotFound();

            var foodToDelete = foodInterface.GetFood(foodId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!foodInterface.DeleteFood(foodToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting food");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{foodId}/pokemon/{pokeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult RemoveFoodFromPokemon(int foodId, int pokeId)
        {
            if (!foodInterface.FoodExists(foodId))
                return NotFound("Food does not exist");

            if (!pokemonInterface.PokemonExists(pokeId))
                return NotFound("Pokemon does not exist");

            if (!foodInterface.PokemonCanEatFood(pokeId, foodId))
            {
                ModelState.AddModelError("", "This pokemon does not have this food in its menu");
                return StatusCode(404, ModelState);
            }

            if (!foodInterface.RemoveFoodFromPokemon(pokeId, foodId))
            {
                ModelState.AddModelError("", "Something went wrong while deleting food from pokemon");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}