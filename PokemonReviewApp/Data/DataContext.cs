using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext>options) : base(options)
        {
            
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Pokemon> Pokemon { get; set; }
        public DbSet<PokemonOwner> PokemonsOwners { get; set; }
        public DbSet<PokemonCategory> PokemonCategories { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<PokemonFood> PokemonFoods { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PokemonCategory>()
                    .HasKey(pc => new { pc.PokemonId, pc.CategoryId });
            modelBuilder.Entity<PokemonCategory>()
                    .HasOne(p => p.Pokemon)
                    .WithMany(pc =>pc.PokemonCategories)
                    .HasForeignKey(p => p.PokemonId);
            modelBuilder.Entity<PokemonCategory>()
                    .HasOne(p => p.Category)
                    .WithMany(pc => pc.PokemonCategories)
                    .HasForeignKey(c => c.CategoryId);

            modelBuilder.Entity<PokemonOwner>()
                    .HasKey(po => new { po.PokemonId, po.OwnerId });
            modelBuilder.Entity<PokemonOwner>()
                    .HasOne(p => p.Pokemon)
                    .WithMany(po => po.PokemonOwners)
                    .HasForeignKey(p => p.PokemonId);
            modelBuilder.Entity<PokemonOwner>()
                    .HasOne(p => p.Owner)
                    .WithMany(po => po.PokemonOwners)
                    .HasForeignKey(o => o.OwnerId);

            modelBuilder.Entity<Review>()
                    .Property(r => r.Rating)
                    .HasPrecision(18, 2);

            modelBuilder.Entity<PokemonFood>()
                    .HasKey(pf => new { pf.PokemonId, pf.FoodId });
            modelBuilder.Entity<PokemonFood>()
                    .HasOne(p => p.Pokemon)
                    .WithMany(pf => pf.PokemonFoods)
                    .HasForeignKey(p => p.PokemonId);
            modelBuilder.Entity<PokemonFood>()
                    .HasOne(p => p.Food)
                    .WithMany(pf => pf.PokemonFoods)
                    .HasForeignKey(p => p.FoodId);

        }

        internal object GetReviewer(int reviewerId)
        {
            throw new NotImplementedException();
        }

        internal object GetReviewers()
        {
            throw new NotImplementedException();
        }

        internal object GetReviewsByReviewer(int reviewerId)
        {
            throw new NotImplementedException();
        }

        internal bool ReviewerExists(int reviewerId)
        {
            throw new NotImplementedException();
        }

        internal bool ReviewersExist(Func<object, bool> value)
        {
            throw new NotImplementedException();
        }
    }
}
