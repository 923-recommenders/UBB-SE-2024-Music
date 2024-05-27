using UBB_SE_2024_Music.Data;
using UBB_SE_2024_Music.Models;
using Microsoft.EntityFrameworkCore;

namespace UBB_SE_2024_Music.Repositories
{
    public class UserRepository : Repository<Users>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Users> GetUserByUsername(string username)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.UserName == username);

            if (user == null)
            {
                return null;
            }

            return user;
        }
        public async Task EnableOrDisableArtist(Users user)
        {
            if (user.Role == 1)
            {
                user.Role = 2;
            }
            else
            {
                user.Role = 1;
            }
            await context.SaveChangesAsync();
        }
    }
}