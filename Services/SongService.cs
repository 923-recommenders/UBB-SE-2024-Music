using System.ComponentModel.DataAnnotations;
using Microsoft.IdentityModel.Tokens;
using UBB_SE_2024_Music.Models;
using UBB_SE_2024_Music.Repositories;

namespace UBB_SE_2024_Music.Services
{
    public interface ISongService
    {
        Task<Song?> GetSongById(int songId);
        Task<IEnumerable<Song>> GetAllSongs();
        Task AddSong(Song song);
        Task DeleteSong(Song song);
    }
    public class SongService : ISongService
    {
        private readonly IRepository<Song> repository;
        private readonly IRepository<Users> userRepository;

        public SongService(IRepository<Song> repository, IRepository<Users> userRepository)
        {
            this.repository = repository;
            this.userRepository = userRepository;
        }
        private static bool ValidSong(Song song)
        {
            if (song.Name.IsNullOrEmpty() || song.ArtistName.IsNullOrEmpty())
            {
                return false;
            }

            if (song.SongPath.IsNullOrEmpty() || song.ImagePath.IsNullOrEmpty())
            {
                return false;
            }

            return true;
        }

        public async Task<Users> GetUserById(int id)
        {
            return await userRepository.GetById(id);
        }

        public async Task<IEnumerable<Song>> GetAllSongs()
        {
            return await repository.GetAll();
        }

        public async Task<Song> GetSongById(int id)
        {
            return await repository.GetById(id);
        }

        public async Task AddSong(Song song)
        {
            if (!ValidSong(song))
            {
                throw new ValidationException("Invalid song data.");
            }

            try
            {
                await repository.Add(song);
            }
            catch (Exception)
            {
                throw new ArgumentException("Database internal song add error");
            }
        }

        public async Task DeleteSong(Song song)
        {
            try
            {
                await repository.Delete(song);
            }
            catch (Exception)
            {
                throw new ArgumentException("Database internal song delete eror");
            }
        }
    }
}

