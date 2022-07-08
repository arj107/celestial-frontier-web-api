using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using celestial_frontier_web_api.Models;

namespace celestial_frontier_web_api.WebControllers {
    public class FrigatesController : Controller {
        private readonly CelestialFrontier_DevContext _context;

        public FrigatesController(CelestialFrontier_DevContext context) {
            _context = context;
        }

        // GET: Frigates
        public async Task<IActionResult> Index() {
            var celestialFrontier_DevContext = _context.Frigates.Include(f => f.PointDefenseSystemPowerLevel).Include(f => f.SpaceshipCategory).Include(f => f.SpaceshipType).Include(f => f.WeaponPowerLevel);
            return View(await celestialFrontier_DevContext.ToListAsync());
        }

        // GET: Frigates/Details/5
        public async Task<IActionResult> Details(Guid? id) {
            if (id == null || _context.Frigates == null) {
                return NotFound();
            }

            var frigate = await _context.Frigates
                .Include(f => f.PointDefenseSystemPowerLevel)
                .Include(f => f.SpaceshipCategory)
                .Include(f => f.SpaceshipType)
                .Include(f => f.WeaponPowerLevel)
                .FirstOrDefaultAsync(m => m.FrigateGuid == id);
            if (frigate == null) {
                return NotFound();
            }

            return View(frigate);
        }

        // GET: Frigates/Create
        public IActionResult Create() {
            ViewData["PointDefenseSystemPowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text");
            ViewData["SpaceshipCategoryGuid"] = new SelectList(_context.SpaceshipCategories.Where(c => c.Text == "Subordinate"), "SpaceshipCategoryGuid", "Text");
            ViewData["SpaceshipTypeGuid"] = new SelectList(_context.SpaceshipTypes.Where(c => c.Text == "Frigate"), "SpaceshipTypeGuid", "Text");
            ViewData["WeaponPowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text");
            return View();
        }

        // POST: Frigates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FrigateGuid,SpaceshipTypeGuid,SpaceshipCategoryGuid,WeaponPowerLevelGuid,PointDefenseSystemPowerLevelGuid,Class,HullMass,SpecificHeatCapacity,BaseHullHealth,Description,BaseVigorCost,BaseCrewCost,ModulePointLimit")] Frigate frigate) {
            if (ModelState.IsValid) {
                frigate.FrigateGuid = Guid.NewGuid();
                _context.Add(frigate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PointDefenseSystemPowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text", frigate.PointDefenseSystemPowerLevelGuid);
            ViewData["SpaceshipCategoryGuid"] = new SelectList(_context.SpaceshipCategories.Where(c => c.Text == "Subordinate"), "SpaceshipCategoryGuid", "Text", frigate.SpaceshipCategoryGuid);
            ViewData["SpaceshipTypeGuid"] = new SelectList(_context.SpaceshipTypes.Where(c => c.Text == "Frigate"), "SpaceshipTypeGuid", "Text", frigate.SpaceshipTypeGuid);
            ViewData["WeaponPowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text", frigate.WeaponPowerLevelGuid);
            return View(frigate);
        }

        // GET: Frigates/Edit/5
        public async Task<IActionResult> Edit(Guid? id) {
            if (id == null || _context.Frigates == null) {
                return NotFound();
            }

            var frigate = await _context.Frigates.FindAsync(id);
            if (frigate == null) {
                return NotFound();
            }
            ViewData["PointDefenseSystemPowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text", frigate.PointDefenseSystemPowerLevelGuid);
            ViewData["SpaceshipCategoryGuid"] = new SelectList(_context.SpaceshipCategories.Where(c => c.Text == "Subordinate"), "SpaceshipCategoryGuid", "Text", frigate.SpaceshipCategoryGuid);
            ViewData["SpaceshipTypeGuid"] = new SelectList(_context.SpaceshipTypes.Where(c => c.Text == "Frigate"), "SpaceshipTypeGuid", "Text", frigate.SpaceshipTypeGuid);
            ViewData["WeaponPowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text", frigate.WeaponPowerLevelGuid);
            return View(frigate);
        }

        // POST: Frigates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("FrigateGuid,SpaceshipTypeGuid,SpaceshipCategoryGuid,WeaponPowerLevelGuid,PointDefenseSystemPowerLevelGuid,Class,HullMass,SpecificHeatCapacity,BaseHullHealth,Description,BaseVigorCost,BaseCrewCost,ModulePointLimit")] Frigate frigate) {
            if (id != frigate.FrigateGuid) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(frigate);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!FrigateExists(frigate.FrigateGuid)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PointDefenseSystemPowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text", frigate.PointDefenseSystemPowerLevelGuid);
            ViewData["SpaceshipCategoryGuid"] = new SelectList(_context.SpaceshipCategories.Where(c => c.Text == "Subordinate"), "SpaceshipCategoryGuid", "Text", frigate.SpaceshipCategoryGuid);
            ViewData["SpaceshipTypeGuid"] = new SelectList(_context.SpaceshipTypes.Where(c => c.Text == "Frigate"), "SpaceshipTypeGuid", "Text", frigate.SpaceshipTypeGuid);
            ViewData["WeaponPowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text", frigate.WeaponPowerLevelGuid);
            return View(frigate);
        }

        // GET: Frigates/Delete/5
        public async Task<IActionResult> Delete(Guid? id) {
            if (id == null || _context.Frigates == null) {
                return NotFound();
            }

            var frigate = await _context.Frigates
                .Include(f => f.PointDefenseSystemPowerLevel)
                .Include(f => f.SpaceshipCategory)
                .Include(f => f.SpaceshipType)
                .Include(f => f.WeaponPowerLevel)
                .FirstOrDefaultAsync(m => m.FrigateGuid == id);
            if (frigate == null) {
                return NotFound();
            }

            return View(frigate);
        }

        // POST: Frigates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id) {
            if (_context.Frigates == null) {
                return Problem("Entity set 'CelestialFrontier_DevContext.Frigates'  is null.");
            }
            var frigate = await _context.Frigates.FindAsync(id);
            if (frigate != null) {
                _context.Frigates.Remove(frigate);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FrigateExists(Guid id) {
            return _context.Frigates.Any(e => e.FrigateGuid == id);
        }
    }
}
