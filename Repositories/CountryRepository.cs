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
            var country = await _context.Countries.FindAsync(id);

            if (country == null)
            {
                return null;
            }

            return country;
        }

        public async Task<Country> GetByName(string name)
        {
            var country = await _context.Countries.FirstOrDefaultAsync(u => u.Name == name);

            if (country == null)
            {
                return null;
            }

            return country;
        }

        public async Task<List<Country>> GetAll()
        {
            return await _context.Countries.ToListAsync();
        }

        public async Task Add(Country newCountry)
        {
            _context.Add(newCountry);

            await _context.SaveChangesAsync();
        }
    }
}