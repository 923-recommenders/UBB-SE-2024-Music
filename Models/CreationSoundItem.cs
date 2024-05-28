using System.ComponentModel.DataAnnotations.Schema;
using NamespaceCBlurred.Data.Models;

namespace UBB_SE_2024_Music.Models
{
    public class CreationSoundItem
    {
        public int Id { get; set; }
        [ForeignKey("Sound")]
        public int SoundId { get; set; }
        public Sound Sound { get; set; } = null!;

        [ForeignKey("Creation")]
        public int CreationId { get; set; }
        public Creation Creation { get; set; } = null!;
    }
}
