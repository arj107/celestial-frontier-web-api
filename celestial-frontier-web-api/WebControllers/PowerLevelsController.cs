using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using celestial_frontier_web_api.Models;

namespace celestial_frontier_web_api.WebControllers
{
    public class PowerLevelsController : Controller
    {
        private readonly CelestialFrontierContext _context;

        public PowerLevelsController(CelestialFrontierContext context)
        {
            _context = context;
        }

        // GET: PowerLevels
        public async Task<IActionResult> Index()
        {
              return _context.PowerLevels != null ? 
                          View(await _context.PowerLevels.ToListAsync()) :
                          Problem("Entity set 'CelestialFrontierContext.PowerLevels'  is null.");
        }

        // GET: PowerLevels/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.PowerLevels == null)
            {
                return NotFound();
            }

            var powerLevel = await _context.PowerLevels
                .FirstOrDefaultAsync(m => m.PowerLevelGuid == id);
            if (powerLevel == null)
            {
                return NotFound();
            }

            return View(powerLevel);
        }

        // GET: PowerLevels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PowerLevels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PowerLevelGuid,Text")] PowerLevel powerLevel)
        {
            if (ModelState.IsValid)
            {
                powerLevel.PowerLevelGuid = Guid.NewGuid();
                _context.Add(powerLevel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(powerLevel);
        }

        // GET: PowerLevels/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.PowerLevels == null)
            {
                return NotFound();
            }

            var powerLevel = await _context.PowerLevels.FindAsync(id);
            if (powerLevel == null)
            {
                return NotFound();
            }
            return View(powerLevel);
        }

        // POST: PowerLevels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PowerLevelGuid,Text")] PowerLevel powerLevel)
        {
            if (id != powerLevel.PowerLevelGuid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(powerLevel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PowerLevelExists(powerLevel.PowerLevelGuid))
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
            return View(powerLevel);
        }

        // GET: PowerLevels/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.PowerLevels == null)
            {
                return NotFound();
            }

            var powerLevel = await _context.PowerLevels
                .FirstOrDefaultAsync(m => m.PowerLevelGuid == id);
            if (powerLevel == null)
            {
                return NotFound();
            }

            return View(powerLevel);
        }

        // POST: PowerLevels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.PowerLevels == null)
            {
                return Problem("Entity set 'CelestialFrontierContext.PowerLevels'  is null.");
            }
            var powerLevel = await _context.PowerLevels.FindAsync(id);
            if (powerLevel != null)
            {
                _context.PowerLevels.Remove(powerLevel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PowerLevelExists(Guid id)
        {
          return (_context.PowerLevels?.Any(e => e.PowerLevelGuid == id)).GetValueOrDefault();
        }
    }
}
