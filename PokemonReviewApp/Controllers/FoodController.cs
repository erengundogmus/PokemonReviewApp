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

    }
}