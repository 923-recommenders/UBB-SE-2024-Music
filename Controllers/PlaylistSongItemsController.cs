using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using UBB_SE_2024_Music.DTO;
using UBB_SE_2024_Music.Models;
using UBB_SE_2024_Music.Services;
using UBB_SE_2024_Music.Services.Interfaces;

namespace UBB_SE_2024_Music.Controllers
{
    public class PlaylistSongItemsController : Controller
    {
        private readonly IPlaylistSongItemService _playlistSongItemService;
        private readonly IPlaylistService _playlistService;
        private readonly ISongService _songService;

        public PlaylistSongItemsController(IPlaylistSongItemService playlistSongItemService)
        {
            _playlistSongItemService = playlistSongItemService ?? throw new ArgumentNullException(nameof(playlistSongItemService));
        }

        public async Task<IActionResult> SongsManagement(int id)
        {
            try
            {
                var playlist = await _playlistService.GetPlaylistById(id);
                if (playlist == null)
                {
                    return NotFound();
                }

                var songs = await _playlistSongItemService.GetSongsByPlaylistId(id);
                var model = new SongsManagementViewModel
                {
                    Playlist = playlist,
                    Songs = songs
                };

                return View(model);
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

        public IActionResult AddSong(int playlistId)
        {
            var model = new SongCreateViewModel
            {
                PlaylistId = playlistId
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddSong(SongCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await _playlistSongItemService.AddSongToPlaylist(model.PlaylistId, model.SongId);
                return RedirectToAction(nameof(SongsManagement), new { id = model.PlaylistId });
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Internal server error: " + ex.Message);
                return View("Error");
            }
        }

        public async Task<IActionResult> EditSong(int id, int playlistId)
        {
            try
            {
                var song = await _songService.GetSongById(id);
                if (song == null)
                {
                    return NotFound();
                }

                var model = new SongEditViewModel
                {
                    PlaylistId = playlistId,
                    SongId = id,
                    Name = song.Name,
                    ArtistName = song.ArtistName
                };

                return View(model);
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

        [HttpPost]
        public async Task<IActionResult> EditSong(SongEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var songModel = new SongForAddUpdateModel
                {
                    Name = model.Name,
                    ArtistName = model.ArtistName
                };

                var updated = await _songService.UpdateSong(model.SongId, songModel);
                if (!updated)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(SongsManagement), new { id = model.PlaylistId });
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Internal server error: " + ex.Message);
                return View("Error");
            }
        }

        public async Task<IActionResult> DeleteSong(int id, int playlistId)
        {
            try
            {
                var song = await _songService.GetSongById(id);
                if (song == null)
                {
                    return NotFound();
                }

                var model = new SongDeleteViewModel
                {
                    PlaylistId = playlistId,
                    SongId = id,
                    Name = song.Name,
                    ArtistName = song.ArtistName
                };

                return View(model);
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

        [HttpPost, ActionName("DeleteSong")]
        public async Task<IActionResult> DeleteSongConfirmed(int id, int playlistId)
        {
            try
            {
                var deleted = await _playlistSongItemService.DeleteSongFromPlaylist(id, playlistId);
                if (!deleted)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(SongsManagement), new { id = playlistId });
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
