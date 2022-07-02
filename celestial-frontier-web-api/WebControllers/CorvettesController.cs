using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using celestial_frontier_web_api.Models;

namespace celestial_frontier_web_api.WebControllers {
    public class CorvettesController : Controller {
        private readonly CelestialFrontier_DevContext _context;

        public CorvettesController(CelestialFrontier_DevContext context) {
            _context = context;
        }

        // GET: Corvettes
        public async Task<IActionResult> Index() {
            var celestialFrontier_DevContext = _context.Corvettes.Include(c => c.PointDefenseSystemPowerLevel).Include(c => c.SpaceshipCategory).Include(c => c.SpaceshipType).Include(c => c.WeaponPowerLevel);
            return View(await celestialFrontier_DevContext.ToListAsync());
        }

        // GET: Corvettes/Details/5
        public async Task<IActionResult> Details(Guid? id) {
            if (id == null || _context.Corvettes == null) {
                return NotFound();
            }

            var corvette = await _context.Corvettes
                .Include(c => c.PointDefenseSystemPowerLevel)
                .Include(c => c.SpaceshipCategory)
                .Include(c => c.SpaceshipType)
                .Include(c => c.WeaponPowerLevel)
                .FirstOrDefaultAsync(m => m.CorvetteGuid == id);
            if (corvette == null) {
                return NotFound();
            }

            return View(corvette);
        }

        // GET: Corvettes/Create
        public IActionResult Create() {
            ViewData["PointDefenseSystemPowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text");
            ViewData["SpaceshipCategoryGuid"] = new SelectList(_context.SpaceshipCategories.Where(c => c.Text == "Subordinate"), "SpaceshipCategoryGuid", "Text");
            ViewData["SpaceshipTypeGuid"] = new SelectList(_context.SpaceshipTypes.Where(c => c.Text == "Corvette"), "SpaceshipTypeGuid", "Text");
            ViewData["WeaponPowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text");
            return View();
        }

        // POST: Corvettes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CorvetteGuid,SpaceshipTypeGuid,SpaceshipCategoryGuid,WeaponPowerLevelGuid,PointDefenseSystemPowerLevelGuid,Class,HullMass,SpecificHeatCapacity,BaseHullHealth,Description,BaseVigorCost,BaseCrewCost,ModulePointLimit")] Corvette corvette) {
            if (ModelState.IsValid) {
                corvette.CorvetteGuid = Guid.NewGuid();
                _context.Add(corvette);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PointDefenseSystemPowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text", corvette.PointDefenseSystemPowerLevelGuid);
            ViewData["SpaceshipCategoryGuid"] = new SelectList(_context.SpaceshipCategories.Where(c => c.Text == "Subordinate"), "SpaceshipCategoryGuid", "Text", corvette.SpaceshipCategoryGuid);
            ViewData["SpaceshipTypeGuid"] = new SelectList(_context.SpaceshipTypes.Where(c => c.Text == "Corvette"), "SpaceshipTypeGuid", "Text", corvette.SpaceshipTypeGuid);
            ViewData["WeaponPowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text", corvette.WeaponPowerLevelGuid);
            return View(corvette);
        }

        // GET: Corvettes/Edit/5
        public async Task<IActionResult> Edit(Guid? id) {
            if (id == null || _context.Corvettes == null) {
                return NotFound();
            }

            var corvette = await _context.Corvettes.FindAsync(id);
            if (corvette == null) {
                return NotFound();
            }
            ViewData["PointDefenseSystemPowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text", corvette.PointDefenseSystemPowerLevelGuid);
            ViewData["SpaceshipCategoryGuid"] = new SelectList(_context.SpaceshipCategories.Where(c => c.Text == "Subordinate"), "SpaceshipCategoryGuid", "Text", corvette.SpaceshipCategoryGuid);
            ViewData["SpaceshipTypeGuid"] = new SelectList(_context.SpaceshipTypes.Where(c => c.Text == "Corvette"), "SpaceshipTypeGuid", "Text", corvette.SpaceshipTypeGuid);
            ViewData["WeaponPowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text", corvette.WeaponPowerLevelGuid);
            return View(corvette);
        }

        // POST: Corvettes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CorvetteGuid,SpaceshipTypeGuid,SpaceshipCategoryGuid,WeaponPowerLevelGuid,PointDefenseSystemPowerLevelGuid,Class,HullMass,SpecificHeatCapacity,BaseHullHealth,Description,BaseVigorCost,BaseCrewCost,ModulePointLimit")] Corvette corvette) {
            if (id != corvette.CorvetteGuid) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(corvette);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!CorvetteExists(corvette.CorvetteGuid)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PointDefenseSystemPowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text", corvette.PointDefenseSystemPowerLevelGuid);
            ViewData["SpaceshipCategoryGuid"] = new SelectList(_context.SpaceshipCategories.Where(c => c.Text == "Subordinate"), "SpaceshipCategoryGuid", "Text", corvette.SpaceshipCategoryGuid);
            ViewData["SpaceshipTypeGuid"] = new SelectList(_context.SpaceshipTypes.Where(c => c.Text == "Corvette"), "SpaceshipTypeGuid", "Text", corvette.SpaceshipTypeGuid);
            ViewData["WeaponPowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text", corvette.WeaponPowerLevelGuid);
            return View(corvette);
        }

        // GET: Corvettes/Delete/5
        public async Task<IActionResult> Delete(Guid? id) {
            if (id == null || _context.Corvettes == null) {
                return NotFound();
            }

            var corvette = await _context.Corvettes
                .Include(c => c.PointDefenseSystemPowerLevel)
                .Include(c => c.SpaceshipCategory)
                .Include(c => c.SpaceshipType)
                .Include(c => c.WeaponPowerLevel)
                .FirstOrDefaultAsync(m => m.CorvetteGuid == id);
            if (corvette == null) {
                return NotFound();
            }

            return View(corvette);
        }

        // POST: Corvettes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id) {
            if (_context.Corvettes == null) {
                return Problem("Entity set 'CelestialFrontier_DevContext.Corvettes'  is null.");
            }
            var corvette = await _context.Corvettes.FindAsync(id);
            if (corvette != null) {
                _context.Corvettes.Remove(corvette);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CorvetteExists(Guid id) {
            return _context.Corvettes.Any(e => e.CorvetteGuid == id);
        }
    }
}
