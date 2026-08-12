using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IReviewInterface
    {
        ICollection<Review> GetReviews();
        Review GetReview(int reviewId);
        ICollection<Review> GetReviewsOfAPokemon(int pokeId);
        bool ReviewExists(int reviewId);
        bool CreateReview(Review review);
        bool Save();
        Reviewer GetReviewer(int reviewerId);
    }
}
