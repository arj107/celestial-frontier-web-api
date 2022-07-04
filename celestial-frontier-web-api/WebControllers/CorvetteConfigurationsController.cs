using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using celestial_frontier_web_api.Models;

namespace celestial_frontier_web_api.WebControllers {
    public class CorvetteConfigurationsController : Controller {
        private readonly CelestialFrontier_DevContext _context;

        public CorvetteConfigurationsController(CelestialFrontier_DevContext context) {
            _context = context;
        }

        // GET: CorvetteConfigurations
        public async Task<IActionResult> Index() {
            var celestialFrontier_DevContext = _context.CorvetteConfigurations.Include(f => f.Armor).Include(f => f.Corvette).Include(f => f.User).Include(f => f.FuelSystem).Include(f => f.Payload).Include(f => f.PointDefenseSystem).Include(f => f.Thruster).Include(f => f.WeaponFrame);
            return View(await celestialFrontier_DevContext.ToListAsync());
        }

        // GET: CorvetteConfigurations/Details/5
        public async Task<IActionResult> Details(Guid? id) {
            if (id == null || _context.CorvetteConfigurations == null) {
                return NotFound();
            }

            var corvetteConfiguration = await _context.CorvetteConfigurations
                .Include(f => f.Armor)
                .Include(f => f.Corvette)
                .Include(f => f.User)
                .Include(f => f.FuelSystem)
                .Include(f => f.Payload)
                .Include(f => f.PointDefenseSystem)
                .Include(f => f.Thruster)
                .Include(f => f.WeaponFrame)
                .FirstOrDefaultAsync(m => m.CorvetteConfigurationGuid == id);
            if (corvetteConfiguration == null) {
                return NotFound();
            }

            return View(corvetteConfiguration);
        }

        // GET: CorvetteConfigurations/Create
        public IActionResult Create() {
            ViewData["ArmorGuid"] = new SelectList(_context.Armors, "ArmorGuid", "Title");
            ViewData["CorvetteGuid"] = new SelectList(_context.Corvettes, "CorvetteGuid", "Class");
            ViewData["UserGuid"] = new SelectList(_context.Users, "UserGuid", "Username");
            ViewData["FuelSystemGuid"] = new SelectList(_context.FuelSystems, "FuelSystemGuid", "Title");
            ViewData["PayloadGuid"] = new SelectList(_context.Payloads, "PayloadGuid", "Title");
            ViewData["PointDefenseSystemGuid"] = new SelectList(_context.PointDefenseSystems, "PointDefenseSystemGuid", "Title");
            ViewData["ThrusterGuid"] = new SelectList(_context.Thrusters, "ThrusterGuid", "Title");
            ViewData["WeaponFrameGuid"] = new SelectList(_context.WeaponFrames, "WeaponFrameGuid", "Title");
            return View();
        }

        // POST: CorvetteConfigurations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CorvetteConfigurationGuid,UserGuid,CorvetteGuid,WeaponFrameGuid,PayloadGuid,ArmorGuid,PointDefenseSystemGuid,FuelSystemGuid,ThrusterGuid")] CorvetteConfiguration corvetteConfiguration) {
            if (ModelState.IsValid) {
                corvetteConfiguration.CorvetteConfigurationGuid = Guid.NewGuid();
                _context.Add(corvetteConfiguration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArmorGuid"] = new SelectList(_context.Armors, "ArmorGuid", "Title", corvetteConfiguration.ArmorGuid);
            ViewData["CorvetteGuid"] = new SelectList(_context.Corvettes, "CorvetteGuid", "Class", corvetteConfiguration.CorvetteGuid);
            ViewData["UserGuid"] = new SelectList(_context.Users, "UserGuid", "Username", corvetteConfiguration.UserGuid);
            ViewData["FuelSystemGuid"] = new SelectList(_context.FuelSystems, "FuelSystemGuid", "Title", corvetteConfiguration.FuelSystemGuid);
            ViewData["PayloadGuid"] = new SelectList(_context.Payloads, "PayloadGuid", "Title", corvetteConfiguration.PayloadGuid);
            ViewData["PointDefenseSystemGuid"] = new SelectList(_context.PointDefenseSystems, "PointDefenseSystemGuid", "Title", corvetteConfiguration.PointDefenseSystemGuid);
            ViewData["ThrusterGuid"] = new SelectList(_context.Thrusters, "ThrusterGuid", "Title", corvetteConfiguration.ThrusterGuid);
            ViewData["WeaponFrameGuid"] = new SelectList(_context.WeaponFrames, "WeaponFrameGuid", "Title", corvetteConfiguration.WeaponFrameGuid);
            return View(corvetteConfiguration);
        }

        // GET: CorvetteConfigurations/Edit/5
        public async Task<IActionResult> Edit(Guid? id) {
            if (id == null || _context.CorvetteConfigurations == null) {
                return NotFound();
            }

            var corvetteConfiguration = await _context.CorvetteConfigurations.FindAsync(id);
            if (corvetteConfiguration == null) {
                return NotFound();
            }
            ViewData["ArmorGuid"] = new SelectList(_context.Armors, "ArmorGuid", "Title", corvetteConfiguration.ArmorGuid);
            ViewData["CorvetteGuid"] = new SelectList(_context.Corvettes, "CorvetteGuid", "Class", corvetteConfiguration.CorvetteGuid);
            ViewData["UserGuid"] = new SelectList(_context.Users, "UserGuid", "Username", corvetteConfiguration.UserGuid);
            ViewData["FuelSystemGuid"] = new SelectList(_context.FuelSystems, "FuelSystemGuid", "Title", corvetteConfiguration.FuelSystemGuid);
            ViewData["PayloadGuid"] = new SelectList(_context.Payloads, "PayloadGuid", "Title", corvetteConfiguration.PayloadGuid);
            ViewData["PointDefenseSystemGuid"] = new SelectList(_context.PointDefenseSystems, "PointDefenseSystemGuid", "Title", corvetteConfiguration.PointDefenseSystemGuid);
            ViewData["ThrusterGuid"] = new SelectList(_context.Thrusters, "ThrusterGuid", "Title", corvetteConfiguration.ThrusterGuid);
            ViewData["WeaponFrameGuid"] = new SelectList(_context.WeaponFrames, "WeaponFrameGuid", "Title", corvetteConfiguration.WeaponFrameGuid);
            return View(corvetteConfiguration);
        }

        // POST: CorvetteConfigurations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CorvetteConfigurationGuid,UserGuid,CorvetteGuid,WeaponFrameGuid,PayloadGuid,ArmorGuid,PointDefenseSystemGuid,FuelSystemGuid,ThrusterGuid")] CorvetteConfiguration corvetteConfiguration) {
            if (id != corvetteConfiguration.CorvetteConfigurationGuid) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(corvetteConfiguration);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!CorvetteConfigurationExists(corvetteConfiguration.CorvetteConfigurationGuid)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArmorGuid"] = new SelectList(_context.Armors, "ArmorGuid", "Title", corvetteConfiguration.ArmorGuid);
            ViewData["CorvetteGuid"] = new SelectList(_context.Corvettes, "CorvetteGuid", "Class", corvetteConfiguration.CorvetteGuid);
            ViewData["UserGuid"] = new SelectList(_context.Users, "UserGuid", "Username", corvetteConfiguration.UserGuid);
            ViewData["FuelSystemGuid"] = new SelectList(_context.FuelSystems, "FuelSystemGuid", "Title", corvetteConfiguration.FuelSystemGuid);
            ViewData["PayloadGuid"] = new SelectList(_context.Payloads, "PayloadGuid", "Title", corvetteConfiguration.PayloadGuid);
            ViewData["PointDefenseSystemGuid"] = new SelectList(_context.PointDefenseSystems, "PointDefenseSystemGuid", "Title", corvetteConfiguration.PointDefenseSystemGuid);
            ViewData["ThrusterGuid"] = new SelectList(_context.Thrusters, "ThrusterGuid", "Title", corvetteConfiguration.ThrusterGuid);
            ViewData["WeaponFrameGuid"] = new SelectList(_context.WeaponFrames, "WeaponFrameGuid", "Title", corvetteConfiguration.WeaponFrameGuid);
            return View(corvetteConfiguration);
        }

        // GET: CorvetteConfigurations/Delete/5
        public async Task<IActionResult> Delete(Guid? id) {
            if (id == null || _context.CorvetteConfigurations == null) {
                return NotFound();
            }

            var corvetteConfiguration = await _context.CorvetteConfigurations
                .Include(f => f.Armor)
                .Include(f => f.Corvette)
                .Include(f => f.User)
                .Include(f => f.FuelSystem)
                .Include(f => f.Payload)
                .Include(f => f.PointDefenseSystem)
                .Include(f => f.Thruster)
                .Include(f => f.WeaponFrame)
                .FirstOrDefaultAsync(m => m.CorvetteConfigurationGuid == id);
            if (corvetteConfiguration == null) {
                return NotFound();
            }

            return View(corvetteConfiguration);
        }

        // POST: CorvetteConfigurations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id) {
            if (_context.CorvetteConfigurations == null) {
                return Problem("Entity set 'CelestialFrontier_DevContext.CorvetteConfigurations'  is null.");
            }
            var corvetteConfiguration = await _context.CorvetteConfigurations.FindAsync(id);
            if (corvetteConfiguration != null) {
                _context.CorvetteConfigurations.Remove(corvetteConfiguration);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CorvetteConfigurationExists(Guid id) {
            return _context.CorvetteConfigurations.Any(e => e.CorvetteConfigurationGuid == id);
        }
    }
}
