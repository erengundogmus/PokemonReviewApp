using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.OutputDtos;

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
        [ProducesResponseType(200, Type = typeof(IEnumerable<PokemonOutputDto>))]
        public IActionResult GetPokemons()
        {
            var pokemons = mapper.Map<List<PokemonOutputDto>>(pokemonInterface.GetPokemons());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pokemons);
        }


        [HttpGet("{pokeid}")]
        [ProducesResponseType(200, Type = typeof(PokemonOutputDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetPokemon(int pokeid)
        {
            if (!pokemonInterface.PokemonExists(pokeid))
                return NotFound();

            var pokemon = mapper.Map<PokemonOutputDto>(pokemonInterface.GetPokemon(pokeid));

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
        public IActionResult CreatePokemon([FromBody] PokemonInputDto pokemonCreate)
        {
            if (pokemonCreate == null)
                return BadRequest(ModelState);

            if (!ownerInterface.OwnerExists(pokemonCreate.OwnerId))
            {
                ModelState.AddModelError("", "Owner does not exist!");
                return NotFound(ModelState);
            }

            if (!categoryInterface.CategoryExists(pokemonCreate.CategoryId))
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

            if (!pokemonInterface.CreatePokemon(pokemonCreate.OwnerId, pokemonCreate.CategoryId, PokemonMap)) // veritabanına kayıt başarılı olmazsa bu hata
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }



        [HttpPut("{pokeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdatePokemon(int pokeId, [FromBody] PokemonInputDto updatedpokemon)
        {
            if (updatedpokemon == null)
                return BadRequest(ModelState);

            if (!pokemonInterface.PokemonExists(pokeId))
                return NotFound();

            // pokemonun olup olmadığını kontrol eder
            var existingPokemon = pokemonInterface.GetPokemons()                               /*şu an güncellenen pokemon hariç(category değişecekse buraya takılmamak için)*/
                .Where(p => p.Name.Trim().ToUpper() == updatedpokemon.Name.Trim().ToUpper() && p.Id != pokeId).FirstOrDefault(); // DEĞİŞİKLİK: updatedpokemon.Id yerine pokeId kullanıldı

            if (existingPokemon != null)
            {
                ModelState.AddModelError("", "Pokemon already exist.");
                return StatusCode(422, ModelState); //422 Unprocessable Entity hata kodu
            }

            if (!ownerInterface.OwnerExists(updatedpokemon.OwnerId))
            {
                ModelState.AddModelError("", "Owner does not exist");
                return NotFound(ModelState);
            }

            if (!categoryInterface.CategoryExists(updatedpokemon.CategoryId))
            {
                ModelState.AddModelError("", "Category does not exist");
                return NotFound(ModelState);
            }


            if (!ModelState.IsValid)
                return BadRequest();

            var pokemonMap = this.mapper.Map<Pokemon>(updatedpokemon);

            pokemonMap.Id = pokeId;

            if (!pokemonInterface.UpdatePokemon(pokemonMap))
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