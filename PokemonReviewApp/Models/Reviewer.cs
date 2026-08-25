using PokemonReviewApp.Interfaces;

namespace PokemonReviewApp.Models
{
    public class Reviewer : ISoftDelete
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }

        public ICollection<Review> Reviews { get; set; }
    }
}
