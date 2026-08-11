using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class CategoryRepository : ICategoryInterface
    {
        private readonly DataContext context;

        public CategoryRepository(DataContext context)
        {
            this.context = context;
        }

        public bool CategoryExists(int id)
        {
            return this.context.Categories.Any(c => c.Id == id);
        }

        public ICollection<Category> GetCategories()
        {
            return this.context.Categories.ToList();
        }

        public Category GetCategory(int id)
        {
            return this.context.Categories.Where(e => e.Id == id).FirstOrDefault();
        }
        public ICollection<Pokemon> GetPokemonByCategory(int categoryId)
        {
            return this.context.PokemonCategories.Where(e =>e.CategoryId == categoryId).Select(c => c.Pokemon).ToList();
        }
    }
}
