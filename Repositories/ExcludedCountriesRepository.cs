using Microsoft.EntityFrameworkCore;
using UBB_SE_2024_Music.Data;
using UBB_SE_2024_Music.Models;

namespace UBB_SE_2024_Music.Repositories
{
    public class ExcludedCountryRepsitory : Repository<Country>
    {
        public ExcludedCountryRepsitory(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Country>> GetAllExcludedCounties(Song song)
        {
            var excludedCountryIds = await context.ExcludedCountries
                .Where(excludedCountry => excludedCountry.SongId == song.SongId)
                .Select(excludedCountry => excludedCountry.CountryId)
                .ToListAsync();

            var excludedCountries = await context.Countries
                .Where(country => excludedCountryIds.Contains(country.CountryId))
                .ToListAsync();

            return excludedCountries;
        }
    }
}