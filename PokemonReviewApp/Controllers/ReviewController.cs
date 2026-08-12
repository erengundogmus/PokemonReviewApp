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
    public class ReviewController : Controller
    {
        private readonly IReviewInterface reviewInterface;
        private readonly IMapper mapper;

        public ReviewController(IReviewInterface reviewInterface, IMapper mapper)
        {
            this.reviewInterface = reviewInterface;
            this.mapper = mapper;

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

    }
}