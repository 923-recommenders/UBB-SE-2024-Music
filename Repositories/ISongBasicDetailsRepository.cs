using UBB_SE_2024_Music.Models;
using UBB_SE_2024_Music.DTO;

namespace UBB_SE_2024_Music.Repositories
{
    public interface ISongBasicDetailsRepository : IRepository<SongDataBaseModel>
    {
        Task<SongBasicInformation> TransformSongBasicDetailsToSongBasicInfo(SongDataBaseModel song);

        Task<SongDataBaseModel> GetSongBasicDetails(int songId);

        Task<List<SongDataBaseModel?>> GetTop5MostListenedSongs(int userId);

        Task<Tuple<SongDataBaseModel, decimal>> GetMostPlayedSongPercentile(int userId);

        Task<Tuple<string, decimal>> GetMostPlayedArtistPercentile(int userId);

        Task<List<string>> GetTop5Genres(int userId);

        Task<List<string>> GetAllNewGenresDiscovered(int userId);
    }
}