using NamespaceCBlurred.Data.Models;
using UBB_SE_2024_Music.Models;

namespace UBB_SE_2024_Music.Repositories.Interfaces
{
    public interface ICreationRepository
    {
        void AddSoundToCreation(Sound sound);
        bool DeleteSoundFromCreation(int soundId);
        IEnumerable<Sound> GetAllSoundsOfCreation();
        bool CreationContainsSound(int soundId);
        Task SaveCreation(string title);
        Task LoadCreation(int creationId);
        Task<IEnumerable<Creation>> GetAllCreations();
    }
}
