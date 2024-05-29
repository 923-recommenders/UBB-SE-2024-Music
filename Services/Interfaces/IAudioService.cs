using Plugin.Maui.Audio;
using UBB_SE_2024_Music.Models;

namespace UBB_SE_2024_Music.Services.Interfaces
{
    public interface IAudioService
    {
        void StopAllSounds();
        void PlayIndividualSound(Sound sound);
        void PlaySounds(IEnumerable<Sound> sounds);
        void PlaySong(Song song);
    }
}
