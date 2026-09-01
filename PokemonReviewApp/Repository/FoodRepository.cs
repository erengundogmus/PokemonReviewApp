using Microsoft.AspNetCore.Http;
using PokemonReviewApp.AuditLogs;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class FoodRepository : IFoodInterface
    {
        private readonly DataContext context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FoodRepository(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this._httpContextAccessor = httpContextAccessor;
        }

        private string GetCurrentUser()
        {
            return _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "System";
        }

        public bool AddFoodToPokemon(int pokeId, int foodId)
        {
            using var transaction = this.context.Database.BeginTransaction();
            try
            {
                this.context.ChangeTracker.Clear();

                var pokemonFood = new PokemonFood
                {
                    PokemonId = pokeId,
                    FoodId = foodId
                };

                this.context.Add(pokemonFood);

                var pokemonFoodLog = new PokemonFoodLog
                {
                    Action = "POST",
                    Status = "Active",
                    PokemonId = pokeId,
                    FoodId = foodId,
                    PerformedBy = GetCurrentUser(),
                    LoggedAt = DateTime.UtcNow
                };

                this.context.PokemonFoodLog.Add(pokemonFoodLog);

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

        public bool CreateFood(Food food)
        {
            using var transaction = this.context.Database.BeginTransaction();
            try
            {
                this.context.ChangeTracker.Clear();

                this.context.Foods.Add(food);

                if (Save())
                {
                    var foodLog = new FoodLog
                    {
                        Action = "POST",
                        Status = "Active",
                        PerformedBy = GetCurrentUser(),
                        FoodId = food.Id,
                        NewName = food.Name,
                        NewHp = food.Hp,
                        LoggedAt = DateTime.UtcNow
                    };

                    this.context.FoodLog.Add(foodLog);

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

        public bool DeleteFood(Food food)
        {
            using var transaction = this.context.Database.BeginTransaction();
            try
            {
                food.IsDeleted = true;
                food.DeletedAt = DateTime.UtcNow;
                this.context.Foods.Update(food);

                var foodLog = new FoodLog
                {
                    Action = "DELETE",
                    Status = "Deleted",
                    PerformedBy = GetCurrentUser(),
                    FoodId = food.Id,
                    NewName = food.Name,
                    NewHp = food.Hp,
                    LoggedAt = DateTime.UtcNow
                };

                this.context.FoodLog.Add(foodLog);

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

        public bool FoodExists(int foodId)
        {
            return this.context.Foods.Any(f => f.Id == foodId && !f.IsDeleted);
        }

        public Food GetFood(int foodId)
        {
            return this.context.Foods.Where(f => f.Id == foodId && !f.IsDeleted).FirstOrDefault();
        }

        public ICollection<Food> GetFoods()
        {
            return this.context.Foods.Where(f => !f.IsDeleted).OrderBy(o => o.Id).ToList();
        }

        public ICollection<Food> GetFoodsByPokemon(int pokeId)
        {
            return this.context.PokemonFoods.Where(pf => pf.PokemonId == pokeId && !pf.Food.IsDeleted).Select(pf => pf.Food).ToList();
        }

        public bool PokemonCanEatFood(int pokeId, int foodId)
        {
            return this.context.PokemonFoods.Any(pf => pf.PokemonId == pokeId && pf.FoodId == foodId && !pf.Food.IsDeleted);
        }

        public bool RemoveFoodFromPokemon(int pokeId, int foodId)
        {
            this.context.ChangeTracker.Clear();

            var pokemonFood = this.context.PokemonFoods.FirstOrDefault(pf => pf.PokemonId == pokeId && pf.FoodId == foodId);

            if (pokemonFood == null)
                return false;

            using var transaction = this.context.Database.BeginTransaction();
            try
            {
                this.context.Remove(pokemonFood);

                var pokemonFoodLog = new PokemonFoodLog
                {
                    Action = "DELETE",
                    Status = "Deleted",
                    PokemonId = pokeId,
                    FoodId = foodId,
                    PerformedBy = GetCurrentUser(),
                    LoggedAt = DateTime.UtcNow
                };

                this.context.PokemonFoodLog.Add(pokemonFoodLog);

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

        public bool Save()
        {
            var saved = this.context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateFood(Food food)
        {
            using var transaction = this.context.Database.BeginTransaction();
            try
            {
                this.context.ChangeTracker.Clear();

                var existingFood = this.context.Foods.FirstOrDefault(f => f.Id == food.Id);

                if (existingFood != null)
                {
                    var foodLog = new FoodLog
                    {
                        Action = "PUT",
                        Status = "Updated",
                        PerformedBy = GetCurrentUser(),
                        FoodId = food.Id,
                        NewName = food.Name,
                        NewHp = food.Hp,
                        LoggedAt = DateTime.UtcNow
                    };

                    this.context.FoodLog.Add(foodLog);

                    existingFood.Name = food.Name;
                    existingFood.Hp = food.Hp;

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
    }
}