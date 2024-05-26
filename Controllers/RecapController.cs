using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using UBB_SE_2024_Music.DTO;
using UBB_SE_2024_Music.Services;

namespace UBB_SE_2024_Music.Controllers
{
    public class RecapController : Controller
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly IRecapService recapService;

        public RecapController(IHttpClientFactory clientFactory, IRecapService recapService)
        {
            this.clientFactory = clientFactory;
            this.recapService = recapService;
        }

        public async Task<IActionResult> Index(int userId)
        {
            var viewModel = await GetEndOfYearRecap(userId);
            return View("FirstScreen", viewModel);
        }

        public async Task<IActionResult> NextScreen(int userId, int pageIndex)
        {
            var viewModel = await GetEndOfYearRecap(userId);

            string viewName = pageIndex switch
            {
                1 => "FirstScreen",
                2 => "MinutesListenedScreen",
                3 => "Top5SongsScreen",
                4 => "MostPlayedSongScreen",
                5 => "MostPlayedArtistScreen",
                6 => "MostListenedGenresScreen",
                7 => "NewGenresScreen",
                8 => "ListenerPersonalityScreen",
                _ => "End"
                // make sure you make it so it automatically goes to the other screens !!!
            };

            if (viewName == "End")
            {
                return RedirectToAction("Index", new { userId });
            }

            ViewBag.PageIndex = pageIndex;
            return View(viewName, viewModel);
        }

        private async Task<EndOfYearRecapViewModel> GetEndOfYearRecap(int userId)
        {
            var endOfYearRecap = await recapService.GenerateEndOfYearRecap(userId);
            return endOfYearRecap;
        }
    }
}
