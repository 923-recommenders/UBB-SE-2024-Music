namespace UBB_SE_2024_Music.Services
{
    public interface IUserService
    {
        Task<bool> RegisterUser(string username, string country, string email, int age);
        Task<bool> EnableOrDisableArtist(int userId);
    }
}