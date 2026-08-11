using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class PokemonRepository : IPokemonInterface
    {
        private readonly DataContext context;

        public PokemonRepository(DataContext context)
        {
            this.context = context;
        }

        public ICollection<Pokemon> GetPokemons()
        {
             return this.context.Pokemon.OrderBy(p => p.Id).ToList();
        }
    }
}
