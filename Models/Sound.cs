using UBB_SE_2024_Music.Enums;

namespace UBB_SE_2024_Music.Models
{
    public class Sound
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public SoundType Type { get; set; }
        public string SoundFilePath { get; set; } = string.Empty;

        public Sound()
        {
        }

        public Sound(int id, string name, SoundType type, string soundFilePath = "")
        {
            this.Id = id;
            this.Name = name;
            this.Type = type;
            this.SoundFilePath = soundFilePath;
        }
    }
}