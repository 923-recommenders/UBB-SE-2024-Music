using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UBB_SE_2024_Music.Services;

namespace UBB_SE_2024_Music.Controllers
{
    public class Top5SongsController : Controller
    {
        private readonly IRecapService recapService;

        public Top5SongsController(IRecapService rrecapService)
        {
            recapService = rrecapService;
        }

        // /Top5Songs?UserId=
        public async Task<IActionResult> Index(int userId)
        {
            var topFiveSongs = await recapService.GetTheTop5MostListenedSongs(userId);
            return View(topFiveSongs);
        }
    }
}