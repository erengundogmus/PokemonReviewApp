using PokemonReviewApp.AuditLogs;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using System.Security.Claims;

namespace PokemonReviewApp.Repository
{
    public class OwnerRepository : IOwnerInterface
    {
        private readonly DataContext context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OwnerRepository(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this._httpContextAccessor = httpContextAccessor;
        }

        private string GetCurrentUser()
        {
            var context = _httpContextAccessor.HttpContext;
            if (context?.User?.Identity?.IsAuthenticated != true)
                return "System";

            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                         ?? context.User.FindFirst("sub")?.Value ?? "UnknownID";

            var userName = context.User.Identity.Name ?? "UnknownUser";

            return $"{userId} ({userName})";
        }

        public bool CreateOwner(Owner owner)
        {
            using var transaction = this.context.Database.BeginTransaction();
            try
            {
                this.context.ChangeTracker.Clear();

                this.context.Owners.Add(owner);

                if (Save())
                {
                    var ownerLog = new OwnerLog
                    {
                        Action = "POST",
                        Status = "Active",
                        PerformedBy = GetCurrentUser(),
                        OwnerId = owner.Id,
                        NewName = owner.Name,
                        NewGym = owner.Gym,
                        LoggedAt = DateTime.UtcNow
                    };

                    this.context.OwnerLog.Add(ownerLog);

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

        public bool DeleteOwner(Owner owner)
        {
            using var transaction = this.context.Database.BeginTransaction();
            try
            {
                owner.IsDeleted = true;
                owner.DeletedAt = DateTime.UtcNow;
                this.context.Owners.Update(owner);

                var ownerLog = new OwnerLog
                {
                    Action = "DELETE",
                    Status = "Deleted",
                    PerformedBy = GetCurrentUser(),
                    OwnerId = owner.Id,
                    NewName = owner.Name,
                    NewGym = owner.Gym,
                    LoggedAt = DateTime.UtcNow
                };

                this.context.OwnerLog.Add(ownerLog);

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

        public Owner GetOwner(int ownerId)
        {
            return this.context.Owners.Where(o => o.Id == ownerId && !o.IsDeleted).FirstOrDefault();
        }

        public ICollection<Owner> GetOwnerOfAPokemon(int pokeId)
        {
            return this.context.PokemonsOwners.Where(p => p.Pokemon.Id == pokeId && !p.Owner.IsDeleted).Select(o => o.Owner).ToList();
        }

        public ICollection<Owner> GetOwners()
        {
            return this.context.Owners.Where(o => !o.IsDeleted).OrderBy(o => o.Id).ToList();
        }

        public ICollection<Pokemon> GetPokemonByOwner(int ownerId)
        {
            return this.context.PokemonsOwners.Where(p => p.Owner.Id == ownerId && !p.Owner.IsDeleted).Select(p => p.Pokemon).ToList();
        }

        public bool OwnerExists(int ownerId)
        {
            return this.context.Owners.Any(o => o.Id == ownerId && !o.IsDeleted);
        }

        public bool Save()
        {
            var saved = this.context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateOwner(Owner owner)
        {
            using var transaction = this.context.Database.BeginTransaction();
            try
            {
                this.context.ChangeTracker.Clear();

                var existingOwner = this.context.Owners.FirstOrDefault(c => c.Id == owner.Id);

                if (existingOwner != null)
                {
                    var ownerLog = new OwnerLog
                    {
                        Action = "PUT",
                        Status = "Updated",
                        PerformedBy = GetCurrentUser(),
                        OwnerId = owner.Id,
                        NewName = owner.Name,
                        NewGym = owner.Gym,
                        LoggedAt = DateTime.UtcNow
                    };

                    this.context.OwnerLog.Add(ownerLog);

                    existingOwner.Name = owner.Name;
                    existingOwner.Gym = owner.Gym;
                    existingOwner.CountryId = owner.CountryId;

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