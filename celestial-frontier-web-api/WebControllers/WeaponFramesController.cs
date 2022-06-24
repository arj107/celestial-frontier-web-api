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
    public class WeaponFramesController : Controller
    {
        private readonly CelestialFrontierContext _context;

        public WeaponFramesController(CelestialFrontierContext context)
        {
            _context = context;
        }

        // GET: WeaponFrames
        public async Task<IActionResult> Index()
        {
            var celestialFrontierContext = _context.WeaponFrames.Include(w => w.PowerLevel).Include(w => w.WeaponFrameType);
            return View(await celestialFrontierContext.ToListAsync());
        }

        // GET: WeaponFrames/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.WeaponFrames == null)
            {
                return NotFound();
            }

            var weaponFrame = await _context.WeaponFrames
                .Include(w => w.PowerLevel)
                .Include(w => w.WeaponFrameType)
                .FirstOrDefaultAsync(m => m.WeaponFrameGuid == id);
            if (weaponFrame == null)
            {
                return NotFound();
            }

            return View(weaponFrame);
        }

        // GET: WeaponFrames/Create
        public IActionResult Create()
        {
            ViewData["PowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text");
            ViewData["WeaponFrameTypeGuid"] = new SelectList(_context.WeaponFrameTypes, "WeaponFrameTypeGuid", "Text");
            return View();
        }

        // POST: WeaponFrames/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WeaponFrameGuid,WeaponFrameTypeGuid,PowerLevelGuid,ModulePointsCost,Mass,SpecificHeatCapacity,BaseMagSize,BaseAccuracyModifier,BaseTurnsToArm,BaseCriticalHitChance,Title,Description")] WeaponFrame weaponFrame)
        {
            if (ModelState.IsValid)
            {
                weaponFrame.WeaponFrameGuid = Guid.NewGuid();
                _context.Add(weaponFrame);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text", weaponFrame.PowerLevelGuid);
            ViewData["WeaponFrameTypeGuid"] = new SelectList(_context.WeaponFrameTypes, "WeaponFrameTypeGuid", "Text", weaponFrame.WeaponFrameTypeGuid);
            return View(weaponFrame);
        }

        // GET: WeaponFrames/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.WeaponFrames == null)
            {
                return NotFound();
            }

            var weaponFrame = await _context.WeaponFrames.FindAsync(id);
            if (weaponFrame == null)
            {
                return NotFound();
            }
            ViewData["PowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text", weaponFrame.PowerLevelGuid);
            ViewData["WeaponFrameTypeGuid"] = new SelectList(_context.WeaponFrameTypes, "WeaponFrameTypeGuid", "Text", weaponFrame.WeaponFrameTypeGuid);
            return View(weaponFrame);
        }

        // POST: WeaponFrames/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("WeaponFrameGuid,WeaponFrameTypeGuid,PowerLevelGuid,ModulePointsCost,Mass,SpecificHeatCapacity,BaseMagSize,BaseAccuracyModifier,BaseTurnsToArm,BaseCriticalHitChance,Title,Description")] WeaponFrame weaponFrame)
        {
            if (id != weaponFrame.WeaponFrameGuid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(weaponFrame);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WeaponFrameExists(weaponFrame.WeaponFrameGuid))
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
            ViewData["PowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text", weaponFrame.PowerLevelGuid);
            ViewData["WeaponFrameTypeGuid"] = new SelectList(_context.WeaponFrameTypes, "WeaponFrameTypeGuid", "Text", weaponFrame.WeaponFrameTypeGuid);
            return View(weaponFrame);
        }

        // GET: WeaponFrames/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.WeaponFrames == null)
            {
                return NotFound();
            }

            var weaponFrame = await _context.WeaponFrames
                .Include(w => w.PowerLevel)
                .Include(w => w.WeaponFrameType)
                .FirstOrDefaultAsync(m => m.WeaponFrameGuid == id);
            if (weaponFrame == null)
            {
                return NotFound();
            }

            return View(weaponFrame);
        }

        // POST: WeaponFrames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.WeaponFrames == null)
            {
                return Problem("Entity set 'CelestialFrontierContext.WeaponFrames'  is null.");
            }
            var weaponFrame = await _context.WeaponFrames.FindAsync(id);
            if (weaponFrame != null)
            {
                _context.WeaponFrames.Remove(weaponFrame);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WeaponFrameExists(Guid id)
        {
          return (_context.WeaponFrames?.Any(e => e.WeaponFrameGuid == id)).GetValueOrDefault();
        }
    }
}
