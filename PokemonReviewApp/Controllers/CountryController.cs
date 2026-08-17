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
    public class CountryController : Controller
    {
        private readonly ICountryInterface countryInterface;
        private readonly IMapper mapper;

        public CountryController(ICountryInterface countryInterface, IMapper mapper)
        {
            this.countryInterface = countryInterface;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CountryOutputDto>))]
        public IActionResult GetCountries()
        {
            var countries = mapper.Map<List<CountryOutputDto>>(countryInterface.GetCountries());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(countries);
        }

        [HttpGet("{countryId}")]
        [ProducesResponseType(200, Type = typeof(CountryOutputDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetCountry(int countryId)
        {
            if (!countryInterface.CountryExists(countryId))
                return NotFound("Country does not exist.");

            var country = mapper.Map<CountryOutputDto>(countryInterface.GetCountry(countryId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(country);
        }

        [HttpGet("owners/{ownerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(CountryOutputDto))]
        public IActionResult GetCountryOfAnOwner(int ownerId)
        {
            var country = countryInterface.GetCountryByOwner(ownerId);

            //owner'ın country'si var mı kontrol ediyoruz
            if (country == null)
                return NotFound("Owner does not exist or has no country.");

            var countryDto = mapper.Map<CountryOutputDto>(country);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(countryDto);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult CreateCountry([FromBody] CountryInputDto countryCreate)
        {
            if (countryCreate == null)
                return BadRequest(ModelState);

            var country = countryInterface.GetCountries()
                .Where(c => c.Name.Trim().ToUpper() == countryCreate.Name.Trim().ToUpper()).FirstOrDefault();

            if (country != null)
            {
                ModelState.AddModelError("", "Country already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var countryMap = mapper.Map<Country>(countryCreate);

            if (!countryInterface.CreateCountry(countryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        [HttpPut("{countryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCountry(int countryId, [FromBody] CountryInputDto updatedcountry)
        {
            if (updatedcountry == null)
                return BadRequest(ModelState);

            if (!countryInterface.CountryExists(countryId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var existingCountry = countryInterface.GetCountries()
                .Where(c => c.Name.Trim().ToUpper() == updatedcountry.Name.Trim().ToUpper() && c.Id != countryId).FirstOrDefault();

            if (existingCountry != null)
            {
                ModelState.AddModelError("", "Country already exists.");
                return StatusCode(422, ModelState);
            }

            var countryMap = this.mapper.Map<Country>(updatedcountry);
            countryMap.Id = countryId;

            if (!countryInterface.UpdateCountry(countryMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating country.");
                return StatusCode(500, ModelState);

            }

            return NoContent();

        }



        [HttpDelete("{countryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCountry(int countryId)
        {
            if (!countryInterface.CountryExists(countryId))
            {
                return NotFound();
            }

            var countryToDelete = countryInterface.GetCountry(countryId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!countryInterface.DeleteCountry(countryToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting country.");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }
    }
}