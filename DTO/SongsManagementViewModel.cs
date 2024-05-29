using UBB_SE_2024_Music.Models;

namespace UBB_SE_2024_Music.DTO
{
    public class SongsManagementViewModel
    {
        public Playlist Playlist { get; set; }
        public IEnumerable<Song> Songs { get; set; }
    }
}
