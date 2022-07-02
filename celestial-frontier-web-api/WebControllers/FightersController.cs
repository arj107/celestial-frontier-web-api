using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using celestial_frontier_web_api.Models;

namespace celestial_frontier_web_api.WebControllers {
    public class FightersController : Controller {
        private readonly CelestialFrontier_DevContext _context;

        public FightersController(CelestialFrontier_DevContext context) {
            _context = context;
        }

        // GET: Fighters
        public async Task<IActionResult> Index() {
            var celestialFrontier_DevContext = _context.Fighters.Include(f => f.PointDefenseSystemPowerLevel).Include(f => f.SpaceshipCategory).Include(f => f.SpaceshipType).Include(f => f.WeaponPowerLevel);
            return View(await celestialFrontier_DevContext.ToListAsync());
        }

        // GET: Fighters/Details/5
        public async Task<IActionResult> Details(Guid? id) {
            if (id == null || _context.Fighters == null) {
                return NotFound();
            }

            var fighter = await _context.Fighters
                .Include(f => f.PointDefenseSystemPowerLevel)
                .Include(f => f.SpaceshipCategory)
                .Include(f => f.SpaceshipType)
                .Include(f => f.WeaponPowerLevel)
                .FirstOrDefaultAsync(m => m.FighterGuid == id);
            if (fighter == null) {
                return NotFound();
            }

            return View(fighter);
        }

        // GET: Fighters/Create
        public IActionResult Create() {
            ViewData["PointDefenseSystemPowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text");
            ViewData["SpaceshipCategoryGuid"] = new SelectList(_context.SpaceshipCategories.Where(c => c.Text == "Subordinate"), "SpaceshipCategoryGuid", "Text");
            ViewData["SpaceshipTypeGuid"] = new SelectList(_context.SpaceshipTypes.Where(c => c.Text == "Fighter"), "SpaceshipTypeGuid", "Text");
            ViewData["WeaponPowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text");
            return View();
        }

        // POST: Fighters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FighterGuid,SpaceshipTypeGuid,SpaceshipCategoryGuid,WeaponPowerLevelGuid,PointDefenseSystemPowerLevelGuid,Class,HullMass,SpecificHeatCapacity,BaseHullHealth,Description,BaseVigorCost,BaseCrewCost,ModulePointLimit")] Fighter fighter) {
            if (ModelState.IsValid) {
                fighter.FighterGuid = Guid.NewGuid();
                _context.Add(fighter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PointDefenseSystemPowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text", fighter.PointDefenseSystemPowerLevelGuid);
            ViewData["SpaceshipCategoryGuid"] = new SelectList(_context.SpaceshipCategories.Where(c => c.Text == "Subordinate"), "SpaceshipCategoryGuid", "Text", fighter.SpaceshipCategoryGuid);
            ViewData["SpaceshipTypeGuid"] = new SelectList(_context.SpaceshipTypes.Where(c => c.Text == "Fighter"), "SpaceshipTypeGuid", "Text", fighter.SpaceshipTypeGuid);
            ViewData["WeaponPowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text", fighter.WeaponPowerLevelGuid);
            return View(fighter);
        }

        // GET: Fighters/Edit/5
        public async Task<IActionResult> Edit(Guid? id) {
            if (id == null || _context.Fighters == null) {
                return NotFound();
            }

            var fighter = await _context.Fighters.FindAsync(id);
            if (fighter == null) {
                return NotFound();
            }
            ViewData["PointDefenseSystemPowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text", fighter.PointDefenseSystemPowerLevelGuid);
            ViewData["SpaceshipCategoryGuid"] = new SelectList(_context.SpaceshipCategories.Where(c => c.Text == "Subordinate"), "SpaceshipCategoryGuid", "Text", fighter.SpaceshipCategoryGuid);
            ViewData["SpaceshipTypeGuid"] = new SelectList(_context.SpaceshipTypes.Where(c => c.Text == "Fighter"), "SpaceshipTypeGuid", "Text", fighter.SpaceshipTypeGuid);
            ViewData["WeaponPowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text", fighter.WeaponPowerLevelGuid);
            return View(fighter);
        }

        // POST: Fighters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("FighterGuid,SpaceshipTypeGuid,SpaceshipCategoryGuid,WeaponPowerLevelGuid,PointDefenseSystemPowerLevelGuid,Class,HullMass,SpecificHeatCapacity,BaseHullHealth,Description,BaseVigorCost,BaseCrewCost,ModulePointLimit")] Fighter fighter) {
            if (id != fighter.FighterGuid) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(fighter);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!FighterExists(fighter.FighterGuid)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PointDefenseSystemPowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text", fighter.PointDefenseSystemPowerLevelGuid);
            ViewData["SpaceshipCategoryGuid"] = new SelectList(_context.SpaceshipCategories.Where(c => c.Text == "Subordinate"), "SpaceshipCategoryGuid", "Text", fighter.SpaceshipCategoryGuid);
            ViewData["SpaceshipTypeGuid"] = new SelectList(_context.SpaceshipTypes.Where(c => c.Text == "Fighter"), "SpaceshipTypeGuid", "Text", fighter.SpaceshipTypeGuid);
            ViewData["WeaponPowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text", fighter.WeaponPowerLevelGuid);
            return View(fighter);
        }

        // GET: Fighters/Delete/5
        public async Task<IActionResult> Delete(Guid? id) {
            if (id == null || _context.Fighters == null) {
                return NotFound();
            }

            var fighter = await _context.Fighters
                .Include(f => f.PointDefenseSystemPowerLevel)
                .Include(f => f.SpaceshipCategory)
                .Include(f => f.SpaceshipType)
                .Include(f => f.WeaponPowerLevel)
                .FirstOrDefaultAsync(m => m.FighterGuid == id);
            if (fighter == null) {
                return NotFound();
            }

            return View(fighter);
        }

        // POST: Fighters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id) {
            if (_context.Fighters == null) {
                return Problem("Entity set 'CelestialFrontier_DevContext.Fighters'  is null.");
            }
            var fighter = await _context.Fighters.FindAsync(id);
            if (fighter != null) {
                _context.Fighters.Remove(fighter);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FighterExists(Guid id) {
            return _context.Fighters.Any(e => e.FighterGuid == id);
        }
    }
}
