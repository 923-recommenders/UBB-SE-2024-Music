using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using UBB_SE_2024_Music.Services.Interfaces;

namespace UBB_SE_2024_Music.Controllers
{
    public class SongController : Controller
    {
        private readonly ISongService songService;

        public SongController(ISongService songService)
        {
            this.songService = songService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var songs = await songService.GetAllSongs();
                return View(songs);
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View("Error");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Internal server error: " + ex.Message);
                return View("Error");
            }
        }
    }
}
