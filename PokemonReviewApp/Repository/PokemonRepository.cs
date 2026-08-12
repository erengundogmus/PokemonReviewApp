using Microsoft.EntityFrameworkCore;
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

        public bool CreatePokemon(Pokemon pokemon)
        {
            this.context.Add(pokemon);
            return Save();
        }

        public bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            var pokemonOwnerEntity = this.context.Owners.Where(a => a.Id == ownerId).FirstOrDefault();
            var category = this.context.Categories.Where(a => a.Id == categoryId).FirstOrDefault();
            //join table için ekledik
            var pokemonOwner = new PokemonOwner()
            {
                Owner = pokemonOwnerEntity,
                Pokemon = pokemon,
            };

            this.context.Add(pokemonOwner);
            
            //join table için ekledik
            var pokemonCategory = new PokemonCategory()
            {
                Category = category,
                Pokemon = pokemon,
            };

            this.context.Add(pokemonCategory);

            this.context.Add(pokemon);

            return Save();
        }

        public Pokemon GetPokemon(int id)
        {
            return this.context.Pokemon.Where(p => p.Id == id).FirstOrDefault();
        }

        public Pokemon GetPokemon(string name)
        {
            return this.context.Pokemon.Where(p => p.Name == name).FirstOrDefault();
        }

        public decimal GetPokemonRating(int pokeId)
        {
            var review = this.context.Reviews.Where(p => p.Pokemon.Id == pokeId);

            if (review.Count() <= 0)
                return 0;

            return ((decimal)review.Sum(r => r.Rating) / review.Count());
        }

        public ICollection<Pokemon> GetPokemons()
        {
             return this.context.Pokemon.OrderBy(p => p.Id).ToList();
        }

        public bool PokemonExists(int pokeId)
        {
            return this.context.Pokemon.Any(p => p.Id == pokeId);
        }

        public bool Save()
        {
            var saved = this.context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
