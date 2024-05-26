using UBB_SE_2024_Music.Enums;
using UBB_SE_2024_Music.Models;
using UBB_SE_2024_Music.DTO;
using UBB_SE_2024_Music.Repositories;

namespace UBB_SE_2024_Music.Services
{
    public class FullDetailsOnSongService : IFullDetailsOnSongService
    {
        private readonly IRepository<UserPlaybackBehaviour> userPlaybackBehaviourRepo;

        public FullDetailsOnSongService(IRepository<UserPlaybackBehaviour> userPlaybackBehaviourRepo)
        {
            this.userPlaybackBehaviourRepo = userPlaybackBehaviourRepo;
        }

        /// <summary>
        /// Retrieves full details on a song, including total minutes listened,
        /// total plays, likes, dislikes, and skips.
        /// </summary>
        /// <param name="songId">The ID of the song.</param>
        /// <returns>A <see cref="FullDetailsOnSong"/> object containing the detailed
        /// information about the song, or null if the song is not found.</returns>
        public async Task<FullDetailsOnSong> GetFullDetailsOnSong(int songId)
        {
            FullDetailsOnSong currentSongDetails = new ();
            DateTime start = new ();
            bool foundSongCheck = false;
            var userPlaybackBehaviourEntities = await userPlaybackBehaviourRepo.GetAll();
            foreach (UserPlaybackBehaviour action in userPlaybackBehaviourEntities)
            {
                if (action.SongId == songId)
                {
                    foundSongCheck = true;

                    switch (action.EventType)
                    {
                        case PlaybackEventType.StartSongPlayback:
                            start = action.Timestamp;
                            break;
                        case PlaybackEventType.EndSongPlayback:
                            int minutes = (action.Timestamp - start).Minutes;
                            currentSongDetails.TotalMinutesListened += minutes;
                            currentSongDetails.TotalPlays++;
                            break;
                        case PlaybackEventType.Like:
                            currentSongDetails.TotalLikes++;
                            break;
                        case PlaybackEventType.Dislike:
                            currentSongDetails.TotalDislikes++;
                            break;
                        case PlaybackEventType.Skip:
                            currentSongDetails.TotalSkips++;
                            break;
                    }
                }
            }
            if (!foundSongCheck)
            {
                return null;
            }
            return currentSongDetails;
        }

        /// <summary>
        /// Retrieves the details of a song for the current month,
        /// including total minutes listened, total plays, likes, dislikes,
        /// and skips.
        /// </summary>
        /// <param name="songId">The ID of the song.</param>
        /// <returns>A <see cref="FullDetailsOnSong"/> object containing the
        /// detailed information about the song for the current month.</returns>
        public async Task<FullDetailsOnSong> GetCurrentMonthDetails(int songId)
        {
            FullDetailsOnSong currentSongDetails = new ();
            var userPlaybackBehaviourEntities = await userPlaybackBehaviourRepo.GetAll();
            foreach (UserPlaybackBehaviour action in userPlaybackBehaviourEntities)
            {
                if (action.SongId == songId && action.Timestamp.Month == DateTime.Now.Month && action.Timestamp.Year == DateTime.Now.Year)
                {
                    switch (action.EventType)
                    {
                        case PlaybackEventType.StartSongPlayback:
                            break;
                        case PlaybackEventType.EndSongPlayback:
                            int minutes = (action.Timestamp - DateTime.Now).Minutes;
                            currentSongDetails.TotalMinutesListened += minutes;
                            currentSongDetails.TotalPlays++;
                            break;
                        case PlaybackEventType.Like:
                            currentSongDetails.TotalLikes++;
                            break;
                        case PlaybackEventType.Dislike:
                            currentSongDetails.TotalDislikes++;
                            break;
                        case PlaybackEventType.Skip:
                            currentSongDetails.TotalSkips++;
                            break;
                    }
                }
            }
            return currentSongDetails;
        }
    }
}
