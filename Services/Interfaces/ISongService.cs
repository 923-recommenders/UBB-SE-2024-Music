using UBB_SE_2024_Music.Models;

namespace UBB_SE_2024_Music.Services.Interfaces
{
    public interface ISongService
    {
        Task<Song?> GetSongById(int songId);
        Task<IEnumerable<Song>> GetAllSongs();
        Task<Song> AddSong(SongForAddUpdateModel songModel);
        Task<bool> DeleteSong(int songId);
        Task<bool> UpdateSong(int songId, SongForAddUpdateModel songModel);
    }
}
