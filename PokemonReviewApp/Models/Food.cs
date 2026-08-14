namespace PokemonReviewApp.Models
{
    public class Food
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Hp { get; set; }
        public ICollection<PokemonFood> PokemonFoods { get; set; }
    }
}