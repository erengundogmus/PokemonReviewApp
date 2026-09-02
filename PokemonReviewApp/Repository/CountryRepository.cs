using AutoMapper;
using PokemonReviewApp.AuditLogs;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using System.Security.Claims;

namespace PokemonReviewApp.Repository
{
    public class CountryRepository : ICountryInterface
    {
        private readonly DataContext context;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CountryRepository(DataContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.mapper = mapper;
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

        public bool CountryExists(int id)
        {
            return context.Countries.Any(c => c.Id == id && !c.IsDeleted);
        }

        public bool CreateCountry(Country country)
        {
            using var transaction = this.context.Database.BeginTransaction();
            try
            {
                this.context.ChangeTracker.Clear();

                this.context.Countries.Add(country);

                if (Save())
                {
                    var countryLog = new CountryLog
                    {
                        Action = "POST",
                        Status = "Active",
                        PerformedBy = GetCurrentUser(),
                        CountryId = country.Id,
                        NewName = country.Name,
                        LoggedAt = DateTime.UtcNow
                    };

                    this.context.CountryLog.Add(countryLog);

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

        public bool DeleteCountry(Country country)
        {
            using var transaction = this.context.Database.BeginTransaction();
            try
            {
                country.IsDeleted = true;
                country.DeletedAt = DateTime.UtcNow;
                this.context.Countries.Update(country);

                var countryLog = new CountryLog
                {
                    Action = "DELETE",
                    Status = "Deleted",
                    PerformedBy = GetCurrentUser(),
                    CountryId = country.Id,
                    NewName = country.Name,
                    LoggedAt = DateTime.UtcNow
                };

                this.context.CountryLog.Add(countryLog);

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

        public ICollection<Country> GetCountries()
        {
            return context.Countries.Where(c => !c.IsDeleted).ToList();
        }

        public Country GetCountry(int id)
        {
            return context.Countries.Where(c => c.Id == id && !c.IsDeleted).FirstOrDefault();
        }

        public Country GetCountryByOwner(int ownerId)
        {
            return context.Owners.Where(o => o.Id == ownerId && !o.Country.IsDeleted).Select(c => c.Country).FirstOrDefault();
        }

        public ICollection<Owner> GetOwnersFromACountry(int countryId)
        {
            return context.Owners.Where(c => c.Country.Id == countryId && !c.Country.IsDeleted).ToList();
        }

        public bool Save()
        {
            var saved = this.context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCountry(Country country)
        {
            using var transaction = this.context.Database.BeginTransaction();
            try
            {
                this.context.ChangeTracker.Clear();

                var existingCountry = this.context.Countries.FirstOrDefault(c => c.Id == country.Id);

                if (existingCountry != null)
                {
                    var countryLog = new CountryLog
                    {
                        Action = "PUT",
                        Status = "Updated",
                        PerformedBy = GetCurrentUser(),
                        CountryId = country.Id,
                        NewName = country.Name,
                        LoggedAt = DateTime.UtcNow
                    };

                    this.context.CountryLog.Add(countryLog);

                    existingCountry.Name = country.Name;

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