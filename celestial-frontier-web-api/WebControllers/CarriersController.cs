using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using celestial_frontier_web_api.Models;

namespace celestial_frontier_web_api.WebControllers {
    public class CarriersController : Controller {
        private readonly CelestialFrontier_DevContext _context;

        public CarriersController(CelestialFrontier_DevContext context) {
            _context = context;
        }

        // GET: Carriers
        public async Task<IActionResult> Index() {
            var celestialFrontier_DevContext = _context.Carriers.Include(c => c.PointDefenseSystemPowerLevel).Include(c => c.SpaceshipCategory).Include(c => c.SpaceshipType).Include(c => c.WeaponPowerLevel);
            return View(await celestialFrontier_DevContext.ToListAsync());
        }

        // GET: Carriers/Details/5
        public async Task<IActionResult> Details(Guid? id) {
            if (id == null || _context.Carriers == null) {
                return NotFound();
            }

            var carrier = await _context.Carriers
                .Include(c => c.PointDefenseSystemPowerLevel)
                .Include(c => c.SpaceshipCategory)
                .Include(c => c.SpaceshipType)
                .Include(c => c.WeaponPowerLevel)
                .FirstOrDefaultAsync(m => m.CarrierGuid == id);
            if (carrier == null) {
                return NotFound();
            }

            return View(carrier);
        }

        // GET: Carriers/Create
        public IActionResult Create() {
            ViewData["PointDefenseSystemPowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text");
            ViewData["SpaceshipCategoryGuid"] = new SelectList(_context.SpaceshipCategories.Where(c => c.Text == "Principal"), "SpaceshipCategoryGuid", "Text");
            ViewData["SpaceshipTypeGuid"] = new SelectList(_context.SpaceshipTypes.Where(c => c.Text == "Carrier"), "SpaceshipTypeGuid", "Text");
            ViewData["WeaponPowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text");
            return View();
        }

        // POST: Carriers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarrierGuid,SpaceshipTypeGuid,SpaceshipCategoryGuid,WeaponPowerLevelGuid,PointDefenseSystemPowerLevelGuid,Class,HullMass,SpecificHeatCapacity,BaseHullHealth,Description,BaseVigorCost,CrewCapacity,ModulePointLimit,FighterBays,CorvetteBays,FrigateBays,DestroyerBays")] Carrier carrier) {
            if (ModelState.IsValid) {
                carrier.CarrierGuid = Guid.NewGuid();
                _context.Add(carrier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PointDefenseSystemPowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text", carrier.PointDefenseSystemPowerLevelGuid);
            ViewData["SpaceshipCategoryGuid"] = new SelectList(_context.SpaceshipCategories.Where(c => c.Text == "Principal"), "SpaceshipCategoryGuid", "Text", carrier.SpaceshipCategoryGuid);
            ViewData["SpaceshipTypeGuid"] = new SelectList(_context.SpaceshipTypes.Where(c => c.Text == "Carrier"), "SpaceshipTypeGuid", "Text", carrier.SpaceshipTypeGuid);
            ViewData["WeaponPowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text", carrier.WeaponPowerLevelGuid);
            return View(carrier);
        }

        // GET: Carriers/Edit/5
        public async Task<IActionResult> Edit(Guid? id) {
            if (id == null || _context.Carriers == null) {
                return NotFound();
            }

            var carrier = await _context.Carriers.FindAsync(id);
            if (carrier == null) {
                return NotFound();
            }
            ViewData["PointDefenseSystemPowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text", carrier.PointDefenseSystemPowerLevelGuid);
            ViewData["SpaceshipCategoryGuid"] = new SelectList(_context.SpaceshipCategories.Where(c => c.Text == "Principal"), "SpaceshipCategoryGuid", "Text", carrier.SpaceshipCategoryGuid);
            ViewData["SpaceshipTypeGuid"] = new SelectList(_context.SpaceshipTypes.Where(c => c.Text == "Carrier"), "SpaceshipTypeGuid", "Text", carrier.SpaceshipTypeGuid);
            ViewData["WeaponPowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text", carrier.WeaponPowerLevelGuid);
            return View(carrier);
        }

        // POST: Carriers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CarrierGuid,SpaceshipTypeGuid,SpaceshipCategoryGuid,WeaponPowerLevelGuid,PointDefenseSystemPowerLevelGuid,Class,HullMass,SpecificHeatCapacity,BaseHullHealth,Description,BaseVigorCost,CrewCapacity,ModulePointLimit,FighterBays,CorvetteBays,FrigateBays,DestroyerBays")] Carrier carrier) {
            if (id != carrier.CarrierGuid) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(carrier);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!CarrierExists(carrier.CarrierGuid)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PointDefenseSystemPowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text", carrier.PointDefenseSystemPowerLevelGuid);
            ViewData["SpaceshipCategoryGuid"] = new SelectList(_context.SpaceshipCategories.Where(c => c.Text == "Principal"), "SpaceshipCategoryGuid", "Text", carrier.SpaceshipCategoryGuid);
            ViewData["SpaceshipTypeGuid"] = new SelectList(_context.SpaceshipTypes.Where(c => c.Text == "Carrier"), "SpaceshipTypeGuid", "Text", carrier.SpaceshipTypeGuid);
            ViewData["WeaponPowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text", carrier.WeaponPowerLevelGuid);
            return View(carrier);
        }

        // GET: Carriers/Delete/5
        public async Task<IActionResult> Delete(Guid? id) {
            if (id == null || _context.Carriers == null) {
                return NotFound();
            }

            var carrier = await _context.Carriers
                .Include(c => c.PointDefenseSystemPowerLevel)
                .Include(c => c.SpaceshipCategory)
                .Include(c => c.SpaceshipType)
                .Include(c => c.WeaponPowerLevel)
                .FirstOrDefaultAsync(m => m.CarrierGuid == id);
            if (carrier == null) {
                return NotFound();
            }

            return View(carrier);
        }

        // POST: Carriers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id) {
            if (_context.Carriers == null) {
                return Problem("Entity set 'CelestialFrontier_DevContext.Carriers'  is null.");
            }
            var carrier = await _context.Carriers.FindAsync(id);
            if (carrier != null) {
                _context.Carriers.Remove(carrier);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarrierExists(Guid id) {
            return _context.Carriers.Any(e => e.CarrierGuid == id);
        }
    }
}
