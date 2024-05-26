using Microsoft.AspNetCore.Mvc;
using UBB_SE_2024_Music.Services;
using UBB_SE_2024_Music.DTO;

namespace UBB_SE_2024_Music.Controllers
{
    public class TopGenresController : Controller
    {
        private readonly ITopGenresService topGenresService;

        public TopGenresController(ITopGenresService topGenresService)
        {
            this.topGenresService = topGenresService;
        }

        [Route("TopGenres/Index/{month}/{year}")]
        public IActionResult Index(int month, int year)
        {
            var topGenres = topGenresService.GetTop3Genres(month, year).Result;
            ViewBag.TopGenres = topGenres;
            return View();
        }

        public IActionResult SubGenres(int month, int year)
        {
            var topSubGenres = topGenresService.GetTop3SubGenres(month, year).Result;
            ViewBag.TopSubGenres = topSubGenres;
            return View("SubGenres");
        }
    }
}
