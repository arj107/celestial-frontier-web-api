using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using celestial_frontier_web_api.Models;

namespace celestial_frontier_web_api.WebControllers {
    public class BattlecruiserConfigurationsController : Controller {
        private readonly CelestialFrontier_DevContext _context;

        public BattlecruiserConfigurationsController(CelestialFrontier_DevContext context) {
            _context = context;
        }

        // GET: BattlecruiserConfigurations
        public async Task<IActionResult> Index() {
            var celestialFrontier_DevContext = _context.BattlecruiserConfigurations.Include(f => f.Armor).Include(f => f.Battlecruiser).Include(f => f.User).Include(f => f.FuelSystem).Include(f => f.Payload1).Include(f => f.Payload2).Include(f => f.PointDefenseSystem).Include(f => f.Thruster).Include(f => f.WeaponFrame1).Include(f => f.WeaponFrame2);
            return View(await celestialFrontier_DevContext.ToListAsync());
        }

        // GET: BattlecruiserConfigurations/Details/5
        public async Task<IActionResult> Details(Guid? id) {
            if (id == null || _context.BattlecruiserConfigurations == null) {
                return NotFound();
            }

            var battlecruiserConfiguration = await _context.BattlecruiserConfigurations
                .Include(f => f.Armor)
                .Include(f => f.Battlecruiser)
                .Include(f => f.User)
                .Include(f => f.FuelSystem)
                .Include(f => f.Payload1)
                .Include(f => f.Payload2)
                .Include(f => f.PointDefenseSystem)
                .Include(f => f.Thruster)
                .Include(f => f.WeaponFrame1)
                .Include(f => f.WeaponFrame2)
                .FirstOrDefaultAsync(m => m.BattlecruiserConfigurationGuid == id);
            if (battlecruiserConfiguration == null) {
                return NotFound();
            }

            return View(battlecruiserConfiguration);
        }

        // GET: BattlecruiserConfigurations/Create
        public IActionResult Create() {
            ViewData["ArmorGuid"] = new SelectList(_context.Armors, "ArmorGuid", "Title");
            ViewData["BattlecruiserGuid"] = new SelectList(_context.Battlecruisers, "BattlecruiserGuid", "Class");
            ViewData["UserGuid"] = new SelectList(_context.Users, "UserGuid", "Username");
            ViewData["FuelSystemGuid"] = new SelectList(_context.FuelSystems, "FuelSystemGuid", "Title");
            ViewData["Payload1Guid"] = new SelectList(_context.Payloads, "PayloadGuid", "Title");
            ViewData["Payload2Guid"] = new SelectList(_context.Payloads, "PayloadGuid", "Title");
            ViewData["PointDefenseSystemGuid"] = new SelectList(_context.PointDefenseSystems, "PointDefenseSystemGuid", "Title");
            ViewData["ThrusterGuid"] = new SelectList(_context.Thrusters, "ThrusterGuid", "Title");
            ViewData["WeaponFrame1Guid"] = new SelectList(_context.WeaponFrames, "WeaponFrameGuid", "Title");
            ViewData["WeaponFrame2Guid"] = new SelectList(_context.WeaponFrames, "WeaponFrameGuid", "Title");
            return View();
        }

        // POST: BattlecruiserConfigurations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BattlecruiserConfigurationGuid,Name,UserGuid,BattlecruiserGuid,WeaponFrame1Guid,Payload1Guid,WeaponFrame2Guid,Payload2Guid,ArmorGuid,PointDefenseSystemGuid,FuelSystemGuid,ThrusterGuid")] BattlecruiserConfiguration battlecruiserConfiguration) {
            if (ModelState.IsValid) {
                battlecruiserConfiguration.BattlecruiserConfigurationGuid = Guid.NewGuid();
                _context.Add(battlecruiserConfiguration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArmorGuid"] = new SelectList(_context.Armors, "ArmorGuid", "Title", battlecruiserConfiguration.ArmorGuid);
            ViewData["BattlecruiserGuid"] = new SelectList(_context.Battlecruisers, "BattlecruiserGuid", "Class", battlecruiserConfiguration.BattlecruiserGuid);
            ViewData["UserGuid"] = new SelectList(_context.Users, "UserGuid", "Username", battlecruiserConfiguration.UserGuid);
            ViewData["FuelSystemGuid"] = new SelectList(_context.FuelSystems, "FuelSystemGuid", "Title", battlecruiserConfiguration.FuelSystemGuid);
            ViewData["Payload1Guid"] = new SelectList(_context.Payloads, "PayloadGuid", "Title", battlecruiserConfiguration.Payload1Guid);
            ViewData["Payload2Guid"] = new SelectList(_context.Payloads, "PayloadGuid", "Title", battlecruiserConfiguration.Payload2Guid);
            ViewData["PointDefenseSystemGuid"] = new SelectList(_context.PointDefenseSystems, "PointDefenseSystemGuid", "Title", battlecruiserConfiguration.PointDefenseSystemGuid);
            ViewData["ThrusterGuid"] = new SelectList(_context.Thrusters, "ThrusterGuid", "Title", battlecruiserConfiguration.ThrusterGuid);
            ViewData["WeaponFrame1Guid"] = new SelectList(_context.WeaponFrames, "WeaponFrameGuid", "Title", battlecruiserConfiguration.WeaponFrame1Guid);
            ViewData["WeaponFrame2Guid"] = new SelectList(_context.WeaponFrames, "WeaponFrameGuid", "Title", battlecruiserConfiguration.WeaponFrame2Guid);
            return View(battlecruiserConfiguration);
        }

        // GET: BattlecruiserConfigurations/Edit/5
        public async Task<IActionResult> Edit(Guid? id) {
            if (id == null || _context.BattlecruiserConfigurations == null) {
                return NotFound();
            }

            var battlecruiserConfiguration = await _context.BattlecruiserConfigurations.FindAsync(id);
            if (battlecruiserConfiguration == null) {
                return NotFound();
            }
            ViewData["ArmorGuid"] = new SelectList(_context.Armors, "ArmorGuid", "Title", battlecruiserConfiguration.ArmorGuid);
            ViewData["BattlecruiserGuid"] = new SelectList(_context.Battlecruisers, "BattlecruiserGuid", "Class", battlecruiserConfiguration.BattlecruiserGuid);
            ViewData["UserGuid"] = new SelectList(_context.Users, "UserGuid", "Username", battlecruiserConfiguration.UserGuid);
            ViewData["FuelSystemGuid"] = new SelectList(_context.FuelSystems, "FuelSystemGuid", "Title", battlecruiserConfiguration.FuelSystemGuid);
            ViewData["Payload1Guid"] = new SelectList(_context.Payloads, "PayloadGuid", "Title", battlecruiserConfiguration.Payload1Guid);
            ViewData["Payload2Guid"] = new SelectList(_context.Payloads, "PayloadGuid", "Title", battlecruiserConfiguration.Payload2Guid);
            ViewData["PointDefenseSystemGuid"] = new SelectList(_context.PointDefenseSystems, "PointDefenseSystemGuid", "Title", battlecruiserConfiguration.PointDefenseSystemGuid);
            ViewData["ThrusterGuid"] = new SelectList(_context.Thrusters, "ThrusterGuid", "Title", battlecruiserConfiguration.ThrusterGuid);
            ViewData["WeaponFrame1Guid"] = new SelectList(_context.WeaponFrames, "WeaponFrameGuid", "Title", battlecruiserConfiguration.WeaponFrame1Guid);
            ViewData["WeaponFrame2Guid"] = new SelectList(_context.WeaponFrames, "WeaponFrameGuid", "Title", battlecruiserConfiguration.WeaponFrame2Guid);
            return View(battlecruiserConfiguration);
        }

        // POST: BattlecruiserConfigurations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("BattlecruiserConfigurationGuid,Name,UserGuid,BattlecruiserGuid,WeaponFrame1Guid,Payload1Guid,WeaponFrame2Guid,Payload2Guid,ArmorGuid,PointDefenseSystemGuid,FuelSystemGuid,ThrusterGuid")] BattlecruiserConfiguration battlecruiserConfiguration) {
            if (id != battlecruiserConfiguration.BattlecruiserConfigurationGuid) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(battlecruiserConfiguration);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!BattlecruiserConfigurationExists(battlecruiserConfiguration.BattlecruiserConfigurationGuid)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArmorGuid"] = new SelectList(_context.Armors, "ArmorGuid", "Title", battlecruiserConfiguration.ArmorGuid);
            ViewData["BattlecruiserGuid"] = new SelectList(_context.Battlecruisers, "BattlecruiserGuid", "Class", battlecruiserConfiguration.BattlecruiserGuid);
            ViewData["UserGuid"] = new SelectList(_context.Users, "UserGuid", "Username", battlecruiserConfiguration.UserGuid);
            ViewData["FuelSystemGuid"] = new SelectList(_context.FuelSystems, "FuelSystemGuid", "Title", battlecruiserConfiguration.FuelSystemGuid);
            ViewData["Payload1Guid"] = new SelectList(_context.Payloads, "PayloadGuid", "Title", battlecruiserConfiguration.Payload1Guid);
            ViewData["Payload2Guid"] = new SelectList(_context.Payloads, "PayloadGuid", "Title", battlecruiserConfiguration.Payload2Guid);
            ViewData["PointDefenseSystemGuid"] = new SelectList(_context.PointDefenseSystems, "PointDefenseSystemGuid", "Title", battlecruiserConfiguration.PointDefenseSystemGuid);
            ViewData["ThrusterGuid"] = new SelectList(_context.Thrusters, "ThrusterGuid", "Title", battlecruiserConfiguration.ThrusterGuid);
            ViewData["WeaponFrame1Guid"] = new SelectList(_context.WeaponFrames, "WeaponFrameGuid", "Title", battlecruiserConfiguration.WeaponFrame1Guid);
            ViewData["WeaponFrame2Guid"] = new SelectList(_context.WeaponFrames, "WeaponFrameGuid", "Title", battlecruiserConfiguration.WeaponFrame2Guid);
            return View(battlecruiserConfiguration);
        }

        // GET: BattlecruiserConfigurations/Delete/5
        public async Task<IActionResult> Delete(Guid? id) {
            if (id == null || _context.BattlecruiserConfigurations == null) {
                return NotFound();
            }

            var battlecruiserConfiguration = await _context.BattlecruiserConfigurations
                .Include(f => f.Armor)
                .Include(f => f.Battlecruiser)
                .Include(f => f.User)
                .Include(f => f.FuelSystem)
                .Include(f => f.Payload1)
                .Include(f => f.Payload2)
                .Include(f => f.PointDefenseSystem)
                .Include(f => f.Thruster)
                .Include(f => f.WeaponFrame1)
                .Include(f => f.WeaponFrame2)
                .FirstOrDefaultAsync(m => m.BattlecruiserConfigurationGuid == id);
            if (battlecruiserConfiguration == null) {
                return NotFound();
            }

            return View(battlecruiserConfiguration);
        }

        // POST: BattlecruiserConfigurations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id) {
            if (_context.BattlecruiserConfigurations == null) {
                return Problem("Entity set 'CelestialFrontier_DevContext.BattlecruiserConfigurations'  is null.");
            }
            var battlecruiserConfiguration = await _context.BattlecruiserConfigurations.FindAsync(id);
            if (battlecruiserConfiguration != null) {
                _context.BattlecruiserConfigurations.Remove(battlecruiserConfiguration);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BattlecruiserConfigurationExists(Guid id) {
            return _context.BattlecruiserConfigurations.Any(e => e.BattlecruiserConfigurationGuid == id);
        }
    }
}
