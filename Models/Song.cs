using Microsoft.EntityFrameworkCore;

namespace UBB_SE_2024_Music.Models
{
    [PrimaryKey(nameof(SongId))]
    public class Song
    {
        public int SongId { get; set; } = 0;
        public int ArtistId { get; set; } = 0;
        public string ArtistName { get; set; } = "DefaultArtistName";
        public string Name { get; set; } = "DefaultName";
        public string Genre { get; set; } = "DefaultGenre";
        public string Subgenre { get; set; } = "DefaultSubgenre";
        public string Language { get; set; } = "DefaultLanguage";
        public string Country { get; set; } = "DefaultCountry";
        public bool IsExplicit { get; set; } = false;
        public string Album { get; set; } = "DefaultAlbum";
        public string SongPath { get; set; } = "song_default.mp3";
        public string ImagePath { get; set; } = "song_img_default.png";
    }
}