using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SpigotCaseStudy.Models.DbModels;

namespace SpigotCaseStudy.Controllers
{
    public class MediaItemsController : Controller
    {
        private readonly SpigotCaseStudyDbContext _context;

        public MediaItemsController(SpigotCaseStudyDbContext context)
        {
            _context = context;
        }

        //GET: api/mediaitems?q
        [HttpGet]
        public IActionResult SearchResults(string q)
        {
            List<MediaItem> mediaItemsFromDb = new List<MediaItem>();
            if (String.IsNullOrEmpty(q))
                return View(mediaItemsFromDb);

            q = q.ToLower();
            mediaItemsFromDb = _context.MediaItems.Where(x =>
            x.Title != null && x.Title.ToLower().Contains(q) ||
            x.Description != null && x.Description.ToLower().Contains(q)).ToList();
            return View(mediaItemsFromDb);


            //TODO 2019-08-04: currently nasa image/video lib api returning 503 service unavailable.
            //Confirm issue is valid, make the call to api here for media content not in the database
        }

        // GET: MediaItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.MediaItems.ToListAsync());
        }

        // GET: MediaItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mediaItem = await _context.MediaItems
                .FirstOrDefaultAsync(m => m.MediaItemId == id);
            if (mediaItem == null)
            {
                return NotFound();
            }

            return View(mediaItem);
        }

        // GET: MediaItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MediaItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MediaItemId,ExternalId,ApiEndpointId,Title,Description,ImageUrl,LargeImageUrl,VideoUrl,DateCreated,DateLastAccessed,KeyWords,MediaType")] MediaItem mediaItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mediaItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mediaItem);
        }

        // GET: MediaItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mediaItem = await _context.MediaItems.FindAsync(id);
            if (mediaItem == null)
            {
                return NotFound();
            }
            return View(mediaItem);
        }

        // POST: MediaItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MediaItemId,ExternalId,ApiEndpointId,Title,Description,ImageUrl,LargeImageUrl,VideoUrl,DateCreated,DateLastAccessed,KeyWords,MediaType")] MediaItem mediaItem)
        {
            if (id != mediaItem.MediaItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mediaItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MediaItemExists(mediaItem.MediaItemId))
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
            return View(mediaItem);
        }

        // GET: MediaItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mediaItem = await _context.MediaItems
                .FirstOrDefaultAsync(m => m.MediaItemId == id);
            if (mediaItem == null)
            {
                return NotFound();
            }

            return View(mediaItem);
        }

        // POST: MediaItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mediaItem = await _context.MediaItems.FindAsync(id);
            _context.MediaItems.Remove(mediaItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MediaItemExists(int id)
        {
            return _context.MediaItems.Any(e => e.MediaItemId == id);
        }
    }
}
