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
        private readonly ICategoryInterface categoryInterface; // category'nin varlığını kontrol etmek için
        private readonly IOwnerInterface ownerInterface;  // owner'ın varlığını kontrol etmek için

        public PokemonController(IPokemonInterface pokemonInterface, ICategoryInterface categoryInterface, IOwnerInterface ownerInterface, IMapper mapper)
        {
            this.pokemonInterface = pokemonInterface;
            this.mapper = mapper;
            this.categoryInterface = categoryInterface; // category'nin varlığını kontrol etmek için
            this.ownerInterface = ownerInterface; // owner'ın varlığını kontrol etmek için
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


        [HttpGet("{pokeid}")]
        [ProducesResponseType(200, Type = typeof(Pokemon))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public IActionResult GetPokemon(int pokeid)
        {
            if (!pokemonInterface.PokemonExists(pokeid))
                return NotFound();

            var pokemon = mapper.Map<PokemonDto>(pokemonInterface.GetPokemon(pokeid));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pokemon);
        }

        [HttpGet("{pokeID}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
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
        [ProducesResponseType(404)]
        public IActionResult CreatePokemon([FromQuery] int ownerId, [FromQuery] int categoryId, [FromBody] PokemonDto pokemonCreate)
        {
            if (pokemonCreate == null)
                return BadRequest(ModelState);

            if (!ownerInterface.OwnerExists(ownerId))
            {
                ModelState.AddModelError("", "Owner does not exist!");
                return NotFound(ModelState);
            }

            if (!categoryInterface.CategoryExists(categoryId))
            {
                ModelState.AddModelError("", "Category does not exist!");
                return NotFound(ModelState);
            }
    
            var pokemon = pokemonInterface.GetPokemons().Where(c => c.Name.Trim().ToUpper() == pokemonCreate.Name.Trim().ToUpper()).FirstOrDefault();

            if (pokemon != null)
            {
                ModelState.AddModelError("", "Pokemon already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var PokemonMap = mapper.Map<Pokemon>(pokemonCreate);

            if (!pokemonInterface.CreatePokemon(ownerId, categoryId, PokemonMap)) // veritabanına kayıt başarılı olmazsa bu hata
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }



        [HttpPut("{pokemonId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdatePokemon(int pokemonId, [FromQuery] int ownerId, [FromQuery] int categoryId, [FromBody] PokemonDto updatedpokemon)
        {
            if (updatedpokemon == null)
                return BadRequest(ModelState);
            if (pokemonId != updatedpokemon.Id)
                return BadRequest(ModelState);
            if (!pokemonInterface.PokemonExists(pokemonId))
                return NotFound();
            
            // pokemonun olup olmadığını kontrol eder
            var existingPokemon = pokemonInterface.GetPokemons()                               /*şu an güncellenen pokemon hariç(category değişecekse buraya takılmamak için)*/  
                .Where(p => p.Name.Trim().ToUpper() == updatedpokemon.Name.Trim().ToUpper() && p.Id != pokemonId).FirstOrDefault();

            if (existingPokemon != null)
            {
                ModelState.AddModelError("", "Pokemon already exist.");
                return StatusCode(422, ModelState); //422 Unprocessable Entity hata kodu
            }

            if (!ownerInterface.OwnerExists(ownerId))
            {
                ModelState.AddModelError("", "Owner does not exist");
                return NotFound(ModelState);
            }

            if (!categoryInterface.CategoryExists(categoryId))
            {
                ModelState.AddModelError("", "Category does not exist");
                return NotFound(ModelState);
            }


            if (!ModelState.IsValid)
                return BadRequest();

            var pokemonMap = this.mapper.Map<Pokemon>(updatedpokemon);

            if (!pokemonInterface.UpdatePokemon(ownerId, categoryId, pokemonMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating pokemon.");
                return StatusCode(500, ModelState);

            }

            return NoContent();

        }


        [HttpDelete("{pokemonId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeletePokemon(int pokemonId)
        {
            if (!pokemonInterface.PokemonExists(pokemonId))
            {
                return NotFound();
            }

            var pokemonToDelete = pokemonInterface.GetPokemon(pokemonId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!pokemonInterface.DeletePokemon(pokemonToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting pokemon.");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

    }

}
