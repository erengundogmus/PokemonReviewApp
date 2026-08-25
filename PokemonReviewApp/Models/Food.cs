using PokemonReviewApp.Interfaces;

namespace PokemonReviewApp.Models
{
    public class Food : ISoftDelete
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Hp { get; set; }

        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }

        public ICollection<PokemonFood> PokemonFoods { get; set; }
    }
}