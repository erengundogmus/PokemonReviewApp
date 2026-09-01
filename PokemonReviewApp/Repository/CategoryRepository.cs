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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CategoryRepository(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this._httpContextAccessor = httpContextAccessor;
        }

        //loga kullanıcı eklemek için
        private string GetCurrentUser()
        {
            return _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "System";
        }

        public bool CategoryExists(int id)
        {
            return this.context.Categories.Any(c => c.Id == id && !c.IsDeleted);
        }

        public bool CreateCategory(Category category)
        {
            using var transaction = this.context.Database.BeginTransaction();
            try
            {
                this.context.Categories.Add(category);

                if (Save())
                {
                    var categoryLog = new CategoryLog
                    {
                        Action = "POST",
                        Status = "Active",
                        PerformedBy = GetCurrentUser(),
                        CategoryId = category.Id,
                        NewName = category.Name,
                        LoggedAt = DateTime.UtcNow
                    };

                    this.context.CategoryLog.Add(categoryLog);

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

        public bool DeleteCategory(Category category)
        {
            using var transaction = this.context.Database.BeginTransaction();
            try
            {
                category.IsDeleted = true;
                category.DeletedAt = DateTime.UtcNow;
                this.context.Categories.Update(category);

                var categoryLog = new CategoryLog
                {
                    Action = "DELETE",
                    Status = "Deleted",
                    PerformedBy = GetCurrentUser(),
                    CategoryId = category.Id,
                    NewName = category.Name,
                    LoggedAt = DateTime.UtcNow
                };

                this.context.CategoryLog.Add(categoryLog);

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

        public ICollection<Category> GetCategories()
        {
            return this.context.Categories.Where(c => !c.IsDeleted).ToList();
        }

        public Category GetCategory(int id)
        {
            return this.context.Categories.Where(e => e.Id == id && !e.IsDeleted).FirstOrDefault();
        }

        public ICollection<Pokemon> GetPokemonByCategory(int categoryId)
        {
            return this.context.Pokemons
                .Include(p => p.PokemonCategories).ThenInclude(pc => pc.Category)
                .Include(p => p.PokemonOwners).ThenInclude(po => po.Owner)
                .Where(p => p.PokemonCategories.Any(pc => pc.CategoryId == categoryId && !pc.Category.IsDeleted))
                .ToList();
        }

        public bool UpdateCategory(Category category)
        {
            // transaction başlatma
            using var transaction = this.context.Database.BeginTransaction();
            try
            {
                this.context.ChangeTracker.Clear();

                var existingCategory = this.context.Categories.FirstOrDefault(c => c.Id == category.Id);

                if (existingCategory != null)
                {
                    var categoryLog = new CategoryLog
                    {
                        Action = "PUT",
                        Status = "Updated",
                        PerformedBy = GetCurrentUser(),
                        CategoryId = category.Id,
                        NewName = category.Name,
                        LoggedAt = DateTime.UtcNow
                    };
                    this.context.CategoryLog.Add(categoryLog);

                    existingCategory.Name = category.Name;

                    bool isSaved = Save();

                    if (isSaved)
                    {
                        //hata yoksa onaylanıyor
                        transaction.Commit();
                        return true;
                    }
                }

                //hata varsa eskiye döndürüyor
                transaction.Rollback();
                return false;
            }
            catch (Exception)
            {
                //tüm işlemleri iptal ediyor (Rollback)
                transaction.Rollback();
                throw; //hatayı dönderiyor
            }
        }

        public bool Save()
        {
            var saved = this.context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}