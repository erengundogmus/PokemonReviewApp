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

        public bool DeletePokemon(Pokemon pokemon)
        {
            pokemon.IsDeleted = true;
            pokemon.DeletedAt = DateTime.UtcNow;
            return Save();

        }

        public Pokemon GetPokemon(int id)
        {
            //lazy loading kapalı olduğu için veritabanından yüklenmiyor include kullanıyoruz
            return this.context.Pokemon.Where(p => p.Id == id).Include(p => p.PokemonOwners)
                .ThenInclude(po => po.Owner).Include(p => p.PokemonCategories).ThenInclude(pc => pc.Category).FirstOrDefault();
        }

        public Pokemon GetPokemon(string name)
        {
            return this.context.Pokemon.Where(p => p.Name == name).Include(p => p.PokemonOwners)
                .ThenInclude(po => po.Owner).Include(p => p.PokemonCategories).ThenInclude(pc => pc.Category).FirstOrDefault();
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
            return this.context.Pokemon.Include(p => p.PokemonOwners)
                .ThenInclude(po => po.Owner).Include(p => p.PokemonCategories).ThenInclude(pc => pc.Category).OrderBy(p => p.Id).ToList();
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

        public bool UpdatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            this.context.ChangeTracker.Clear();
            //eski kategori ve sahip
            var existingOwner = this.context.PokemonsOwners.Where(po => po.PokemonId == pokemon.Id).FirstOrDefault();
            var existingCategory = this.context.PokemonCategories.Where(pc => pc.PokemonId == pokemon.Id).FirstOrDefault();

            //eski kayıtları sil
            if (existingOwner != null)
                this.context.Remove(existingOwner);
            if (existingCategory != null)
                this.context.Remove(existingCategory);

            //yeni owner ve category nesnelerini buluyor
            var pokemonOwnerEntity = this.context.Owners.Where(a => a.Id == ownerId).FirstOrDefault();
            var categoryEntity = this.context.Categories.Where(a => a.Id == categoryId).FirstOrDefault();

            var pokemonOwner = new PokemonOwner()
            {
                Owner = pokemonOwnerEntity,
                Pokemon = pokemon,
            };
            this.context.Add(pokemonOwner);

            var pokemonCategory = new PokemonCategory()
            {
                Category = categoryEntity,
                Pokemon = pokemon,
            };
            this.context.Add(pokemonCategory);
            this.context.Update(pokemon);

            return Save();
        }



    }
}