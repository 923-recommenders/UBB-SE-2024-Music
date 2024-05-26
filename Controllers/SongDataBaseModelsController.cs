using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UBB_SE_2024_Music.Data;
using UBB_SE_2024_Music.Models;

namespace UBB_SE_2024_Music.Controllers
{
    public class SongDataBaseModelsController : Controller
    {
        private readonly ApplicationDbContext context;

        public SongDataBaseModelsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET: SongDataBaseModels
        public async Task<IActionResult> Index()
        {
            return View(await context.SongDataBaseModel.ToListAsync());
        }

        // GET: SongDataBaseModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var songDataBaseModel = await context.SongDataBaseModel
                .FirstOrDefaultAsync(m => m.SongId == id);
            if (songDataBaseModel == null)
            {
                return NotFound();
            }

            return View(songDataBaseModel);
        }

        // GET: SongDataBaseModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SongDataBaseModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SongId,Name,Genre,Subgenre,ArtistId,Language,Country,Album,Image")] SongDataBaseModel songDataBaseModel)
        {
            if (ModelState.IsValid)
            {
                context.Add(songDataBaseModel);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(songDataBaseModel);
        }

        // GET: SongDataBaseModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var songDataBaseModel = await context.SongDataBaseModel.FindAsync(id);
            if (songDataBaseModel == null)
            {
                return NotFound();
            }
            return View(songDataBaseModel);
        }

        // POST: SongDataBaseModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SongId,Name,Genre,Subgenre,ArtistId,Language,Country,Album,Image")] SongDataBaseModel songDataBaseModel)
        {
            if (id != songDataBaseModel.SongId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(songDataBaseModel);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SongDataBaseModelExists(songDataBaseModel.SongId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(songDataBaseModel);
        }

        // GET: SongDataBaseModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var songDataBaseModel = await context.SongDataBaseModel
                .FirstOrDefaultAsync(m => m.SongId == id);
            if (songDataBaseModel == null)
            {
                return NotFound();
            }

            return View(songDataBaseModel);
        }

        // POST: SongDataBaseModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var songDataBaseModel = await context.SongDataBaseModel.FindAsync(id);
            if (songDataBaseModel != null)
            {
                context.SongDataBaseModel.Remove(songDataBaseModel);
            }

            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SongDataBaseModelExists(int id)
        {
            return context.SongDataBaseModel.Any(e => e.SongId == id);
        }
    }
}
