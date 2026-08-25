using PokemonReviewApp.Interfaces;

namespace PokemonReviewApp.Models
{
    public class Review : ISoftDelete
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public decimal Rating { get; set; }

        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }

        public Reviewer Reviewer { get; set; }
        public Pokemon Pokemon { get; set; }
    }
}
