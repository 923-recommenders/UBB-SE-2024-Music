using UBB_SE_2024_Music.Models;

namespace UBB_SE_2024_Music.Services
{
    public interface ISoundService
    {
        Task<Sound?> GetSoundById(int soundId);
        Task<IEnumerable<Sound>> GetAllSounds();
        Task<IEnumerable<Sound>> FilterSoundsByType(SoundType type);
        Task<Sound> AddSound(SoundForAddUpdateModel soundModel);
        Task<bool> DeleteSound(int soundId);
        Task<bool> UpdateSound(int soundId, SoundForAddUpdateModel soundModel);
    }
}
