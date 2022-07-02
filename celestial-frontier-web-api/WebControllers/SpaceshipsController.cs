using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using celestial_frontier_web_api.Models;

namespace celestial_frontier_web_api.WebControllers {
    public class SpaceshipsController : Controller {
        private readonly CelestialFrontier_DevContext _context;

        public SpaceshipsController(CelestialFrontier_DevContext context) {
            _context = context;
        }

        // GET: Spaceships
        public async Task<IActionResult> Index() {
            var celestialFrontierContext = _context.Spaceships.Include(s => s.PointDefenseSystemPowerLevel).Include(s => s.SpaceshipCategory).Include(s => s.SpaceshipType).Include(s => s.WeaponPowerLevel);
            return View(await celestialFrontierContext.ToListAsync());
        }

        // GET: Spaceships/Details/5
        public async Task<IActionResult> Details(Guid? id) {
            if (id == null || _context.Spaceships == null) {
                return NotFound();
            }

            var spaceship = await _context.Spaceships
                .Include(s => s.PointDefenseSystemPowerLevel)
                .Include(s => s.SpaceshipCategory)
                .Include(s => s.SpaceshipType)
                .Include(s => s.WeaponPowerLevel)
                .FirstOrDefaultAsync(m => m.SpaceshipGuid == id);
            if (spaceship == null) {
                return NotFound();
            }

            return View(spaceship);
        }

        // GET: Spaceships/Create
        public IActionResult Create() {
            ViewData["PointDefenseSystemPowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text");
            ViewData["SpaceshipCategoryGuid"] = new SelectList(_context.SpaceshipCategories, "SpaceshipCategoryGuid", "Text");
            ViewData["SpaceshipTypeGuid"] = new SelectList(_context.SpaceshipTypes, "SpaceshipTypeGuid", "Text");
            ViewData["WeaponPowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text");
            return View();
        }

        // POST: Spaceships/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SpaceshipGuid,SpaceshipTypeGuid,SpaceshipCategoryGuid,WeaponPowerLevelGuid,PointDefenseSystemPowerLevelGuid,Class,HullMass,SpecificHeatCapacity,BaseHullHealth,Description")] Spaceship spaceship) {
            if (ModelState.IsValid) {
                spaceship.SpaceshipGuid = Guid.NewGuid();
                _context.Add(spaceship);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PointDefenseSystemPowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text", spaceship.PointDefenseSystemPowerLevelGuid);
            ViewData["SpaceshipCategoryGuid"] = new SelectList(_context.SpaceshipCategories, "SpaceshipCategoryGuid", "Text", spaceship.SpaceshipCategoryGuid);
            ViewData["SpaceshipTypeGuid"] = new SelectList(_context.SpaceshipTypes, "SpaceshipTypeGuid", "Text", spaceship.SpaceshipTypeGuid);
            ViewData["WeaponPowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text", spaceship.WeaponPowerLevelGuid);
            return View(spaceship);
        }

        // GET: Spaceships/Edit/5
        public async Task<IActionResult> Edit(Guid? id) {
            if (id == null || _context.Spaceships == null) {
                return NotFound();
            }

            var spaceship = await _context.Spaceships.FindAsync(id);
            if (spaceship == null) {
                return NotFound();
            }
            ViewData["PointDefenseSystemPowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text", spaceship.PointDefenseSystemPowerLevelGuid);
            ViewData["SpaceshipCategoryGuid"] = new SelectList(_context.SpaceshipCategories, "SpaceshipCategoryGuid", "Text", spaceship.SpaceshipCategoryGuid);
            ViewData["SpaceshipTypeGuid"] = new SelectList(_context.SpaceshipTypes, "SpaceshipTypeGuid", "Text", spaceship.SpaceshipTypeGuid);
            ViewData["WeaponPowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text", spaceship.WeaponPowerLevelGuid);
            return View(spaceship);
        }

        // POST: Spaceships/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("SpaceshipGuid,SpaceshipTypeGuid,SpaceshipCategoryGuid,WeaponPowerLevelGuid,PointDefenseSystemPowerLevelGuid,Class,HullMass,SpecificHeatCapacity,BaseHullHealth,Description")] Spaceship spaceship) {
            if (id != spaceship.SpaceshipGuid) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(spaceship);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!SpaceshipExists(spaceship.SpaceshipGuid)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PointDefenseSystemPowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text", spaceship.PointDefenseSystemPowerLevelGuid);
            ViewData["SpaceshipCategoryGuid"] = new SelectList(_context.SpaceshipCategories, "SpaceshipCategoryGuid", "Text", spaceship.SpaceshipCategoryGuid);
            ViewData["SpaceshipTypeGuid"] = new SelectList(_context.SpaceshipTypes, "SpaceshipTypeGuid", "Text", spaceship.SpaceshipTypeGuid);
            ViewData["WeaponPowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text", spaceship.WeaponPowerLevelGuid);
            return View(spaceship);
        }

        // GET: Spaceships/Delete/5
        public async Task<IActionResult> Delete(Guid? id) {
            if (id == null || _context.Spaceships == null) {
                return NotFound();
            }

            var spaceship = await _context.Spaceships
                .Include(s => s.PointDefenseSystemPowerLevel)
                .Include(s => s.SpaceshipCategory)
                .Include(s => s.SpaceshipType)
                .Include(s => s.WeaponPowerLevel)
                .FirstOrDefaultAsync(m => m.SpaceshipGuid == id);
            if (spaceship == null) {
                return NotFound();
            }

            return View(spaceship);
        }

        // POST: Spaceships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id) {
            if (_context.Spaceships == null) {
                return Problem("Entity set 'CelestialFrontier_DevContext.Spaceships'  is null.");
            }
            var spaceship = await _context.Spaceships.FindAsync(id);
            if (spaceship != null) {
                _context.Spaceships.Remove(spaceship);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpaceshipExists(Guid id) {
            return (_context.Spaceships?.Any(e => e.SpaceshipGuid == id)).GetValueOrDefault();
        }
    }
}
