using PokemonReviewApp.Interfaces;

namespace PokemonReviewApp.Models
{
    public class Country : ISoftDelete
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }

        public ICollection<Owner> Owners { get; set; }
    }
}
