using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.AuditLogs;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class PokemonRepository : IPokemonInterface
    {
        private readonly DataContext context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PokemonRepository(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this._httpContextAccessor = httpContextAccessor;
        }

        private string GetCurrentUser()
        {
            return _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "System";
        }

        public bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            using var transaction = this.context.Database.BeginTransaction();
            try
            {
                this.context.ChangeTracker.Clear();

                this.context.Pokemons.Add(pokemon);

                if (Save())
                {
                    var pokemonOwnerEntity = this.context.Owners.Where(a => a.Id == ownerId).FirstOrDefault();
                    var category = this.context.Categories.Where(a => a.Id == categoryId).FirstOrDefault();

                    var pokemonOwner = new PokemonOwner()
                    {
                        Owner = pokemonOwnerEntity,
                        Pokemon = pokemon,
                    };

                    this.context.Add(pokemonOwner);

                    var pokemonCategory = new PokemonCategory()
                    {
                        Category = category,
                        Pokemon = pokemon,
                    };

                    this.context.Add(pokemonCategory);

                    var pokemonLog = new PokemonLog
                    {
                        Action = "POST",
                        Status = "Active",
                        PerformedBy = GetCurrentUser(),
                        PokemonId = pokemon.Id,
                        NewName = pokemon.Name,
                        NewBirthDate = pokemon.BirthDate,
                        NewOwnerId = ownerId,
                        NewCategoryId = categoryId,
                        LoggedAt = DateTime.UtcNow
                    };

                    this.context.PokemonLog.Add(pokemonLog);

                    if (Save())
                    {
                        transaction.Commit();
                        return true;
                    }
                }

                transaction.Rollback();
                return false;
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public bool DeletePokemon(Pokemon pokemon)
        {
            using var transaction = this.context.Database.BeginTransaction();
            try
            {
                pokemon.IsDeleted = true;
                pokemon.DeletedAt = DateTime.UtcNow;
                this.context.Pokemons.Update(pokemon);

                var pokemonLog = new PokemonLog
                {
                    Action = "DELETE",
                    Status = "Deleted",
                    PerformedBy = GetCurrentUser(),
                    PokemonId = pokemon.Id,
                    NewName = pokemon.Name,
                    NewBirthDate = pokemon.BirthDate,
                    LoggedAt = DateTime.UtcNow
                };

                this.context.PokemonLog.Add(pokemonLog);

                if (Save())
                {
                    transaction.Commit();
                    return true;
                }

                transaction.Rollback();
                return false;
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public Pokemon GetPokemon(int id)
        {
            //lazy loading kapalı olduğu için veritabanından yüklenmiyor include kullanıyoruz
            return this.context.Pokemons.Where(p => p.Id == id && !p.IsDeleted).Include(p => p.PokemonOwners)
                .ThenInclude(po => po.Owner).Include(p => p.PokemonCategories).ThenInclude(pc => pc.Category).FirstOrDefault();
        }

        public Pokemon GetPokemon(string name)
        {
            return this.context.Pokemons.Where(p => p.Name == name && !p.IsDeleted).Include(p => p.PokemonOwners)
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
            return this.context.Pokemons.Where(p => !p.IsDeleted).Include(p => p.PokemonOwners)
                .ThenInclude(po => po.Owner).Include(p => p.PokemonCategories).ThenInclude(pc => pc.Category).OrderBy(p => p.Id).ToList();
        }

        public bool PokemonExists(int pokeId)
        {
            return this.context.Pokemons.Any(p => p.Id == pokeId && !p.IsDeleted);
        }

        public bool Save()
        {
            var saved = this.context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            using var transaction = this.context.Database.BeginTransaction();
            try
            {
                this.context.ChangeTracker.Clear();

                var existingOwnerRelation = this.context.PokemonsOwners.Where(po => po.PokemonId == pokemon.Id).FirstOrDefault();
                var existingCategoryRelation = this.context.PokemonCategories.Where(pc => pc.PokemonId == pokemon.Id).FirstOrDefault();

                var existingPokemon = this.context.Pokemons.FirstOrDefault(p => p.Id == pokemon.Id);

                if (existingPokemon != null)
                {
                    // Dışarıdan gelen yeni değerleri mevcut entity'ye aktarıyoruz (Tracking çakışmasını önlemek için)
                    existingPokemon.Name = pokemon.Name;
                    existingPokemon.BirthDate = pokemon.BirthDate;

                    var pokemonLog = new PokemonLog
                    {
                        Action = "PUT",
                        Status = "Updated",
                        PerformedBy = GetCurrentUser(),
                        PokemonId = pokemon.Id,
                        NewName = pokemon.Name,
                        NewBirthDate = pokemon.BirthDate,
                        NewOwnerId = ownerId,
                        NewCategoryId = categoryId,
                        LoggedAt = DateTime.UtcNow
                    };

                    this.context.PokemonLog.Add(pokemonLog);
                }

                if (existingOwnerRelation != null)
                    this.context.Remove(existingOwnerRelation);
                if (existingCategoryRelation != null)
                    this.context.Remove(existingCategoryRelation);

                var pokemonOwnerEntity = this.context.Owners.Where(a => a.Id == ownerId).FirstOrDefault();
                var categoryEntity = this.context.Categories.Where(a => a.Id == categoryId).FirstOrDefault();

                var pokemonOwner = new PokemonOwner()
                {
                    Owner = pokemonOwnerEntity,
                    Pokemon = existingPokemon,
                };
                this.context.Add(pokemonOwner);

                var pokemonCategory = new PokemonCategory()
                {
                    Category = categoryEntity,
                    Pokemon = existingPokemon,
                };
                this.context.Add(pokemonCategory);

                // existingPokemon zaten context tarafından takip edildiği için ekstra Update çağrısına gerek kalmıyor

                if (Save())
                {
                    transaction.Commit();
                    return true;
                }

                transaction.Rollback();
                return false;
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}