using UBB_SE_2024_Music.Enums;
using UBB_SE_2024_Music.DTO;

namespace UBB_SE_2024_Music.Services
{
    public interface IFullDetailsOnSongService
    {
        public Task<FullDetailsOnSong> GetFullDetailsOnSong(int songId);
        public Task<FullDetailsOnSong> GetCurrentMonthDetails(int songId);
    }
}
