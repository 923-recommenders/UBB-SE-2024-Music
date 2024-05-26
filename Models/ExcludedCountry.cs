using Microsoft.EntityFrameworkCore;

namespace UBB_SE_2024_Music.Models
{
    [PrimaryKey(nameof(CountryId), nameof(SongId))]
    public class ExcludedCountry
    {
        public int CountryId { get; set; }
        public int SongId { get; set; }
    }
}