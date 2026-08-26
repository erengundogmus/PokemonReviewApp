using AutoMapper;
using PokemonReviewApp.AuditLogs;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class CountryRepository : ICountryInterface
    {
        private readonly DataContext context;
        private readonly IMapper mapper;

        public CountryRepository(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public bool CountryExists(int id)
        {
            return context.Countries.Any(c => c.Id == id);
        }

        public bool CreateCountry(Country country)
        {
            this.context.Add(country);
            bool isCountrySaved = this.context.SaveChanges() > 0;

            if (isCountrySaved)
            {
                var countryLog = new CountryLog
                {
                    Action = "POST",
                    CountryId = country.Id,
                    NewName = country.Name,
                    LoggedAt = DateTime.UtcNow
                };

                this.context.CountryLog.Add(countryLog);
                return Save();
            }

            return false;
        }

        public bool DeleteCountry(Country country)
        {
            country.IsDeleted = true;
            country.DeletedAt = DateTime.UtcNow;
            return Save();
        }

        public ICollection<Country> GetCountries()
        {
            return context.Countries.ToList();
        }

        public Country GetCountry(int id)
        {
            return context.Countries.Where(c => c.Id == id).FirstOrDefault();
        }

        public Country GetCountryByOwner(int ownerId)
        {
            return context.Owners.Where(o => o.Id == ownerId).Select(c => c.Country).FirstOrDefault();
        }

        public ICollection<Owner> GetOwnersFromACountry(int countryId)
        {
            return context.Owners.Where(c => c.Country.Id == countryId).ToList();
        }

        public bool Save()
        {
            var saved = this.context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCountry(Country country)
        {
            var existingCountry = this.context.Countries.FirstOrDefault(c => c.Id == country.Id);

            if (existingCountry != null)
            {
                var countryLog = new CountryLog
                {
                    Action = "PUT",
                    CountryId = country.Id,
                    OldName = existingCountry.Name,
                    NewName = country.Name,
                    LoggedAt = DateTime.UtcNow
                };

                this.context.CountryLog.Add(countryLog);

                existingCountry.Name = country.Name;
            }

            return Save();
        }
    }
}