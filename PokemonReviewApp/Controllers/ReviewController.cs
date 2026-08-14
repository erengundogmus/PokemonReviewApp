using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

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
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]

        public IActionResult GetReviews()
        {
            var reviews = mapper.Map<List<ReviewDto>>(reviewInterface.GetReviews());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviews);
        }


        [HttpGet("{reviewid}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]

        public IActionResult GetReview(int reviewid)
        {
            if (!reviewInterface.ReviewExists(reviewid))
                return NotFound();

            var review = mapper.Map<ReviewDto>(reviewInterface.GetReview(reviewid));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(review);

        }

        [HttpGet("pokemon/{pokeId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewsForAPokemon(int pokeId)
        {
            var reviews = mapper.Map<List<ReviewDto>>(reviewInterface.GetReviewsOfAPokemon(pokeId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(reviews);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReview([FromQuery] int reviewerId, [FromQuery] int pokeId, [FromBody] ReviewDto reviewCreate)
        {
            if (reviewCreate == null)
                return BadRequest(ModelState);

            var review = reviewInterface.GetReviews().Where(c => c.Title.Trim().ToUpper() == reviewCreate.Title.Trim().ToUpper()).FirstOrDefault();

            if (review != null)
            {
                ModelState.AddModelError("", "Review already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pokemon = pokemonInterface.GetPokemon(pokeId);
            if (pokemon == null)
            {
                ModelState.AddModelError("", "Pokemon does not exist");
                return NotFound(ModelState);
            }

            var reviewer = reviewInterface.GetReviewer(reviewerId);
            if (reviewer == null)
            {
                ModelState.AddModelError("", "Reviewer does not exist");
                return NotFound(ModelState);
            }

            var ReviewMap = mapper.Map<Review>(reviewCreate);
            ReviewMap.Pokemon = pokemon;
            ReviewMap.Reviewer = reviewer;

            if (!reviewInterface.CreateReview(ReviewMap))
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
        public IActionResult UpdateReview(int reviewId, [FromBody] ReviewDto updatedreview)
        {
            if (updatedreview == null)
                return BadRequest(ModelState);
            if (reviewId != updatedreview.Id)
                return BadRequest(ModelState);
            if (!reviewInterface.ReviewExists(reviewId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();

            var reviewMap = this.mapper.Map<Review>(updatedreview);

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