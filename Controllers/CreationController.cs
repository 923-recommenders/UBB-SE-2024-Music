using Microsoft.AspNetCore.Mvc;
using UBB_SE_2024_Music.Models;
using UBB_SE_2024_Music.Services;
using UBB_SE_2024_Music.Services.Interfaces;

namespace UBB_SE_2024_Music.Controllers
{
    public class CreationController : Controller
    {
        private readonly ICreationService creationService;
        private readonly ISoundService soundService;

        public CreationController(ICreationService creationService, ISoundService soundService)
        {
            this.creationService = creationService ?? throw new ArgumentNullException(nameof(creationService));
            this.soundService = soundService ?? throw new ArgumentNullException(nameof(soundService));
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var sounds = await soundService.GetAllSounds();
                return View(sounds);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Internal server error: " + ex.Message;
                return View("Error");
            }
        }

        public async Task<IActionResult> AllCreations()
        {
            try
            {
                var creations = await creationService.GetAllCreations();
                return View(creations);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Internal server error: " + ex.Message;
                return View("Error");
            }
        }

        public async Task<IActionResult> LoadCreation(int creationId)
        {
            try
            {
                await creationService.LoadCreation(creationId);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Internal server error: " + ex.Message;
                return View("Error");
            }
        }

        public IActionResult AddSound()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddSound(Sound sound)
        {
            try
            {
                creationService.AddSoundToCreation(sound);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Internal server error: " + ex.Message;
                return View("Error");
            }
        }

        public IActionResult SaveCreation()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveCreation(string title)
        {
            try
            {
                await creationService.SaveCreation(title);
                return RedirectToAction(nameof(AllCreations));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Internal server error: " + ex.Message;
                return View("Error");
            }
        }
    }
}

