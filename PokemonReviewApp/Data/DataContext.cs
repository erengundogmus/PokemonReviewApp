using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.AuditLogs;
using PokemonReviewApp.Interfaces;
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
        public DbSet<Pokemon> Pokemons { get; set; }
        public DbSet<PokemonOwner> PokemonsOwners { get; set; }
        public DbSet<PokemonCategory> PokemonCategories { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<PokemonFood> PokemonFoods { get; set; }
        public DbSet<FoodLog> FoodLog { get; set; }
        public DbSet<CategoryLog> CategoryLog { get; set; }
        public DbSet<CountryLog> CountryLog { get; set; }
        public DbSet<OwnerLog> OwnerLog { get; set; }
        public DbSet<PokemonLog> PokemonLog { get; set; }
        public DbSet<ReviewLog> ReviewLog { get; set; }
        public DbSet<ReviewerLog> ReviewerLog { get; set; }
        public DbSet<PokemonFoodLog> PokemonFoodLog { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); //base çağrısının başta olması gerekiyormuş

            modelBuilder.Entity<PokemonCategory>()
                    .HasKey(pc => new { pc.PokemonId, pc.CategoryId });
            modelBuilder.Entity<PokemonCategory>()
                    .HasOne(p => p.Pokemon)
                    .WithMany(pc => pc.PokemonCategories)
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

            //ISoftDelete uygulayan tüm sınıflara otomatik olarak "IsDeleted == false" filtresi uygular
            foreach (var entityType in modelBuilder.Model.GetEntityTypes()) //tüm tabloları tarar
            {
                if (typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType)) //model ISoftDelete yapabiliyor mu kontrol eder
                {
                    var parameter = System.Linq.Expressions.Expression.Parameter(entityType.ClrType, "e"); //e adında geçici bir parametre oluşturur
                    var property = System.Linq.Expressions.Expression.Property(parameter, nameof(ISoftDelete.IsDeleted)); //incelenen modelin IsoftDelete özelliğini yakalar
                    //e => e.IsDeleted == false silinmiş olarak işaretlenmeyenler
                    var condition = System.Linq.Expressions.Expression.Lambda(System.Linq.Expressions.Expression.Equal(property, System.Linq.Expressions.Expression.Constant(false)),parameter);
                    //filtreyi kullanmamızı sağlar
                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(condition);
                }
            }
        }

        /*        LOG SİSTEMİ İÇİN TEK BİR KONTROL MERKEZİ DENEMESİ
         
        //log sistemi için 
        public override int SaveChanges()
        {
            var modifiedEntries = ChangeTracker.Entries().Where(e => e.Entity is ILoggable &&(e.State == EntityState.Added || e.State == EntityState.Modified)).ToList(); //listenin kopyası

            var logsToAdd = new List<Log>(); //geçici olarak burada tutuyor

            foreach (var entry in modifiedEntries)
            {
                var entityName = entry.Entity.GetType().Name;
                string action = entry.State == EntityState.Added ? "POST" : "PUT";

                string? oldValues = null;
                if (entry.State == EntityState.Modified)
                {
                    var databaseValues = entry.GetDatabaseValues();
                    if (databaseValues != null)
                    {
                        var originalObj = databaseValues.ToObject();
                        oldValues = System.Text.Json.JsonSerializer.Serialize(originalObj);
                    }
                }

                var newValues = System.Text.Json.JsonSerializer.Serialize(entry.CurrentValues.ToObject());

                var log = new Log
                {
                    Action = action,
                    TableName = entityName,
                    OldValues = oldValues,
                    NewValues = newValues,
                    LoggedAt = DateTime.UtcNow
                };

                logsToAdd.Add(log); //geçici listeye ekliyoruz
            }

            //döngüden çıkınca kalıcı olarak ekliyor
            if (logsToAdd.Any())
            {
                Logs.AddRange(logsToAdd);
            }

            return base.SaveChanges();
        } */

    }
}
