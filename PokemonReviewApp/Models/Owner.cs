using PokemonReviewApp.Interfaces;

namespace PokemonReviewApp.Models
{
    public class Owner : ISoftDelete
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gym { get; set; }

        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }

        public Country Country { get; set; }
        public ICollection<PokemonOwner> PokemonOwners { get; set; }
    }
}
