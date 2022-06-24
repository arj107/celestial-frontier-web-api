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
    public class PointDefenseSystemsController : Controller
    {
        private readonly CelestialFrontierContext _context;

        public PointDefenseSystemsController(CelestialFrontierContext context)
        {
            _context = context;
        }

        // GET: PointDefenseSystems
        public async Task<IActionResult> Index()
        {
            var celestialFrontierContext = _context.PointDefenseSystems.Include(p => p.PowerLevel);
            return View(await celestialFrontierContext.ToListAsync());
        }

        // GET: PointDefenseSystems/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.PointDefenseSystems == null)
            {
                return NotFound();
            }

            var pointDefenseSystem = await _context.PointDefenseSystems
                .Include(p => p.PowerLevel)
                .FirstOrDefaultAsync(m => m.PointDefenseSystemGuid == id);
            if (pointDefenseSystem == null)
            {
                return NotFound();
            }

            return View(pointDefenseSystem);
        }

        // GET: PointDefenseSystems/Create
        public IActionResult Create()
        {
            ViewData["PowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text");
            return View();
        }

        // POST: PointDefenseSystems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PointDefenseSystemGuid,PowerLevelGuid,ModulePointsCost,Mass,SpecificHeatCapacity,CountermeasureCapacity,CountermeasureRegenerationRate,Efficiency,Title,Description")] PointDefenseSystem pointDefenseSystem)
        {
            if (ModelState.IsValid)
            {
                pointDefenseSystem.PointDefenseSystemGuid = Guid.NewGuid();
                _context.Add(pointDefenseSystem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text", pointDefenseSystem.PowerLevelGuid);
            return View(pointDefenseSystem);
        }

        // GET: PointDefenseSystems/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.PointDefenseSystems == null)
            {
                return NotFound();
            }

            var pointDefenseSystem = await _context.PointDefenseSystems.FindAsync(id);
            if (pointDefenseSystem == null)
            {
                return NotFound();
            }
            ViewData["PowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text", pointDefenseSystem.PowerLevelGuid);
            return View(pointDefenseSystem);
        }

        // POST: PointDefenseSystems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PointDefenseSystemGuid,PowerLevelGuid,ModulePointsCost,Mass,SpecificHeatCapacity,CountermeasureCapacity,CountermeasureRegenerationRate,Efficiency,Title,Description")] PointDefenseSystem pointDefenseSystem)
        {
            if (id != pointDefenseSystem.PointDefenseSystemGuid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pointDefenseSystem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PointDefenseSystemExists(pointDefenseSystem.PointDefenseSystemGuid))
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
            ViewData["PowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text", pointDefenseSystem.PowerLevelGuid);
            return View(pointDefenseSystem);
        }

        // GET: PointDefenseSystems/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.PointDefenseSystems == null)
            {
                return NotFound();
            }

            var pointDefenseSystem = await _context.PointDefenseSystems
                .Include(p => p.PowerLevel)
                .FirstOrDefaultAsync(m => m.PointDefenseSystemGuid == id);
            if (pointDefenseSystem == null)
            {
                return NotFound();
            }

            return View(pointDefenseSystem);
        }

        // POST: PointDefenseSystems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.PointDefenseSystems == null)
            {
                return Problem("Entity set 'CelestialFrontierContext.PointDefenseSystems'  is null.");
            }
            var pointDefenseSystem = await _context.PointDefenseSystems.FindAsync(id);
            if (pointDefenseSystem != null)
            {
                _context.PointDefenseSystems.Remove(pointDefenseSystem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PointDefenseSystemExists(Guid id)
        {
          return (_context.PointDefenseSystems?.Any(e => e.PointDefenseSystemGuid == id)).GetValueOrDefault();
        }
    }
}
