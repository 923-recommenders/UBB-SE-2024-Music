using UBB_SE_2024_Music.Enums;

namespace UBB_SE_2024_Music.Models
{
    public class SoundForAddUpdateModel
    {
        public string Name { get; set; } = null!;
        public SoundType Type { get; set; }
        public string SoundFilePath { get; set; } = null!;
    }
}
