using UBB_SE_2024_Music.Models;

namespace UBB_SE_2024_Music.Repositories.Interfaces
{
    public interface ISongRepository
    {
        Task<Song?> GetSongById(int songId);
        Task<IEnumerable<Song>> GetAllSongs();
        Task<int> AddSong(Song song);
        Task<bool> DeleteSong(int songId);
        Task<bool> UpdateSong(int songId, Song song);
    }
}
