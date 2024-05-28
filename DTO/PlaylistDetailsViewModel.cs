using UBB_SE_2024_Music.Models;

namespace UBB_SE_2024_Music.DTO
{
    public class PlaylistDetailsViewModel
    {
        public Playlist Playlist { get; set; } = null!;
        public IEnumerable<Song> Songs { get; set; } = null!;
    }
}
