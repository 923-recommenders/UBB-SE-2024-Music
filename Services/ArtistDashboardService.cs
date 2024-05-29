using UBB_SE_2024_Music.DTO;
using UBB_SE_2024_Music.Models;
using UBB_SE_2024_Music.Repositories;

namespace UBB_SE_2024_Music.Services
{
    public class ArtistDashboardService
    {
        private readonly IRepository<Song> songRepository;
        private readonly IRepository<SongFeatures> featureRepository;
        private readonly IRepository<SongRecommendationDetails> songRecommendationRepository;
        private readonly IRepository<ArtistDetails> artistRepository;

        public ArtistDashboardService(IRepository<Song> songRepository, IRepository<SongFeatures> featureRepository, IRepository<SongRecommendationDetails> songRecommendationRepository, IRepository<ArtistDetails> artistRepository)
        {
            this.songRepository = songRepository;
            this.featureRepository = featureRepository;
            this.songRecommendationRepository = songRecommendationRepository;
            this.artistRepository = artistRepository;
        }

        private async Task<SongBasicInformation> TransformSongDataBaseModelToSongInfo(Song song)
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

            var artists = await artistRepository.GetAll();
            var artist = artists.FirstOrDefault(a => a.ArtistId == song.ArtistId);
            if (artist != null)
            {
                songInfo.Artist = artist.Name;
            }

            var features = (await featureRepository.GetAll()).Where(f => f.SongId == song.SongId).ToList();
            foreach (var feature in features)
            {
                songInfo.Features.Add(feature.ToString());
            }

            return songInfo;
        }

        /// <summary>
        /// Retrieves all songs by a specific artist.
        /// </summary>
        /// <param name="artistId">The ID of the artist.</param>
        /// <returns>A list of simplified song information models for the specified artist.</returns>
        public async Task<List<SongBasicInformation>> GetAllArtistSongsAsync(string artistName)
        {
            var artist = (await artistRepository.GetAll())
                .Where(artist => artist.Name == artistName)
                .FirstOrDefault();
            if (artist == null)
            {
                return new List<SongBasicInformation>();
            }

            var songs = (await songRepository.GetAll()).Where(s => s.ArtistId == artist.ArtistId).ToList();
            var artistSongs = new List<SongBasicInformation>();
            foreach (var song in songs)
            {
                var songInfo = await TransformSongDataBaseModelToSongInfo(song);
                artistSongs.Add(songInfo);
            }
            return artistSongs;
        }

        /// <summary>
        /// Searches for songs by title.
        /// </summary>
        /// <param name="title">The title of the song to search for.</param>
        /// <returns>A list of simplified song information models that match the search title.</returns>
        public async Task<List<SongBasicInformation>> SearchSongsByTitleAsync(string title)
        {
            var songs = (await songRepository.GetAll()).Where(s => s.Name.ToLower().Contains(title.ToLower().Trim())).ToList();
            var songInfos = new List<SongBasicInformation>();
            foreach (var song in songs)
            {
                var songInfo = await TransformSongDataBaseModelToSongInfo(song);
                songInfos.Add(songInfo);
            }
            return songInfos;
        }

        /// <summary>
        /// Retrieves detailed information about a specific song.
        /// </summary>
        /// <param name="songId">The ID of the song.</param>
        /// <returns>A simplified song information model for the specified song, or null if not found.</returns>
        public async Task<SongBasicInformation> GetSongInformationAsync(int songId)
        {
            var song = await songRepository.GetById(songId);
            return song != null ? await TransformSongDataBaseModelToSongInfo(song) : null;
        }

        /// <summary>
        /// Retrieves song recommendation details for a specific song.
        /// </summary>
        /// <param name="songId">The ID of the song.</param>
        /// <returns>Song recommendation details for the specified song.</returns>
        public async Task<SongRecommendationDetails> GetSongRecommandationDetailsAsync(int songId)
        {
            return await songRecommendationRepository.GetById(songId) ?? new SongRecommendationDetails();
        }

        /// <summary>
        /// Retrieves artist information by a specific song.
        /// </summary>
        /// <param name="songId">The ID of the song.</param>
        /// <returns>Artist details for the specified song, or null if not found.</returns>
        public async Task<ArtistDetails> GetArtistInfoBySongAsync(int songId)
        {
            var song = await songRepository.GetById(songId);
            return song != null ? await artistRepository.GetById(song.ArtistId) : null;
        }

        /// <summary>
        /// Retrieves the artist with the most published songs.
        /// </summary>
        /// <returns>Artist details for the artist with the most published songs.</returns>
        public async Task<ArtistDetails> GetMostPublishedArtistAsync()
        {
            var songs = await songRepository.GetAll();
            if (songs == null || !songs.Any())
            {
                return null;
            }
            var artistSongCount = songs.GroupBy(s => s.ArtistId).ToDictionary(g => g.Key, g => g.Count());
            var artistId = artistSongCount.OrderByDescending(kvp => kvp.Value).FirstOrDefault().Key;
            return await artistRepository.GetById(artistId);
        }

        /// <summary>
        /// Retrieves songs by the most published artist for the main page.
        /// </summary>
        /// <returns>A list of simplified song information models by the most published artist.</returns>
        public async Task<List<SongBasicInformation>> GetSongsByMostPublishedArtistForMainPageAsync()
        {
            var mostPublishedArtist = await GetMostPublishedArtistAsync();
            if (mostPublishedArtist == null)
            {
                return new List<SongBasicInformation>();
            }

            var songs = (await songRepository.GetAll()).Where(s => s.ArtistId == mostPublishedArtist.ArtistId).ToList();
            var songInfos = new List<SongBasicInformation>();
            foreach (var song in songs)
            {
                var songInfo = await TransformSongDataBaseModelToSongInfo(song);
                songInfos.Add(songInfo);
            }
            return songInfos;
        }
    }
}
