using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using UBB_SE_2024_Music.Models;
using UBB_SE_2024_Music.Services;

namespace UBB_SE_2024_Music.Controllers
{
    public class PlaylistController : Controller
    {
        private readonly IPlaylistService playlistService;

        public PlaylistController(IPlaylistService playlistService)
        {
            this.playlistService = playlistService ?? throw new ArgumentNullException(nameof(playlistService));
        }

        public async Task<IActionResult> Details(int playlistId)
        {
            try
            {
                var playlist = await playlistService.GetPlaylistById(playlistId);
                if (playlist == null)
                {
                    return NotFound();
                }

                return View(playlist);
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

        public async Task<IActionResult> Index()
        {
            try
            {
                var playlists = await playlistService.GetAllPlaylists();
                return View(playlists);
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

        public async Task<IActionResult> PlaylistsOfUser(int userId)
        {
            try
            {
                var playlists = await playlistService.GetAllPlaylistsOfUser(userId);
                return View(playlists);
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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PlaylistForAddUpdateModel playlistModel)
        {
            if (!ModelState.IsValid)
            {
                return View(playlistModel);
            }

            try
            {
                var addedPlaylist = await playlistService.AddPlaylist(playlistModel);
                return RedirectToAction(nameof(Details), new { playlistId = addedPlaylist.Id });
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(playlistModel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Internal server error: " + ex.Message);
                return View("Error");
            }
        }

        public async Task<IActionResult> Edit(int playlistId)
        {
            var playlist = await playlistService.GetPlaylistById(playlistId);
            if (playlist == null)
            {
                return NotFound();
            }

            return View(playlist);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int playlistId, PlaylistForAddUpdateModel playlistModel)
        {
            if (!ModelState.IsValid)
            {
                return View(playlistModel);
            }

            try
            {
                var updated = await playlistService.UpdatePlaylist(playlistId, playlistModel);
                if (!updated)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Details), new { playlistId });
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(playlistModel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Internal server error: " + ex.Message);
                return View("Error");
            }
        }

        public async Task<IActionResult> Delete(int playlistId)
        {
            var playlist = await playlistService.GetPlaylistById(playlistId);
            if (playlist == null)
            {
                return NotFound();
            }

            return View(playlist);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int playlistId)
        {
            try
            {
                var deleted = await playlistService.DeletePlaylist(playlistId);
                if (!deleted)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
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
