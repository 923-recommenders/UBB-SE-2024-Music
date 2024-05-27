using Microsoft.EntityFrameworkCore;
using UBB_SE_2024_Music.Data;
using UBB_SE_2024_Music.Models;
using UBB_SE_2024_Music.Repositories.Interfaces;

namespace UBB_SE_2024_Music.Repositories
{
    public class SongRepository : ISongRepository
    {
        private readonly ApplicationDbContext context;

        public SongRepository(ApplicationDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
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
    }
}
