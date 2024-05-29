using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UBB_SE_2024_Music.Services;

namespace UBB_SE_2024_Music.Controllers
{
    [Authorize]
    public class ArtistDashboardController : Controller
    {
        private readonly ArtistDashboardService artistDashboardService;

        public ArtistDashboardController(ArtistDashboardService artistDashboardService)
        {
            this.artistDashboardService = artistDashboardService;
        }

        public async Task<IActionResult> Index(string artistName)
        {
            var artistSongs = await artistDashboardService.GetAllArtistSongsAsync(artistName);
            if (artistSongs == null || !artistSongs.Any())
            {
                // Handle empty results
                ViewBag.Message = "No songs found.";
                return View("NoSongs");
            }
            return View(artistSongs);
        }

        public async Task<IActionResult> Search(string title)
        {
            var songs = await artistDashboardService.SearchSongsByTitleAsync(title);
            if (songs == null || !songs.Any())
            {
                // Handle empty search results, e.g., show a message or redirect to another view.
                ViewBag.Message = "No songs found matching your search criteria.";
                return View("NoSongs");
            }
            return View(songs);
        }

        public async Task<IActionResult> Details(int songId)
        {
            var songInfo = await artistDashboardService.GetSongInformationAsync(songId);
            if (songInfo == null)
            {
                // Handle case where song is not found.
                ViewBag.Message = "Song not found.";
                return View("NoSongs");
            }
            return View(songInfo);
        }
    }
}
