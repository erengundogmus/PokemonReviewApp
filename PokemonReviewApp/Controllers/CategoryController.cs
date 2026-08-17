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
        [ProducesResponseType(200, Type = typeof(IEnumerable<CategoryOutputDto>))]
        public IActionResult GetCategories()
        {
            var categories = mapper.Map<List<CategoryOutputDto>>(categoryInterface.GetCategories());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(categories);
        }

        [HttpGet("{categoryId}")]
        [ProducesResponseType(200, Type = typeof(CategoryOutputDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetCategory(int categoryId)
        {
            if (!categoryInterface.CategoryExists(categoryId))
                return NotFound();

            var category = mapper.Map<CategoryOutputDto>(categoryInterface.GetCategory(categoryId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(category);
        }

        [HttpGet("pokemon/{categoryId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PokemonOutputDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetPokemonByCategoryId(int categoryId)
        {
            if (!categoryInterface.CategoryExists(categoryId)) //category yoksa boş olduğunu bildirir
                return NotFound("Category does not exist.");

            var pokemons = mapper.Map<List<PokemonOutputDto>>(categoryInterface.GetPokemonByCategory(categoryId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pokemons);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCategory([FromBody] CategoryInputDto categoryCreate)
        {
            if (categoryCreate == null)
                return BadRequest(ModelState);

            var category = categoryInterface.GetCategories()
                .Where(c => c.Name.Trim().ToUpper() == categoryCreate.Name.Trim().ToUpper())
                .FirstOrDefault();

            if (category != null)
            {
                ModelState.AddModelError("", "Category already exist");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryMap = mapper.Map<Category>(categoryCreate);

            if (!categoryInterface.CreateCategory(categoryMap))
            {
                //modelstate key value olduğu için iki tane "" açtık
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created.");
        }


        [HttpPut("{categoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCategory(int categoryId, [FromBody] CategoryInputDto updatedcategory)
        {
            if (updatedcategory == null)
                return BadRequest(ModelState);

            if (!categoryInterface.CategoryExists(categoryId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var existingCategory = categoryInterface.GetCategories()
            .Where(c => c.Name.Trim().ToUpper() == updatedcategory.Name.Trim().ToUpper() && c.Id != categoryId).FirstOrDefault();

            if (existingCategory != null)
            {
                ModelState.AddModelError("", "Category already exist.");
                return StatusCode(422, ModelState); //422 Unprocessable Entity hata kodu
            }

            /*   mapping yapmasaydık kullanacağımız yöntem
            Category category = new Category
            {
                Id = updatedcategory.Id,
                Name = updatedcategory.Name,
            };
            */

            var categoryMap = this.mapper.Map<Category>(updatedcategory);
            categoryMap.Id = categoryId;

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
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }
    }
}