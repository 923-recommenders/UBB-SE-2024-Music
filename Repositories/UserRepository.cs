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

        public async Task BcryptPassword(Users user)
        {
            string salt = "a$^#shfdyu$^%agb@#%jqd#!cbjhacs!@#!b";
            string encryptedPassword = user.Password + salt;
            user.Password = BCrypt.Net.BCrypt.HashPassword(encryptedPassword);
            await _context.SaveChangesAsync();
        }

        public bool VerifyPassword(string inputPassword, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(inputPassword + "a$^#shfdyu$^%agb@#%jqd#!cbjhacs!@#!b", hashedPassword);
        }

        public async Task<Users> GetUserByUsername(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);

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
            await _context.SaveChangesAsync();
        }
    }
}