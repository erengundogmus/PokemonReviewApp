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
    public class CategoryController : Controller
    {
        private readonly ICategoryInterface categoryInterface;
        private readonly IMapper mapper;

        public CategoryController(ICategoryInterface categoryInterface, IMapper mapper)
        {
            this.categoryInterface = categoryInterface;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public IActionResult GetCategories()
        {
            var categories = mapper.Map<List<CategoryDto>>(categoryInterface.GetCategories());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(categories);
        }

        [HttpGet("{categoryId}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]
        public IActionResult GetCategory(int categoryId)
        {
            if (!categoryInterface.CategoryExists(categoryId))
                return NotFound();

            var category = mapper.Map<CategoryDto>(categoryInterface.GetCategory(categoryId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(category);
        }

        [HttpGet("pokemon/{categoryId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonByCategoryId(int categoryId)
        {
            var pokemons = mapper.Map<List<PokemonDto>>(categoryInterface.GetPokemonByCategory(categoryId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pokemons);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCategory([FromBody] CategoryDto categoryCreate)
        {
            if (categoryCreate == null)
                return BadRequest(ModelState);

            var category = categoryInterface.GetCategories()
                .Where(c => c.Name.Trim().ToUpper() == categoryCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (category != null)
            {
                ModelState.AddModelError("", "Category already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryMap = mapper.Map<Category>(categoryCreate);

            if (!categoryInterface.CreateCategory(categoryMap))
            {
                //modelstate key value olduğu için iki tane "" açtık
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        [HttpPut("{categoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCategory(int categoryID, [FromBody]CategoryDto updatedcategory)
        {
            if(updatedcategory == null)
                return BadRequest(ModelState);
            if(categoryID != updatedcategory.Id)
                return BadRequest(ModelState);
            if(!categoryInterface.CategoryExists(categoryID))
                return NotFound();
            if(!ModelState.IsValid)
                return BadRequest();

            /*   mapping yapmasaydık kullanacağımız yöntem
            Category category = new Category
            {
                Id = updatedcategory.Id,
                Name = updatedcategory.Name,
            };
            */

            var categoryMap = this.mapper.Map<Category>(updatedcategory);
            
            if (!categoryInterface.UpdateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating category.");
                return StatusCode(500, ModelState);

            }

            return NoContent();

        }


        [HttpDelete("{categoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCategory(int categoryId)
        {
            if (!categoryInterface.CategoryExists(categoryId))
            {
                return NotFound();
            }

            var categoryToDelete = categoryInterface.GetCategory(categoryId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!categoryInterface.DeleteCategory(categoryToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting category.");
            }

            return NoContent();

        }



    }
}