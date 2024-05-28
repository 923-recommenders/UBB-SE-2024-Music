using Microsoft.EntityFrameworkCore;
using UBB_SE_2024_Music.Enums;

namespace UBB_SE_2024_Music.Models
{
    [PrimaryKey(nameof(UserId))]
    public class Users
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public int Role { get; set; }
    }
}