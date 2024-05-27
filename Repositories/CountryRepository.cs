using Microsoft.EntityFrameworkCore;
using UBB_SE_2024_Music.Data;
using UBB_SE_2024_Music.Models;

namespace UBB_SE_2024_Music.Repositories
{
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        public CountryRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<Country> GetById(int id)
        {
            var country = await context.Countries.FindAsync(id);

            if (country == null)
            {
                return null;
            }

            return country;
        }

        public async Task<Country> GetByName(string name)
        {
            var country = await context.Countries.FirstOrDefaultAsync(country => country.Name == name);

            if (country == null)
            {
                return null;
            }

            return country;
        }

        public async Task<List<Country>> GetAll()
        {
            return await context.Countries.ToListAsync();
        }

        public async Task Add(Country newCountry)
        {
            context.Add(newCountry);

            await context.SaveChangesAsync();
        }
    }
}