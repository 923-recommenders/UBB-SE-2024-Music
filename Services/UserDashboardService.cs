using System;
using UBB_SE_2024_Music.DTO;
using UBB_SE_2024_Music.Models;
using UBB_SE_2024_Music.Repositories;

namespace UBB_SE_2024_Music.Services
{
    public class UserDashboardService
    {
        private readonly SongRepository songRepository;
        private readonly IRepository<SongRecommendationDetails> songRecommendationRepository;

        public UserDashboardService(SongRepository songRepository,
            IRepository<SongRecommendationDetails> songRecommendationRepository)
        {
            this.songRepository = songRepository;
            this.songRecommendationRepository = songRecommendationRepository;
        }

        /// <summary>
        /// Method to transform the song data model to a simplified song information model.
        /// </summary>

        private async Task<SongBasicInformation> TransformSongDataModelToSongInfo(Song song)
        {
            var songInfo = new SongBasicInformation
            {
                SongId = song.SongId,
                Name = song.Name,
                Genre = song.Genre,
                Subgenre = song.Subgenre,
                Language = song.Language,
                Country = song.Country,
                Album = song.Album,
                Image = song.ImagePath
            };

            return songInfo;
        }

        /// <summary>
        /// Searches for songs by title.
        /// </summary>
        /// <param name="title">The title of the song to search for.</param>
        /// <returns>A list of simplified song information models that match the search title.</returns>
        public async Task<List<SongBasicInformation>> SearchSongsByTitleAsync(string title)
        {
            var songs = (await songRepository.GetAll()).Where(song 
                => song.Name.ToLower().Contains(title.ToLower().Trim())).ToList();
            var songInfos = new List<SongBasicInformation>();
            foreach (var song in songs)
            {
                var songInfo = await TransformSongDataModelToSongInfo(song);
                songInfos.Add(songInfo);
            }
            return songInfos;
        }

        public async Task<List<SongBasicInformation>> GetTop5MostListenedSongs(int userID)
        {
            var mostPlayedSongs = await songRepository.GetTop5MostListenedSongs(userID);
            var songInfoList = new List<SongBasicInformation>();
            foreach (var song in mostPlayedSongs)
            {
                var songInfo = await TransformSongDataModelToSongInfo(song);
                songInfoList.Add(songInfo);
            }

            return songInfoList;
        }

        public List<SongBasicInformation> GetRecommendedSongs(int userID)
        {
            var recommendedSongs = new List<SongBasicInformation>();

            for (int i = 0; i < 5; i++)
            {
                var songBasicInfo = new SongBasicInformation
                {
                    SongId = i,
                    Name = "Recommended Song " + i,
                    Genre = "Pop",
                    Subgenre = "Dance",
                    Language = "English",
                    Country = "USA",
                    Album = "Recommended Album",
                    Image = "recommended_song_img.png"
                };
                recommendedSongs.Add(songBasicInfo);
            }

            return recommendedSongs;
        }

        public List<SongBasicInformation> GetAdvertisedSongs(int userID)
        {
            var advertisedSongs = new List<SongBasicInformation>();

            for (int i = 0; i < 5; i++)
            {
                var songBasicInfo = new SongBasicInformation
                {
                    SongId = i,
                    Name = "Advertised Song " + i,
                    Genre = "Pop",
                    Subgenre = "Dance",
                    Language = "English",
                    Country = "USA",
                    Album = "Advertised Album",
                    Image = "advertised_song_img.png"
                };
                advertisedSongs.Add(songBasicInfo);
            }
            return advertisedSongs;
        }
    }
}
