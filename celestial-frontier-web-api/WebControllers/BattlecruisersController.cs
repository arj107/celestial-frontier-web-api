using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using celestial_frontier_web_api.Models;

namespace celestial_frontier_web_api.WebControllers {
    public class BattlecruisersController : Controller {
        private readonly CelestialFrontier_DevContext _context;

        public BattlecruisersController(CelestialFrontier_DevContext context) {
            _context = context;
        }

        // GET: Battlecruisers
        public async Task<IActionResult> Index() {
            var celestialFrontier_DevContext = _context.Battlecruisers.Include(b => b.PointDefenseSystemPowerLevel).Include(b => b.SpaceshipCategory).Include(b => b.SpaceshipType).Include(b => b.WeaponPowerLevel);
            return View(await celestialFrontier_DevContext.ToListAsync());
        }

        // GET: Battlecruisers/Details/5
        public async Task<IActionResult> Details(Guid? id) {
            if (id == null || _context.Battlecruisers == null) {
                return NotFound();
            }

            var battlecruiser = await _context.Battlecruisers
                .Include(b => b.PointDefenseSystemPowerLevel)
                .Include(b => b.SpaceshipCategory)
                .Include(b => b.SpaceshipType)
                .Include(b => b.WeaponPowerLevel)
                .FirstOrDefaultAsync(m => m.BattlecruiserGuid == id);
            if (battlecruiser == null) {
                return NotFound();
            }

            return View(battlecruiser);
        }

        // GET: Battlecruisers/Create
        public IActionResult Create() {
            ViewData["PointDefenseSystemPowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text");
            ViewData["SpaceshipCategoryGuid"] = new SelectList(_context.SpaceshipCategories.Where(c => c.Text == "Principal"), "SpaceshipCategoryGuid", "Text");
            ViewData["SpaceshipTypeGuid"] = new SelectList(_context.SpaceshipTypes.Where(c => c.Text == "Battlecruiser"), "SpaceshipTypeGuid", "Text");
            ViewData["WeaponPowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text");
            return View();
        }

        // POST: Battlecruisers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BattlecruiserGuid,SpaceshipTypeGuid,SpaceshipCategoryGuid,WeaponPowerLevelGuid,PointDefenseSystemPowerLevelGuid,Class,HullMass,SpecificHeatCapacity,BaseHullHealth,Description,BaseVigorCost,BaseCrewCost,ModulePointLimit")] Battlecruiser battlecruiser) {
            if (ModelState.IsValid) {
                battlecruiser.BattlecruiserGuid = Guid.NewGuid();
                _context.Add(battlecruiser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PointDefenseSystemPowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text", battlecruiser.PointDefenseSystemPowerLevelGuid);
            ViewData["SpaceshipCategoryGuid"] = new SelectList(_context.SpaceshipCategories.Where(c => c.Text == "Principal"), "SpaceshipCategoryGuid", "Text", battlecruiser.SpaceshipCategoryGuid);
            ViewData["SpaceshipTypeGuid"] = new SelectList(_context.SpaceshipTypes.Where(c => c.Text == "Battlecruiser"), "SpaceshipTypeGuid", "Text", battlecruiser.SpaceshipTypeGuid);
            ViewData["WeaponPowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text", battlecruiser.WeaponPowerLevelGuid);
            return View(battlecruiser);
        }

        // GET: Battlecruisers/Edit/5
        public async Task<IActionResult> Edit(Guid? id) {
            if (id == null || _context.Battlecruisers == null) {
                return NotFound();
            }

            var battlecruiser = await _context.Battlecruisers.FindAsync(id);
            if (battlecruiser == null) {
                return NotFound();
            }
            ViewData["PointDefenseSystemPowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text", battlecruiser.PointDefenseSystemPowerLevelGuid);
            ViewData["SpaceshipCategoryGuid"] = new SelectList(_context.SpaceshipCategories.Where(c => c.Text == "Principal"), "SpaceshipCategoryGuid", "Text", battlecruiser.SpaceshipCategoryGuid);
            ViewData["SpaceshipTypeGuid"] = new SelectList(_context.SpaceshipTypes.Where(c => c.Text == "Battlecruiser"), "SpaceshipTypeGuid", "Text", battlecruiser.SpaceshipTypeGuid);
            ViewData["WeaponPowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text", battlecruiser.WeaponPowerLevelGuid);
            return View(battlecruiser);
        }

        // POST: Battlecruisers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("BattlecruiserGuid,SpaceshipTypeGuid,SpaceshipCategoryGuid,WeaponPowerLevelGuid,PointDefenseSystemPowerLevelGuid,Class,HullMass,SpecificHeatCapacity,BaseHullHealth,Description,BaseVigorCost,BaseCrewCost,ModulePointLimit")] Battlecruiser battlecruiser) {
            if (id != battlecruiser.BattlecruiserGuid) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(battlecruiser);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!BattlecruiserExists(battlecruiser.BattlecruiserGuid)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PointDefenseSystemPowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text", battlecruiser.PointDefenseSystemPowerLevelGuid);
            ViewData["SpaceshipCategoryGuid"] = new SelectList(_context.SpaceshipCategories.Where(c => c.Text == "Principal"), "SpaceshipCategoryGuid", "Text", battlecruiser.SpaceshipCategoryGuid);
            ViewData["SpaceshipTypeGuid"] = new SelectList(_context.SpaceshipTypes.Where(c => c.Text == "Battlecruiser"), "SpaceshipTypeGuid", "Text", battlecruiser.SpaceshipTypeGuid);
            ViewData["WeaponPowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text", battlecruiser.WeaponPowerLevelGuid);
            return View(battlecruiser);
        }

        // GET: Battlecruisers/Delete/5
        public async Task<IActionResult> Delete(Guid? id) {
            if (id == null || _context.Battlecruisers == null) {
                return NotFound();
            }

            var battlecruiser = await _context.Battlecruisers
                .Include(b => b.PointDefenseSystemPowerLevel)
                .Include(b => b.SpaceshipCategory)
                .Include(b => b.SpaceshipType)
                .Include(b => b.WeaponPowerLevel)
                .FirstOrDefaultAsync(m => m.BattlecruiserGuid == id);
            if (battlecruiser == null) {
                return NotFound();
            }

            return View(battlecruiser);
        }

        // POST: Battlecruisers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id) {
            if (_context.Battlecruisers == null) {
                return Problem("Entity set 'CelestialFrontier_DevContext.Battlecruisers'  is null.");
            }
            var battlecruiser = await _context.Battlecruisers.FindAsync(id);
            if (battlecruiser != null) {
                _context.Battlecruisers.Remove(battlecruiser);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BattlecruiserExists(Guid id) {
            return _context.Battlecruisers.Any(e => e.BattlecruiserGuid == id);
        }
    }
}
