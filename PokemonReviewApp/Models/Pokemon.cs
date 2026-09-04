using PokemonReviewApp.Interfaces;

namespace PokemonReviewApp.Models
{
    public class Pokemon : ISoftDelete
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public byte[]? Photo { get; set; }

        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }


        public ICollection<Review> Reviews { get; set; }
        public ICollection<PokemonOwner> PokemonOwners { get; set; }
        public ICollection<PokemonCategory> PokemonCategories { get; set; }
        public ICollection<PokemonFood> PokemonFoods { get; set; }
    }
}
