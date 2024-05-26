using UBB_SE_2024_Music.Models;

namespace UBB_SE_2024_Music.Services
{
    public interface IPlaylistSongItemService
    {
        Task AddSongToPlaylist(int songId, int playlistId);
        Task<bool> DeleteSongFromPlaylist(int songId, int playlistId);
        Task<IEnumerable<Song>> GetSongsByPlaylistId(int playlistId);
        Task<IEnumerable<Playlist>> GetPlaylistsBySongId(int songId);
    }
}
