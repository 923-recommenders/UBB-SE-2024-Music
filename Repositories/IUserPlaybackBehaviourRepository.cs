using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2024_Music.Models;
namespace UBB_SE_2024_Music.Repositories
{
    public interface IUserPlaybackBehaviourRepository
    {
        Task<UserPlaybackBehaviour> GetUserPlaybackBehaviour(int userId, int? songId = null, DateTime? timestamp = null);
        Task<List<UserPlaybackBehaviour>> GetListOfUserPlaybackBehaviourEntities(int userId);
    }
}
