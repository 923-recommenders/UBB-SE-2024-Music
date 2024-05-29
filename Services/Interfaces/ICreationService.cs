using UBB_SE_2024_Music.Models;

namespace UBB_SE_2024_Music.Services.Interfaces
{
    public interface ICreationService
    {
        void AddSoundToCreation(Sound sound);
        bool DeleteSoundFromCreation(int soundId);
        IEnumerable<Sound> GetAllSoundsOfCreation();
        Task SaveCreation(string title);
        Task LoadCreation(int creationId);
        Task<IEnumerable<Creation>> GetAllCreations();
    }
}
