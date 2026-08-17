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
    public class ReviewerController : Controller
    {
        private readonly IReviewerInterface reviewerInterface;
        private readonly IMapper mapper;

        public ReviewerController(IReviewerInterface reviewerInterface, IMapper mapper)
        {
            this.reviewerInterface = reviewerInterface;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewerOutputDto>))]
        public IActionResult GetReviewers()
        {
            var reviewers = mapper.Map<List<ReviewerOutputDto>>(reviewerInterface.GetReviewers());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviewers);
        }

        [HttpGet("{reviewerId}")]
        [ProducesResponseType(200, Type = typeof(ReviewerOutputDto))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewer(int reviewerId)
        {
            if (!reviewerInterface.ReviewerExists(reviewerId))
                return NotFound();

            var reviewer = mapper.Map<ReviewerOutputDto>(reviewerInterface.GetReviewer(reviewerId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviewer);
        }

        [HttpGet("{reviewerId}/reviews")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewOutputDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetReviewsByAReviewer(int reviewerId)
        {
            if (!reviewerInterface.ReviewerExists(reviewerId))
                return NotFound();

            var reviews = mapper.Map<List<ReviewOutputDto>>(reviewerInterface.GetReviewsByReviewer(reviewerId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviews);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        public IActionResult CreateReviewer([FromBody] ReviewerInputDto reviewerCreate)
        {
            if (reviewerCreate == null)
                return BadRequest(ModelState);

            var reviewer = reviewerInterface.GetReviewers()
                .Where(c => c.FirstName.Trim().ToUpper() == reviewerCreate.FirstName.Trim().ToUpper() && c.LastName.Trim().ToUpper() == reviewerCreate.LastName.Trim().ToUpper()).FirstOrDefault();

            if (reviewer != null)
            {
                ModelState.AddModelError("", "Reviewer already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ReviewerMap = mapper.Map<Reviewer>(reviewerCreate);

            if (!reviewerInterface.CreateReviewer(ReviewerMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{reviewerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        public IActionResult UpdateReviewer(int reviewerId, [FromBody] ReviewerInputDto updatedreviewer)
        {
            if (updatedreviewer == null)
                return BadRequest(ModelState);

            if (!reviewerInterface.ReviewerExists(reviewerId))
                return NotFound();

            var existingReviewer = reviewerInterface.GetReviewers()
                .Where(r => r.FirstName.Trim().ToUpper() == updatedreviewer.FirstName.Trim().ToUpper()
                && r.LastName.Trim().ToUpper() == updatedreviewer.LastName.Trim().ToUpper() && r.Id != reviewerId).FirstOrDefault();

            if (existingReviewer != null)
            {
                ModelState.AddModelError("", "Reviewer already exists.");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewerMap = this.mapper.Map<Reviewer>(updatedreviewer);
            reviewerMap.Id = reviewerId;

            if (!reviewerInterface.UpdateReviewer(reviewerMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating reviewer.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{reviewerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReviewer(int reviewerId)
        {
            if (!reviewerInterface.ReviewerExists(reviewerId))
            {
                return NotFound();
            }

            var reviewerToDelete = reviewerInterface.GetReviewer(reviewerId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!reviewerInterface.DeleteReviewer(reviewerToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting reviewer.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}