using Microsoft.AspNetCore.Mvc;
using UBB_SE_2024_Music.Services;

namespace UBB_SE_2024_Music.Controllers
{
    public class ArtistDashboardController : Controller
    {
        private readonly ArtistDashboardService artistDashboardService;

        public ArtistDashboardController(ArtistDashboardService artistDashboardService)
        {
            this.artistDashboardService = artistDashboardService;
        }

        public async Task<IActionResult> Index(int artistId)
        {
            var artistSongs = await artistDashboardService.GetAllArtistSongsAsync(artistId);
            return View(artistSongs);
        }

        public async Task<IActionResult> Search(string title)
        {
            var songs = await artistDashboardService.SearchSongsByTitleAsync(title);
            return View(songs);
        }

        public async Task<IActionResult> Details(int songId)
        {
            var songInfo = await artistDashboardService.GetSongInformationAsync(songId);
            return View(songInfo);
        }
    }
}
