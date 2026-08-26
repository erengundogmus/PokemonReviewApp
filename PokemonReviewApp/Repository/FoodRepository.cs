using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.Models.Logs;

namespace PokemonReviewApp.Repository
{
    public class FoodRepository : IFoodInterface
    {
        private readonly DataContext context;

        public FoodRepository(DataContext context)
        {
            this.context = context;
        }

        public bool AddFoodToPokemon(int pokeId, int foodId)
        {
            var pokemonFood = new PokemonFood
            {
                PokemonId = pokeId,
                FoodId = foodId
            };
            this.context.Add(pokemonFood);
            return Save();
        }

        public bool CreateFood(Food food)
        {
            this.context.Add(food);
            
            //log için
            var foodLog = new FoodLog
            {
                Action = "POST",
                FoodId = food.Id,
                NewName = food.Name,
                NewHp = food.Hp,
                LoggedAt = DateTime.UtcNow
            };
            this.context.FoodLogs.Add(foodLog);
            //log için

            return Save();
        }
        

        public bool DeleteFood(Food food)
        {
            food.IsDeleted = true;
            food.DeletedAt = DateTime.UtcNow;
            return Save();
        }

        public bool FoodExists(int foodId)
        {
            return this.context.Foods.Any(f => f.Id == foodId);
        }
        
        public Food GetFood(int foodId)
        {
            return this.context.Foods.Where(f => f.Id == foodId).FirstOrDefault();
        }

        public ICollection<Food> GetFoods()
        {
            return this.context.Foods.OrderBy(o => o.Id).ToList();
        }

        public ICollection<Food> GetFoodsByPokemon(int pokeId)
        {
            return this.context.PokemonFoods.Where(pf => pf.PokemonId == pokeId).Select(pf => pf.Food).ToList();
        }

        public bool PokemonCanEatFood(int pokeId, int foodId)
        {
            return this.context.PokemonFoods.Any(pf => pf.PokemonId == pokeId && pf.FoodId == foodId);
        }

        public bool RemoveFoodFromPokemon(int pokeId, int foodId)
        {
            var pokemonFood = this.context.PokemonFoods.FirstOrDefault(pf => pf.PokemonId == pokeId && pf.FoodId == foodId);

            if (pokemonFood != null)
            {
                this.context.Remove(pokemonFood);
                return Save();
            }
            return false;
        }

        public bool Save()
        {
            var saved = this.context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateFood(Food food)
        {
            var existingFood = this.context.Foods.FirstOrDefault(f => f.Id == food.Id);

            if (existingFood != null)
            {
                var foodLog = new FoodLog
                {
                    Action = "PUT",
                    FoodId = food.Id,
                    OldName = existingFood.Name,
                    OldHp = existingFood.Hp,
                    NewName = food.Name,
                    NewHp = food.Hp,
                    LoggedAt = DateTime.UtcNow
                };

                this.context.FoodLogs.Add(foodLog);

                existingFood.Name = food.Name;
                existingFood.Hp = food.Hp;
            }

            return Save();
        }

    }    
}
