using UBB_SE_2024_Music.DTO;
using UBB_SE_2024_Music.Models;

namespace UBB_SE_2024_Music.Repositories
{
    public interface ISongBasicDetailsRepository : IRepository<Song>
    {
        Task<SongBasicInformation> TransformSongBasicDetailsToSongBasicInfo(Song song);

        Task<Song> GetSongBasicDetails(int songId);

        Task<List<Song?>> GetTop5MostListenedSongs(int userId);

        Task<Tuple<Song, decimal>> GetMostPlayedSongPercentile(int userId);

        Task<Tuple<string, decimal>> GetMostPlayedArtistPercentile(int userId);

        Task<List<string>> GetTop5Genres(int userId);

        Task<List<string>> GetAllNewGenresDiscovered(int userId);
    }
}