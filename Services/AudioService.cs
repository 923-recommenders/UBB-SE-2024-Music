using Plugin.Maui.Audio;
using UBB_SE_2024_Music.Models;
using UBB_SE_2024_Music.Services.Interfaces;

namespace UBB_SE_2024_Music.Services
{
    public class AudioService : IAudioService
    {
        public IAudioManager AppAudioManager { get; }
        public List<IAudioPlayer> AppAudioPlayers { get; set; } = new List<IAudioPlayer>();

        public AudioService()
        {
            AppAudioManager = AudioManager.Current;
        }

        public void StopAllSounds()
        {
            foreach (IAudioPlayer player in AppAudioPlayers)
            {
                player.Stop();
            }

            AppAudioPlayers = new List<IAudioPlayer>();
        }

        public void PlayIndividualSound(Sound sound)
        {
            StopAllSounds();

            Stream track = FileSystem.OpenAppPackageFileAsync(sound.SoundFilePath).Result;
            IAudioPlayer player = AppAudioManager.CreatePlayer(track);

            player.Play();
            AppAudioPlayers.Add(player);
        }

        public void PlaySounds(IEnumerable<Sound> sounds)
        {
            StopAllSounds();

            foreach (Sound sound in sounds)
            {
                Stream track = FileSystem.OpenAppPackageFileAsync(sound.SoundFilePath).Result;
                IAudioPlayer player = AppAudioManager.CreatePlayer(track);

                player.Play();
                AppAudioPlayers.Add(player);
            }
        }

        public void PlaySong(Song song)
        {
            StopAllSounds();

            Stream track = FileSystem.OpenAppPackageFileAsync(song.SongPath).Result;
            IAudioPlayer player = AppAudioManager.CreatePlayer(track);

            player.Play();
            AppAudioPlayers.Add(player);
        }
    }
}
