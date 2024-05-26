using UBB_SE_2024_Music.DTO;
using UBB_SE_2024_Music.Enums;

namespace UBB_SE_2024_Music.Services
{
    public interface IFullDetailsOnSongService
    {
        public Task<FullDetailsOnSong> GetFullDetailsOnSong(int songId);
        public Task<FullDetailsOnSong> GetCurrentMonthDetails(int songId);
    }
}
