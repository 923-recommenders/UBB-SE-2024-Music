using Microsoft.EntityFrameworkCore;

namespace UBB_SE_2024_Music.Models
{
    [PrimaryKey(nameof(CountryId))]
    public class Country
    {
        public int CountryId { get; set; }
        public string Name { get; set; }
    }
}