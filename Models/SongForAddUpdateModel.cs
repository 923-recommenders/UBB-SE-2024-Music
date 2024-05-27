namespace UBB_SE_2024_Music.Models
{
    public class SongForAddUpdateModel
    {
        public string ArtistName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string Subgenre { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public bool IsExplicit { get; set; }
        public string SongPath { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
    }
}
