using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IFoodInterface
    {
        ICollection<Food> GetFoods();
        Food GetFood(int foodId);
        ICollection<Food> GetFoodsByPokemon(int pokeId);
        bool FoodExists(int foodId);
        bool CreateFood(Food food);
        bool UpdateFood(Food food);
        bool DeleteFood(Food food);
        bool AddFoodToPokemon(int pokeId, int foodId);
        bool RemoveFoodFromPokemon(int pokeId, int foodId);
        bool PokemonCanEatFood(int pokeId, int foodId);

        bool Save();
    }
}