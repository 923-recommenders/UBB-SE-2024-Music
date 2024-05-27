using Microsoft.AspNetCore.Mvc;

namespace UBB_SE_2024_Music.Controllers
{
    public class UserDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
