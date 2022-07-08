using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using celestial_frontier_web_api.Models;

namespace celestial_frontier_web_api.WebControllers {
    public class DestroyerConfigurationsController : Controller {
        private readonly CelestialFrontier_DevContext _context;

        public DestroyerConfigurationsController(CelestialFrontier_DevContext context) {
            _context = context;
        }

        // GET: DestroyerConfigurations
        public async Task<IActionResult> Index() {
            var celestialFrontier_DevContext = _context.DestroyerConfigurations.Include(f => f.Armor).Include(f => f.Destroyer).Include(f => f.User).Include(f => f.FuelSystem).Include(f => f.Payload1).Include(f => f.Payload2).Include(f => f.PointDefenseSystem).Include(f => f.Thruster).Include(f => f.WeaponFrame1).Include(f => f.WeaponFrame2);
            return View(await celestialFrontier_DevContext.ToListAsync());
        }

        // GET: DestroyerConfigurations/Details/5
        public async Task<IActionResult> Details(Guid? id) {
            if (id == null || _context.DestroyerConfigurations == null) {
                return NotFound();
            }

            var destroyerConfiguration = await _context.DestroyerConfigurations
                .Include(f => f.Armor)
                .Include(f => f.Destroyer)
                .Include(f => f.User)
                .Include(f => f.FuelSystem)
                .Include(f => f.Payload1)
                .Include(f => f.Payload2)
                .Include(f => f.PointDefenseSystem)
                .Include(f => f.Thruster)
                .Include(f => f.WeaponFrame1)
                .Include(f => f.WeaponFrame2)
                .FirstOrDefaultAsync(m => m.DestroyerConfigurationGuid == id);
            if (destroyerConfiguration == null) {
                return NotFound();
            }

            return View(destroyerConfiguration);
        }

        // GET: DestroyerConfigurations/Create
        public IActionResult Create() {
            ViewData["ArmorGuid"] = new SelectList(_context.Armors, "ArmorGuid", "Title");
            ViewData["DestroyerGuid"] = new SelectList(_context.Destroyers, "DestroyerGuid", "Class");
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

        // POST: DestroyerConfigurations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DestroyerConfigurationGuid,Name,UserGuid,DestroyerGuid,WeaponFrame1Guid,Payload1Guid,WeaponFrame2Guid,Payload2Guid,ArmorGuid,PointDefenseSystemGuid,FuelSystemGuid,ThrusterGuid")] DestroyerConfiguration destroyerConfiguration) {
            if (ModelState.IsValid) {
                destroyerConfiguration.DestroyerConfigurationGuid = Guid.NewGuid();
                _context.Add(destroyerConfiguration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArmorGuid"] = new SelectList(_context.Armors, "ArmorGuid", "Title", destroyerConfiguration.ArmorGuid);
            ViewData["DestroyerGuid"] = new SelectList(_context.Destroyers, "DestroyerGuid", "Class", destroyerConfiguration.DestroyerGuid);
            ViewData["UserGuid"] = new SelectList(_context.Users, "UserGuid", "Username", destroyerConfiguration.UserGuid);
            ViewData["FuelSystemGuid"] = new SelectList(_context.FuelSystems, "FuelSystemGuid", "Title", destroyerConfiguration.FuelSystemGuid);
            ViewData["Payload1Guid"] = new SelectList(_context.Payloads, "PayloadGuid", "Title", destroyerConfiguration.Payload1Guid);
            ViewData["Payload2Guid"] = new SelectList(_context.Payloads, "PayloadGuid", "Title", destroyerConfiguration.Payload2Guid);
            ViewData["PointDefenseSystemGuid"] = new SelectList(_context.PointDefenseSystems, "PointDefenseSystemGuid", "Title", destroyerConfiguration.PointDefenseSystemGuid);
            ViewData["ThrusterGuid"] = new SelectList(_context.Thrusters, "ThrusterGuid", "Title", destroyerConfiguration.ThrusterGuid);
            ViewData["WeaponFrame1Guid"] = new SelectList(_context.WeaponFrames, "WeaponFrameGuid", "Title", destroyerConfiguration.WeaponFrame1Guid);
            ViewData["WeaponFrame2Guid"] = new SelectList(_context.WeaponFrames, "WeaponFrameGuid", "Title", destroyerConfiguration.WeaponFrame2Guid);
            return View(destroyerConfiguration);
        }

        // GET: DestroyerConfigurations/Edit/5
        public async Task<IActionResult> Edit(Guid? id) {
            if (id == null || _context.DestroyerConfigurations == null) {
                return NotFound();
            }

            var destroyerConfiguration = await _context.DestroyerConfigurations.FindAsync(id);
            if (destroyerConfiguration == null) {
                return NotFound();
            }
            ViewData["ArmorGuid"] = new SelectList(_context.Armors, "ArmorGuid", "Title", destroyerConfiguration.ArmorGuid);
            ViewData["DestroyerGuid"] = new SelectList(_context.Destroyers, "DestroyerGuid", "Class", destroyerConfiguration.DestroyerGuid);
            ViewData["UserGuid"] = new SelectList(_context.Users, "UserGuid", "Username", destroyerConfiguration.UserGuid);
            ViewData["FuelSystemGuid"] = new SelectList(_context.FuelSystems, "FuelSystemGuid", "Title", destroyerConfiguration.FuelSystemGuid);
            ViewData["Payload1Guid"] = new SelectList(_context.Payloads, "PayloadGuid", "Title", destroyerConfiguration.Payload1Guid);
            ViewData["Payload2Guid"] = new SelectList(_context.Payloads, "PayloadGuid", "Title", destroyerConfiguration.Payload2Guid);
            ViewData["PointDefenseSystemGuid"] = new SelectList(_context.PointDefenseSystems, "PointDefenseSystemGuid", "Title", destroyerConfiguration.PointDefenseSystemGuid);
            ViewData["ThrusterGuid"] = new SelectList(_context.Thrusters, "ThrusterGuid", "Title", destroyerConfiguration.ThrusterGuid);
            ViewData["WeaponFrame1Guid"] = new SelectList(_context.WeaponFrames, "WeaponFrameGuid", "Title", destroyerConfiguration.WeaponFrame1Guid);
            ViewData["WeaponFrame2Guid"] = new SelectList(_context.WeaponFrames, "WeaponFrameGuid", "Title", destroyerConfiguration.WeaponFrame2Guid);
            return View(destroyerConfiguration);
        }

        // POST: DestroyerConfigurations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("DestroyerConfigurationGuid,Name,UserGuid,DestroyerGuid,WeaponFrame1Guid,Payload1Guid,WeaponFrame2Guid,Payload2Guid,ArmorGuid,PointDefenseSystemGuid,FuelSystemGuid,ThrusterGuid")] DestroyerConfiguration destroyerConfiguration) {
            if (id != destroyerConfiguration.DestroyerConfigurationGuid) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(destroyerConfiguration);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!DestroyerConfigurationExists(destroyerConfiguration.DestroyerConfigurationGuid)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArmorGuid"] = new SelectList(_context.Armors, "ArmorGuid", "Title", destroyerConfiguration.ArmorGuid);
            ViewData["DestroyerGuid"] = new SelectList(_context.Destroyers, "DestroyerGuid", "Class", destroyerConfiguration.DestroyerGuid);
            ViewData["UserGuid"] = new SelectList(_context.Users, "UserGuid", "Username", destroyerConfiguration.UserGuid);
            ViewData["FuelSystemGuid"] = new SelectList(_context.FuelSystems, "FuelSystemGuid", "Title", destroyerConfiguration.FuelSystemGuid);
            ViewData["Payload1Guid"] = new SelectList(_context.Payloads, "PayloadGuid", "Title", destroyerConfiguration.Payload1Guid);
            ViewData["Payload2Guid"] = new SelectList(_context.Payloads, "PayloadGuid", "Title", destroyerConfiguration.Payload2Guid);
            ViewData["PointDefenseSystemGuid"] = new SelectList(_context.PointDefenseSystems, "PointDefenseSystemGuid", "Title", destroyerConfiguration.PointDefenseSystemGuid);
            ViewData["ThrusterGuid"] = new SelectList(_context.Thrusters, "ThrusterGuid", "Title", destroyerConfiguration.ThrusterGuid);
            ViewData["WeaponFrame1Guid"] = new SelectList(_context.WeaponFrames, "WeaponFrameGuid", "Title", destroyerConfiguration.WeaponFrame1Guid);
            ViewData["WeaponFrame2Guid"] = new SelectList(_context.WeaponFrames, "WeaponFrameGuid", "Title", destroyerConfiguration.WeaponFrame2Guid);
            return View(destroyerConfiguration);
        }

        // GET: DestroyerConfigurations/Delete/5
        public async Task<IActionResult> Delete(Guid? id) {
            if (id == null || _context.DestroyerConfigurations == null) {
                return NotFound();
            }

            var destroyerConfiguration = await _context.DestroyerConfigurations
                .Include(f => f.Armor)
                .Include(f => f.Destroyer)
                .Include(f => f.User)
                .Include(f => f.FuelSystem)
                .Include(f => f.Payload1)
                .Include(f => f.Payload2)
                .Include(f => f.PointDefenseSystem)
                .Include(f => f.Thruster)
                .Include(f => f.WeaponFrame1)
                .Include(f => f.WeaponFrame2)
                .FirstOrDefaultAsync(m => m.DestroyerConfigurationGuid == id);
            if (destroyerConfiguration == null) {
                return NotFound();
            }

            return View(destroyerConfiguration);
        }

        // POST: DestroyerConfigurations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id) {
            if (_context.DestroyerConfigurations == null) {
                return Problem("Entity set 'CelestialFrontier_DevContext.DestroyerConfigurations'  is null.");
            }
            var destroyerConfiguration = await _context.DestroyerConfigurations.FindAsync(id);
            if (destroyerConfiguration != null) {
                _context.DestroyerConfigurations.Remove(destroyerConfiguration);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DestroyerConfigurationExists(Guid id) {
            return _context.DestroyerConfigurations.Any(e => e.DestroyerConfigurationGuid == id);
        }
    }
}
