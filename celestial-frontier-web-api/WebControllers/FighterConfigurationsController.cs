using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using celestial_frontier_web_api.Models;

namespace celestial_frontier_web_api.WebControllers {
    public class FighterConfigurationsController : Controller {
        private readonly CelestialFrontier_DevContext _context;

        public FighterConfigurationsController(CelestialFrontier_DevContext context) {
            _context = context;
        }

        // GET: FighterConfigurations
        public async Task<IActionResult> Index() {
            var celestialFrontier_DevContext = _context.FighterConfigurations.Include(f => f.Armor).Include(f => f.Fighter).Include(f => f.User).Include(f => f.FuelSystem).Include(f => f.Payload).Include(f => f.PointDefenseSystem).Include(f => f.Thruster).Include(f => f.WeaponFrame);
            return View(await celestialFrontier_DevContext.ToListAsync());
        }

        // GET: FighterConfigurations/Details/5
        public async Task<IActionResult> Details(Guid? id) {
            if (id == null || _context.FighterConfigurations == null) {
                return NotFound();
            }

            var fighterConfiguration = await _context.FighterConfigurations
                .Include(f => f.Armor)
                .Include(f => f.Fighter)
                .Include(f => f.User)
                .Include(f => f.FuelSystem)
                .Include(f => f.Payload)
                .Include(f => f.PointDefenseSystem)
                .Include(f => f.Thruster)
                .Include(f => f.WeaponFrame)
                .FirstOrDefaultAsync(m => m.FighterConfigurationGuid == id);
            if (fighterConfiguration == null) {
                return NotFound();
            }

            return View(fighterConfiguration);
        }

        // GET: FighterConfigurations/Create
        public IActionResult Create() {
            ViewData["ArmorGuid"] = new SelectList(_context.Armors, "ArmorGuid", "Title");
            ViewData["FighterGuid"] = new SelectList(_context.Fighters, "FighterGuid", "Class");
            ViewData["UserGuid"] = new SelectList(_context.Users, "UserGuid", "Username");
            ViewData["FuelSystemGuid"] = new SelectList(_context.FuelSystems, "FuelSystemGuid", "Title");
            ViewData["PayloadGuid"] = new SelectList(_context.Payloads, "PayloadGuid", "Title");
            ViewData["PointDefenseSystemGuid"] = new SelectList(_context.PointDefenseSystems, "PointDefenseSystemGuid", "Title");
            ViewData["ThrusterGuid"] = new SelectList(_context.Thrusters, "ThrusterGuid", "Title");
            ViewData["WeaponFrameGuid"] = new SelectList(_context.WeaponFrames, "WeaponFrameGuid", "Title");
            return View();
        }

        // POST: FighterConfigurations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FighterConfigurationGuid,UserGuid,FighterGuid,WeaponFrameGuid,PayloadGuid,ArmorGuid,PointDefenseSystemGuid,FuelSystemGuid,ThrusterGuid")] FighterConfiguration fighterConfiguration) {
            if (ModelState.IsValid) {
                fighterConfiguration.FighterConfigurationGuid = Guid.NewGuid();
                _context.Add(fighterConfiguration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArmorGuid"] = new SelectList(_context.Armors, "ArmorGuid", "Title", fighterConfiguration.ArmorGuid);
            ViewData["FighterGuid"] = new SelectList(_context.Fighters, "FighterGuid", "Class", fighterConfiguration.FighterGuid);
            ViewData["UserGuid"] = new SelectList(_context.Users, "UserGuid", "Username", fighterConfiguration.UserGuid);
            ViewData["FuelSystemGuid"] = new SelectList(_context.FuelSystems, "FuelSystemGuid", "Title", fighterConfiguration.FuelSystemGuid);
            ViewData["PayloadGuid"] = new SelectList(_context.Payloads, "PayloadGuid", "Title", fighterConfiguration.PayloadGuid);
            ViewData["PointDefenseSystemGuid"] = new SelectList(_context.PointDefenseSystems, "PointDefenseSystemGuid", "Title", fighterConfiguration.PointDefenseSystemGuid);
            ViewData["ThrusterGuid"] = new SelectList(_context.Thrusters, "ThrusterGuid", "Title", fighterConfiguration.ThrusterGuid);
            ViewData["WeaponFrameGuid"] = new SelectList(_context.WeaponFrames, "WeaponFrameGuid", "Title", fighterConfiguration.WeaponFrameGuid);
            return View(fighterConfiguration);
        }

        // GET: FighterConfigurations/Edit/5
        public async Task<IActionResult> Edit(Guid? id) {
            if (id == null || _context.FighterConfigurations == null) {
                return NotFound();
            }

            var fighterConfiguration = await _context.FighterConfigurations.FindAsync(id);
            if (fighterConfiguration == null) {
                return NotFound();
            }
            ViewData["ArmorGuid"] = new SelectList(_context.Armors, "ArmorGuid", "Title", fighterConfiguration.ArmorGuid);
            ViewData["FighterGuid"] = new SelectList(_context.Fighters, "FighterGuid", "Class", fighterConfiguration.FighterGuid);
            ViewData["UserGuid"] = new SelectList(_context.Users, "UserGuid", "Username", fighterConfiguration.UserGuid);
            ViewData["FuelSystemGuid"] = new SelectList(_context.FuelSystems, "FuelSystemGuid", "Title", fighterConfiguration.FuelSystemGuid);
            ViewData["PayloadGuid"] = new SelectList(_context.Payloads, "PayloadGuid", "Title", fighterConfiguration.PayloadGuid);
            ViewData["PointDefenseSystemGuid"] = new SelectList(_context.PointDefenseSystems, "PointDefenseSystemGuid", "Title", fighterConfiguration.PointDefenseSystemGuid);
            ViewData["ThrusterGuid"] = new SelectList(_context.Thrusters, "ThrusterGuid", "Title", fighterConfiguration.ThrusterGuid);
            ViewData["WeaponFrameGuid"] = new SelectList(_context.WeaponFrames, "WeaponFrameGuid", "Title", fighterConfiguration.WeaponFrameGuid);
            return View(fighterConfiguration);
        }

        // POST: FighterConfigurations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("FighterConfigurationGuid,UserGuid,FighterGuid,WeaponFrameGuid,PayloadGuid,ArmorGuid,PointDefenseSystemGuid,FuelSystemGuid,ThrusterGuid")] FighterConfiguration fighterConfiguration) {
            if (id != fighterConfiguration.FighterConfigurationGuid) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(fighterConfiguration);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!FighterConfigurationExists(fighterConfiguration.FighterConfigurationGuid)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArmorGuid"] = new SelectList(_context.Armors, "ArmorGuid", "Title", fighterConfiguration.ArmorGuid);
            ViewData["FighterGuid"] = new SelectList(_context.Fighters, "FighterGuid", "Class", fighterConfiguration.FighterGuid);
            ViewData["UserGuid"] = new SelectList(_context.Users, "UserGuid", "Username", fighterConfiguration.UserGuid);
            ViewData["FuelSystemGuid"] = new SelectList(_context.FuelSystems, "FuelSystemGuid", "Title", fighterConfiguration.FuelSystemGuid);
            ViewData["PayloadGuid"] = new SelectList(_context.Payloads, "PayloadGuid", "Title", fighterConfiguration.PayloadGuid);
            ViewData["PointDefenseSystemGuid"] = new SelectList(_context.PointDefenseSystems, "PointDefenseSystemGuid", "Title", fighterConfiguration.PointDefenseSystemGuid);
            ViewData["ThrusterGuid"] = new SelectList(_context.Thrusters, "ThrusterGuid", "Title", fighterConfiguration.ThrusterGuid);
            ViewData["WeaponFrameGuid"] = new SelectList(_context.WeaponFrames, "WeaponFrameGuid", "Title", fighterConfiguration.WeaponFrameGuid);
            return View(fighterConfiguration);
        }

        // GET: FighterConfigurations/Delete/5
        public async Task<IActionResult> Delete(Guid? id) {
            if (id == null || _context.FighterConfigurations == null) {
                return NotFound();
            }

            var fighterConfiguration = await _context.FighterConfigurations
                .Include(f => f.Armor)
                .Include(f => f.Fighter)
                .Include(f => f.User)
                .Include(f => f.FuelSystem)
                .Include(f => f.Payload)
                .Include(f => f.PointDefenseSystem)
                .Include(f => f.Thruster)
                .Include(f => f.WeaponFrame)
                .FirstOrDefaultAsync(m => m.FighterConfigurationGuid == id);
            if (fighterConfiguration == null) {
                return NotFound();
            }

            return View(fighterConfiguration);
        }

        // POST: FighterConfigurations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id) {
            if (_context.FighterConfigurations == null) {
                return Problem("Entity set 'CelestialFrontier_DevContext.FighterConfigurations'  is null.");
            }
            var fighterConfiguration = await _context.FighterConfigurations.FindAsync(id);
            if (fighterConfiguration != null) {
                _context.FighterConfigurations.Remove(fighterConfiguration);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FighterConfigurationExists(Guid id) {
            return _context.FighterConfigurations.Any(e => e.FighterConfigurationGuid == id);
        }
    }
}
