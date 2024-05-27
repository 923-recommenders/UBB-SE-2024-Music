using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using UBB_SE_2024_Music.Models;
using UBB_SE_2024_Music.Services.Interfaces;

namespace UBB_SE_2024_Music.Controllers.WebAPI
{
    [Route("api/Songs")]
    [ApiController]
    public class SongControllerWebAPI : Controller
    {
        private readonly ISongService songService;

        public SongControllerWebAPI(ISongService songService)
        {
            this.songService = songService;
        }

        [HttpGet("{songId}", Name = "GetSong")]
        public async Task<IActionResult> GetSong(int songId)
        {
            try
            {
                var song = await songService.GetSongById(songId);
                if (song == null)
                {
                    return NotFound();
                }

                return Ok(song);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSongs()
        {
            try
            {
                var songs = await songService.GetAllSongs();

                return Ok(songs);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddSong([FromBody] SongForAddUpdateModel songModel)
        {
            try
            {
                var addedSong = await songService.AddSong(songModel);

                return CreatedAtRoute(
                    "GetSong", new { songId = addedSong.SongId }, addedSong);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpDelete("{songId}")]
        public async Task<IActionResult> DeleteSong(int songId)
        {
            try
            {
                var deleted = await songService.DeleteSong(songId);
                if (!deleted)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPut("{songId}")]
        public async Task<IActionResult> UpdateSong(int songId, [FromBody] SongForAddUpdateModel songModel)
        {
            try
            {
                var updated = await songService.UpdateSong(songId, songModel);
                if (!updated)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
