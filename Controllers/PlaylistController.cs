using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UBB_SE_2024_Music.DTO;
using UBB_SE_2024_Music.Models;
using UBB_SE_2024_Music.Services;

namespace UBB_SE_2024_Music.Controllers
{
    [Authorize]
    public class PlaylistController : Controller
    {
        private readonly IPlaylistService playlistService;
        private readonly IPlaylistSongItemService playlistSongItemService;

        public PlaylistController(IPlaylistService playlistService, IPlaylistSongItemService playlistSongItemService)
        {
            this.playlistService = playlistService ?? throw new ArgumentNullException(nameof(playlistService));
            this.playlistSongItemService = playlistSongItemService ?? throw new ArgumentNullException(nameof(playlistSongItemService));
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var playlist = await playlistService.GetPlaylistById(id);
                if (playlist == null)
                {
                    return NotFound();
                }

                var songs = await playlistSongItemService.GetSongsByPlaylistId(playlist.Id);

                var viewModel = new PlaylistDetailsViewModel()
                {
                    Playlist = playlist,
                    Songs = songs
                };

                return View(viewModel);
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
                return RedirectToAction(nameof(Details), new { id = addedPlaylist.Id });
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

        public async Task<IActionResult> Edit(int id)
        {
            var playlist = await playlistService.GetPlaylistById(id);
            if (playlist == null)
            {
                return NotFound();
            }

            return View(playlist);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, PlaylistForAddUpdateModel playlistModel)
        {
            if (!ModelState.IsValid)
            {
                return View(playlistModel);
            }

            try
            {
                var updated = await playlistService.UpdatePlaylist(id, playlistModel);
                if (!updated)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Details), new { id });
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

        public async Task<IActionResult> Delete(int id)
        {
            var playlist = await playlistService.GetPlaylistById(id);
            if (playlist == null)
            {
                return NotFound();
            }

            return View(playlist);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var deleted = await playlistService.DeletePlaylist(id);
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
