using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UBB_SE_2024_Music.DTO;
using UBB_SE_2024_Music.Enums;
using UBB_SE_2024_Music.Services;

namespace UBB_SE_2024_Music.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RecapControllerNormal : ControllerBase
    {
        private readonly IRecapService recapService;

        public RecapControllerNormal(IRecapService recapService)
        {
            this.recapService = recapService;
        }

        [HttpGet("getTop5MostListenedSongs/{userId}")]
        public async Task<ActionResult<List<SongBasicInformation>>> GetTheTop5MostListenedSongs(int userId)
        {
            var top5MostListenedSongs = await recapService.GetTheTop5MostListenedSongs(userId);
            return Ok(top5MostListenedSongs);
        }

        [HttpGet("getMostPlayedSongPercentile/{userId}")]
        public async Task<ActionResult<Tuple<SongBasicInformation, decimal>>> GetTheMostPlayedSongPercentile(int userId)
        {
            var mostPlayedSongPercentile = await recapService.GetTheMostPlayedSongPercentile(userId);
            return Ok(mostPlayedSongPercentile);
        }

        [HttpGet("getMostPlayedArtistPercentile/{userId}")]
        public async Task<ActionResult<Tuple<string, decimal>>> GetTheMostPlayedArtistPercentile(int userId)
        {
            var mostPlayedArtistPercentile = await recapService.GetTheMostPlayedArtistPercentile(userId);
            return Ok(mostPlayedArtistPercentile);
        }

        [HttpGet("getTotalMinutesListened/{userId}")]
        public async Task<ActionResult<int>> GetTotalMinutesListened(int userId)
        {
            var totalMinutesListened = await recapService.GetTotalMinutesListened(userId);
            return Ok(totalMinutesListened);
        }

        [HttpGet("getTop5Genres/{userId}")]
        public async Task<ActionResult<List<string>>> GetTheTop5Genres(int userId)
        {
            var top5Genres = await recapService.GetTheTop5Genres(userId);
            return Ok(top5Genres);
        }

        [HttpGet("getNewGenresDiscovered/{userId}")]
        public async Task<ActionResult<List<string>>> GetNewGenresDiscovered(int userId)
        {
            var newGenresDiscovered = await recapService.GetNewGenresDiscovered(userId);
            return Ok(newGenresDiscovered);
        }

        [HttpGet("getListenerPersonality/{userId}")]
        public async Task<ActionResult<ListenerPersonality>> GetListenerPersonality(int userId)
        {
            var listenerPersonality = await recapService.GetListenerPersonality(userId);
            return Ok(listenerPersonality);
        }

        [HttpGet("getEndOfYearRecap/{userId}")]
        public async Task<ActionResult<EndOfYearRecapViewModel>> GenerateEndOfYearRecap(int userId)
        {
            var endOfYearRecap = await recapService.GenerateEndOfYearRecap(userId);
            return Ok(endOfYearRecap);
        }
    }
}