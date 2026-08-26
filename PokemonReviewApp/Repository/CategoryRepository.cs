using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.AuditLogs;
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

        public bool CreateCategory(Category category)
        {
            this.context.Add(category);
            bool isCategorySaved = this.context.SaveChanges() > 0;

            if (isCategorySaved)
            {
                var categoryLog = new CategoryLog
                {
                    Action = "POST",
                    CategoryId = category.Id,
                    NewName = category.Name,
                    LoggedAt = DateTime.UtcNow
                };

                this.context.CategoryLog.Add(categoryLog);
                return Save();
            }

            return false;
        }

        public bool DeleteCategory(Category category)
        {
            category.IsDeleted = true;
            category.DeletedAt = DateTime.UtcNow; ;
            return Save();
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
            return this.context.Pokemons.Include(p => p.PokemonCategories).ThenInclude(pc => pc.Category).Include(p => p.PokemonOwners)
                .ThenInclude(po => po.Owner).Where(p => p.PokemonCategories.Any(pc => pc.CategoryId == categoryId)).ToList();
        }

        public bool UpdateCategory(Category category)
        {
            var existingCategory = this.context.Categories.FirstOrDefault(c => c.Id == category.Id);

            if (existingCategory != null)
            {
                var categoryLog = new CategoryLog
                {
                    Action = "PUT",
                    CategoryId = category.Id,
                    OldName = existingCategory.Name,
                    NewName = category.Name,
                    LoggedAt = DateTime.UtcNow
                };

                this.context.CategoryLog.Add(categoryLog);

                existingCategory.Name = category.Name;
            }

            return Save();
        }


        public bool Save()
        {
            var saved = this.context.SaveChanges();
            return saved > 0 ? true : false;
        }


    }
}
