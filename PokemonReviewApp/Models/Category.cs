using PokemonReviewApp.Interfaces;

namespace PokemonReviewApp.Models
{
    public class Category : ISoftDelete
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }

        public ICollection<PokemonCategory> PokemonCategories { get; set; }
    }
}
