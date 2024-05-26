using UBB_SE_2024_Music.Models;

namespace UBB_SE_2024_Music.Repositories
{
    public interface ICountryRepository
    {
        Task Add(Country newCountry);
        Task<Country> GetById(int id);

        Task<Country> GetByName(string name);

        Task<List<Country>> GetAll();
    }
}