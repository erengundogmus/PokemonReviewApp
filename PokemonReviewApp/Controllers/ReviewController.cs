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
    public class ReviewController : Controller
    {
        private readonly IReviewInterface reviewInterface;
        private readonly IMapper mapper;
        private readonly IPokemonInterface pokemonInterface;

        public ReviewController(IReviewInterface reviewInterface, IMapper mapper, IPokemonInterface pokemonInterface)
        {
            this.reviewInterface = reviewInterface;
            this.mapper = mapper;
            this.pokemonInterface = pokemonInterface;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewOutputDto>))]
        public IActionResult GetReviews()
        {
            var reviews = mapper.Map<List<ReviewOutputDto>>(reviewInterface.GetReviews());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviews);
        }

        [HttpGet("{reviewid}")]
        [ProducesResponseType(200, Type = typeof(ReviewOutputDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetReview(int reviewid)
        {
            if (!reviewInterface.ReviewExists(reviewid))
                return NotFound();

            var review = mapper.Map<ReviewOutputDto>(reviewInterface.GetReview(reviewid));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(review);
        }

        [HttpGet("pokemon/{pokeId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewOutputDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewsForAPokemon(int pokeId)
        {
            var reviews = mapper.Map<List<ReviewOutputDto>>(reviewInterface.GetReviewsOfAPokemon(pokeId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(reviews);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult CreateReview([FromBody] ReviewInputDto reviewCreate)
        {
            if (reviewCreate == null)
                return BadRequest(ModelState);

            var review = reviewInterface.GetReviews()
                .Where(c => c.Title.Trim().ToUpper() == reviewCreate.Title.Trim().ToUpper())
                .FirstOrDefault();

            if (review != null)
            {
                ModelState.AddModelError("", "Review already exists");
                return StatusCode(422, ModelState);
            }

            var pokemon = pokemonInterface.GetPokemon(reviewCreate.PokemonId);
            if (pokemon == null)
            {
                ModelState.AddModelError("", "Pokemon does not exist");
                return NotFound(ModelState);
            }

            var reviewer = reviewInterface.GetReviewer(reviewCreate.ReviewerId);
            if (reviewer == null)
            {
                ModelState.AddModelError("", "Reviewer does not exist");
                return NotFound(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewMap = mapper.Map<Review>(reviewCreate);
            reviewMap.Pokemon = pokemon;
            reviewMap.Reviewer = reviewer;

            if (!reviewInterface.CreateReview(reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{reviewId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReview(int reviewId, [FromBody] ReviewInputDto updatedreview)
        {
            if (updatedreview == null)
                return BadRequest(ModelState);

            if (!reviewInterface.ReviewExists(reviewId))
                return NotFound();

            var pokemon = pokemonInterface.GetPokemon(updatedreview.PokemonId);
            if (pokemon == null)
            {
                ModelState.AddModelError("", "Pokemon does not exist");
                return NotFound(ModelState);
            }

            var reviewer = reviewInterface.GetReviewer(updatedreview.ReviewerId);
            if (reviewer == null)
            {
                ModelState.AddModelError("", "Reviewer does not exist");
                return NotFound(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewMap = this.mapper.Map<Review>(updatedreview);
            reviewMap.Id = reviewId;

            reviewMap.Pokemon = pokemon;
            reviewMap.Reviewer = reviewer;

            if (!reviewInterface.UpdateReview(reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating review.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{reviewId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReview(int reviewId)
        {
            if (!reviewInterface.ReviewExists(reviewId))
            {
                return NotFound();
            }

            var reviewToDelete = reviewInterface.GetReview(reviewId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!reviewInterface.DeleteReview(reviewToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting review.");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }
    }
}