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
                        OwnerId = owner.Id,
                        NewName = owner.Name,
                        NewGym = owner.Gym,
                        LoggedAt = DateTime.UtcNow
                    };

                    this.context.OwnerLog.Add(ownerLog);

                    existingOwner.Name = owner.Name;
                    existingOwner.Gym = owner.Gym;

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
