using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.AuditLogs;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using System.Diagnostics.Metrics;

namespace PokemonReviewApp.Repository
{
    public class OwnerRepository : IOwnerInterface
    {
        private readonly DataContext context;

        public OwnerRepository(DataContext context) 
        {
            this.context = context;
        }

        public bool CreateOwner(Owner owner)
        {
            this.context.Add(owner);
            bool isOwnerSaved = this.context.SaveChanges() > 0;

            if (isOwnerSaved)
            {
                var ownerLog = new OwnerLog
                {
                    Action = "POST",
                    OwnerId = owner.Id,
                    NewName = owner.Name,
                    NewGym = owner.Gym,
                    LoggedAt = DateTime.UtcNow
                };

                this.context.OwnerLog.Add(ownerLog);
                return Save();
            }

            return false;
        }

        public bool DeleteOwner(Owner owner)
        {
            owner.IsDeleted = true;
            owner.DeletedAt = DateTime.UtcNow;
            return Save();
        }

        public Owner GetOwner(int ownerId)
        {
            return this.context.Owners.Where(o => o.Id == ownerId).FirstOrDefault();
        }

        public ICollection<Owner> GetOwnerOfAPokemon(int pokeId)
        {
            return this.context.PokemonsOwners.Where(p => p.Pokemon.Id == pokeId).Select(o => o.Owner).ToList();
        }

        public ICollection<Owner> GetOwners()
        {
            return this.context.Owners.OrderBy(o => o.Id).ToList();
        }

        public ICollection<Pokemon> GetPokemonByOwner(int ownerId)
        {
            return this.context.PokemonsOwners.Where(p => p.Owner.Id == ownerId).Select(p => p.Pokemon).ToList();
        }

        public bool OwnerExists(int ownerId)
        {
            return this.context.Owners.Any(o => o.Id == ownerId);
        }

        public bool Save()
        {
            var saved = this.context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateOwner(Owner owner)
        {
            this.context.ChangeTracker.Clear();

            var existingOwnerForLog = this.context.Owners.AsNoTracking().FirstOrDefault(c => c.Id == owner.Id);

            if (existingOwnerForLog != null)
            {
                var ownerLog = new OwnerLog
                {
                    Action = "PUT",
                    OwnerId = owner.Id,
                    OldName = existingOwnerForLog.Name,
                    NewName = owner.Name,
                    OldGym = existingOwnerForLog.Gym,
                    NewGym = owner.Gym,
                    LoggedAt = DateTime.UtcNow
                };

                this.context.OwnerLog.Add(ownerLog);
            }

            this.context.Update(owner);

            return Save();
        }
    }
}
