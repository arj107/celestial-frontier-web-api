using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using celestial_frontier_web_api.Models;

namespace celestial_frontier_web_api.WebControllers {
    public class DestroyersController : Controller {
        private readonly CelestialFrontier_DevContext _context;

        public DestroyersController(CelestialFrontier_DevContext context) {
            _context = context;
        }

        // GET: Destroyers
        public async Task<IActionResult> Index() {
            var celestialFrontier_DevContext = _context.Destroyers.Include(d => d.PointDefenseSystemPowerLevel).Include(d => d.SpaceshipCategory).Include(d => d.SpaceshipType).Include(d => d.WeaponPowerLevel);
            return View(await celestialFrontier_DevContext.ToListAsync());
        }

        // GET: Destroyers/Details/5
        public async Task<IActionResult> Details(Guid? id) {
            if (id == null || _context.Destroyers == null) {
                return NotFound();
            }

            var destroyer = await _context.Destroyers
                .Include(d => d.PointDefenseSystemPowerLevel)
                .Include(d => d.SpaceshipCategory)
                .Include(d => d.SpaceshipType)
                .Include(d => d.WeaponPowerLevel)
                .FirstOrDefaultAsync(m => m.DestroyerGuid == id);
            if (destroyer == null) {
                return NotFound();
            }

            return View(destroyer);
        }

        // GET: Destroyers/Create
        public IActionResult Create() {
            ViewData["PointDefenseSystemPowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text");
            ViewData["SpaceshipCategoryGuid"] = new SelectList(_context.SpaceshipCategories.Where(c => c.Text == "Subordinate"), "SpaceshipCategoryGuid", "Text");
            ViewData["SpaceshipTypeGuid"] = new SelectList(_context.SpaceshipTypes.Where(c => c.Text == "Destroyer"), "SpaceshipTypeGuid", "Text");
            ViewData["WeaponPowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text");
            return View();
        }

        // POST: Destroyers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DestroyerGuid,SpaceshipTypeGuid,SpaceshipCategoryGuid,WeaponPowerLevelGuid,PointDefenseSystemPowerLevelGuid,Class,HullMass,SpecificHeatCapacity,BaseHullHealth,Description,BaseVigorCost,BaseCrewCost,ModulePointLimit")] Destroyer destroyer) {
            if (ModelState.IsValid) {
                destroyer.DestroyerGuid = Guid.NewGuid();
                _context.Add(destroyer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PointDefenseSystemPowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text", destroyer.PointDefenseSystemPowerLevelGuid);
            ViewData["SpaceshipCategoryGuid"] = new SelectList(_context.SpaceshipCategories.Where(c => c.Text == "Subordinate"), "SpaceshipCategoryGuid", "Text", destroyer.SpaceshipCategoryGuid);
            ViewData["SpaceshipTypeGuid"] = new SelectList(_context.SpaceshipTypes.Where(c => c.Text == "Destroyer"), "SpaceshipTypeGuid", "Text", destroyer.SpaceshipTypeGuid);
            ViewData["WeaponPowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text", destroyer.WeaponPowerLevelGuid);
            return View(destroyer);
        }

        // GET: Destroyers/Edit/5
        public async Task<IActionResult> Edit(Guid? id) {
            if (id == null || _context.Destroyers == null) {
                return NotFound();
            }

            var destroyer = await _context.Destroyers.FindAsync(id);
            if (destroyer == null) {
                return NotFound();
            }
            ViewData["PointDefenseSystemPowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text", destroyer.PointDefenseSystemPowerLevelGuid);
            ViewData["SpaceshipCategoryGuid"] = new SelectList(_context.SpaceshipCategories.Where(c => c.Text == "Subordinate"), "SpaceshipCategoryGuid", "Text", destroyer.SpaceshipCategoryGuid);
            ViewData["SpaceshipTypeGuid"] = new SelectList(_context.SpaceshipTypes.Where(c => c.Text == "Destroyer"), "SpaceshipTypeGuid", "Text", destroyer.SpaceshipTypeGuid);
            ViewData["WeaponPowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text", destroyer.WeaponPowerLevelGuid);
            return View(destroyer);
        }

        // POST: Destroyers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("DestroyerGuid,SpaceshipTypeGuid,SpaceshipCategoryGuid,WeaponPowerLevelGuid,PointDefenseSystemPowerLevelGuid,Class,HullMass,SpecificHeatCapacity,BaseHullHealth,Description,BaseVigorCost,BaseCrewCost,ModulePointLimit")] Destroyer destroyer) {
            if (id != destroyer.DestroyerGuid) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(destroyer);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!DestroyerExists(destroyer.DestroyerGuid)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PointDefenseSystemPowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text", destroyer.PointDefenseSystemPowerLevelGuid);
            ViewData["SpaceshipCategoryGuid"] = new SelectList(_context.SpaceshipCategories.Where(c => c.Text == "Subordinate"), "SpaceshipCategoryGuid", "Text", destroyer.SpaceshipCategoryGuid);
            ViewData["SpaceshipTypeGuid"] = new SelectList(_context.SpaceshipTypes.Where(c => c.Text == "Destroyer"), "SpaceshipTypeGuid", "Text", destroyer.SpaceshipTypeGuid);
            ViewData["WeaponPowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text", destroyer.WeaponPowerLevelGuid);
            return View(destroyer);
        }

        // GET: Destroyers/Delete/5
        public async Task<IActionResult> Delete(Guid? id) {
            if (id == null || _context.Destroyers == null) {
                return NotFound();
            }

            var destroyer = await _context.Destroyers
                .Include(d => d.PointDefenseSystemPowerLevel)
                .Include(d => d.SpaceshipCategory)
                .Include(d => d.SpaceshipType)
                .Include(d => d.WeaponPowerLevel)
                .FirstOrDefaultAsync(m => m.DestroyerGuid == id);
            if (destroyer == null) {
                return NotFound();
            }

            return View(destroyer);
        }

        // POST: Destroyers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id) {
            if (_context.Destroyers == null) {
                return Problem("Entity set 'CelestialFrontier_DevContext.Destroyers'  is null.");
            }
            var destroyer = await _context.Destroyers.FindAsync(id);
            if (destroyer != null) {
                _context.Destroyers.Remove(destroyer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DestroyerExists(Guid id) {
            return _context.Destroyers.Any(e => e.DestroyerGuid == id);
        }
    }
}
