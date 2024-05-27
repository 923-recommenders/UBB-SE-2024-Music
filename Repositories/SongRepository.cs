using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using UBB_SE_2024_Music.Data;
using UBB_SE_2024_Music.DTO;
using UBB_SE_2024_Music.Enums;
using UBB_SE_2024_Music.Models;
using UBB_SE_2024_Music.Repositories.Interfaces;

namespace UBB_SE_2024_Music.Repositories
{
    public class SongRepository : Repository<Song>, ISongBasicDetailsRepository, ISongRepository
    {
        public SongRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<int> AddSong(Song song)
        {
            await context.Songs.AddAsync(song);
            await context.SaveChangesAsync();

            return song.SongId;
        }

        public async Task<bool> DeleteSong(int songId)
        {
            var songToRemove = await context.Songs.FirstOrDefaultAsync(song => song.SongId == songId);
            if (songToRemove == null)
            {
                return false;
            }

            context.Songs.Remove(songToRemove);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Song>> GetAllSongs()
        {
            return await context.Songs.ToListAsync();
        }

        public async Task<Song?> GetSongById(int songId)
        {
            return await context.Songs.FirstOrDefaultAsync(song => song.SongId == songId);
        }

        public async Task<bool> UpdateSong(int songId, Song song)
        {
            var songToUpdate = await context.Songs.FirstOrDefaultAsync(song => song.SongId == songId);
            if (songToUpdate == null)
            {
                return false;
            }

            song.SongId = songId;

            context.Songs.Entry(songToUpdate).CurrentValues.SetValues(song);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<SongBasicInformation> TransformSongBasicDetailsToSongBasicInfo(Song song)
        {
            int artistId = song.ArtistId;
            var artistName = await context.ArtistDetails
                .Where(artist => artist.ArtistId == artistId)
                .Select(artist => artist.Name)
                .FirstOrDefaultAsync();
            return new SongBasicInformation
            {
                SongId = song.SongId,
                Name = song.Name,
                Genre = song.Genre,
                Subgenre = song.Subgenre,
                Artist = artistName,
                Language = song.Language,
                Country = song.Country,
                Album = song.Album,
                Image = song.ImagePath
            };
        }

        public async Task<Song> GetSongBasicDetails(int songId)
        {
            return await context.Songs
                .Where(song => song.SongId == songId)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Song>> GetTop5MostListenedSongs(int userId)
        {
            return await context.Songs
                .Where(song => context.UserPlaybackBehaviour
                    .Where(userPlayback => userPlayback.UserId == userId && userPlayback.EventType == PlaybackEventType.StartSongPlayback)
                    .GroupBy(userPlayback => userPlayback.SongId)
                    .Select(group => group.Key)
                    .Take(5)
                    .Contains(song.SongId))
                .ToListAsync();
        }
        public async Task<Tuple<Song, decimal>> GetMostPlayedSongPercentile(int userId)
        {
            var mostPlayedSong = await GetMostPlayedSong(userId);
            var totalSongs = await GetTotalSongsPlayedByUser(userId);
            var mostListenedSongCount = await GetMostListenedSongCount(userId);
            return new Tuple<Song, decimal>(mostPlayedSong, (decimal)mostListenedSongCount / totalSongs);
        }

        private async Task<Song> GetMostPlayedSong(int userId)
        {
            var mostPlayedSongId = await context.UserPlaybackBehaviour
                .Where(userPlayback => userPlayback.UserId == userId && userPlayback.EventType == PlaybackEventType.StartSongPlayback && userPlayback.Timestamp.Year == DateTime.UtcNow.Year)
                .GroupBy(userPlayback => userPlayback.SongId)
                .OrderByDescending(group => group.Count())
                .Select(group => group.Key)
                .FirstOrDefaultAsync();

            return await context.Songs
                .Where(song => song.SongId == mostPlayedSongId)
                .FirstOrDefaultAsync();
        }

        private async Task<int> GetTotalSongsPlayedByUser(int userId)
        {
            return await context.UserPlaybackBehaviour
                .Where(userPlayback => userPlayback.UserId == userId && userPlayback.EventType == PlaybackEventType.StartSongPlayback && userPlayback.Timestamp.Year == DateTime.UtcNow.Year)
                .CountAsync();
        }

        private async Task<int> GetMostListenedSongCount(int userId)
        {
            var mostListenedSongId = await context.UserPlaybackBehaviour
                .Where(userPlayback => userPlayback.UserId == userId && userPlayback.EventType == PlaybackEventType.StartSongPlayback && userPlayback.Timestamp.Year == DateTime.UtcNow.Year)
                .GroupBy(userPlayback => userPlayback.SongId)
                .OrderByDescending(group => group.Count())
                .Select(group => group.Key)
                .FirstOrDefaultAsync();

            return await context.UserPlaybackBehaviour
                .Where(userPlayback => userPlayback.UserId == userId && userPlayback.EventType == PlaybackEventType.StartSongPlayback && userPlayback.Timestamp.Year == DateTime.UtcNow.Year && userPlayback.SongId == mostListenedSongId)
                .CountAsync();
        }

        public async Task<Tuple<string, decimal>> GetMostPlayedArtistPercentile(int userId)
        {
            var mostPlayedArtistInfo = await GetMostPlayedArtistInfoAsync(userId);
            var mostPlayedArtist = await GetMostPlayedArtist(userId, mostPlayedArtistInfo);
            var totalSongs = await GetTotalNumberOfSongs(userId);
            return new Tuple<string, decimal>(mostPlayedArtist, (decimal)mostPlayedArtistInfo.Start_Listen_Events / totalSongs);
        }

        private async Task<MostPlayedArtistInformation> GetMostPlayedArtistInfoAsync(int userId)
        {
            return await context.UserPlaybackBehaviour
                .Where(userPlayback =>
                    userPlayback.UserId == userId && userPlayback.EventType == PlaybackEventType.StartSongPlayback &&
                    userPlayback.Timestamp.Year == DateTime.UtcNow.Year)
                .Join(
                    context.Songs,
                    userPlayback => userPlayback.SongId,
                    song => song.SongId,
                    (userPlayback, song) => new { ArtistId = song.ArtistId })
                .GroupBy(result => result.ArtistId)
                .Select(group => new MostPlayedArtistInformation
                {
                    Artist_Id = group.Key,
                    Start_Listen_Events = group.Count()
                })
                .OrderByDescending(info => info.Start_Listen_Events)
                .FirstOrDefaultAsync();
        }

        private async Task<string> GetMostPlayedArtist(int userId, MostPlayedArtistInformation mostPlayedArtistInfo)
        {
            return await context.ArtistDetails
                .Where(artistDetails => artistDetails.ArtistId == mostPlayedArtistInfo.Artist_Id)
                .Select(artistDetails => artistDetails.Name)
                .FirstOrDefaultAsync();
        }

        private async Task<int> GetTotalNumberOfSongs(int userId)
        {
            return await context.UserPlaybackBehaviour
                .Where(userPlayback => userPlayback.UserId == userId && userPlayback.EventType == PlaybackEventType.StartSongPlayback && userPlayback.Timestamp.Year == DateTime.UtcNow.Year)
                .CountAsync();
        }

        public async Task<List<string>> GetTop5Genres(int userId)
        {
            return await context.UserPlaybackBehaviour
                .Where(userPlayback => userPlayback.UserId == userId && userPlayback.EventType == PlaybackEventType.StartSongPlayback && userPlayback.Timestamp.Year == DateTime.UtcNow.Year)
                .Join(
                    context.Songs,
                    userPlayback => userPlayback.SongId,
                    song => song.SongId,
                    (userPlayback, song) => song.Genre)
                .GroupBy(genre => genre)
                .OrderByDescending(group => group.Count())
                .Select(group => group.Key)
                .Take(5)
                .ToListAsync();
        }

        public async Task<List<string>> GetAllNewGenresDiscovered(int userId)
        {
            var currentYearGenres = await context.Songs
                .Where(sb => context.UserPlaybackBehaviour
                    .Where(userPlayback => userPlayback.UserId == userId && userPlayback.EventType == PlaybackEventType.StartSongPlayback && userPlayback.Timestamp.Year == DateTime.UtcNow.Year)
                    .Select(userPlayback => userPlayback.SongId)
                    .Contains(sb.SongId))
                .Select(song => song.Genre)
                .Distinct()
                .ToListAsync();

            var previousYearGenres = await context.Songs
                .Where(song => context.UserPlaybackBehaviour
                    .Where(userPlayback => userPlayback.UserId == userId && userPlayback.EventType == PlaybackEventType.StartSongPlayback && userPlayback.Timestamp.Year == DateTime.UtcNow.Year - 1)
                    .Select(userPlayback => userPlayback.SongId)
                    .Contains(song.SongId))
                .Select(song => song.Genre)
                .ToListAsync();

            return currentYearGenres.Except(previousYearGenres).ToList();
        }
    }
}