using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using celestial_frontier_web_api.Models;

namespace celestial_frontier_web_api.WebControllers {
    public class CarrierConfigurationsController : Controller {
        private readonly CelestialFrontier_DevContext _context;

        public CarrierConfigurationsController(CelestialFrontier_DevContext context) {
            _context = context;
        }

        // GET: CarrierConfigurations
        public async Task<IActionResult> Index() {
            var celestialFrontier_DevContext = _context.CarrierConfigurations.Include(f => f.Armor).Include(f => f.Carrier).Include(f => f.User).Include(f => f.FuelSystem).Include(f => f.Payload).Include(f => f.PointDefenseSystem).Include(f => f.Thruster).Include(f => f.WeaponFrame);
            return View(await celestialFrontier_DevContext.ToListAsync());
        }

        // GET: CarrierConfigurations/Details/5
        public async Task<IActionResult> Details(Guid? id) {
            if (id == null || _context.CarrierConfigurations == null) {
                return NotFound();
            }

            var carrierConfiguration = await _context.CarrierConfigurations
                .Include(f => f.Armor)
                .Include(f => f.Carrier)
                .Include(f => f.User)
                .Include(f => f.FuelSystem)
                .Include(f => f.Payload)
                .Include(f => f.PointDefenseSystem)
                .Include(f => f.Thruster)
                .Include(f => f.WeaponFrame)
                .FirstOrDefaultAsync(m => m.CarrierConfigurationGuid == id);
            if (carrierConfiguration == null) {
                return NotFound();
            }

            return View(carrierConfiguration);
        }

        // GET: CarrierConfigurations/Create
        public IActionResult Create() {
            ViewData["ArmorGuid"] = new SelectList(_context.Armors, "ArmorGuid", "Title");
            ViewData["CarrierGuid"] = new SelectList(_context.Carriers, "CarrierGuid", "Class");
            ViewData["UserGuid"] = new SelectList(_context.Users, "UserGuid", "Username");
            ViewData["FuelSystemGuid"] = new SelectList(_context.FuelSystems, "FuelSystemGuid", "Title");
            ViewData["PayloadGuid"] = new SelectList(_context.Payloads, "PayloadGuid", "Title");
            ViewData["PointDefenseSystemGuid"] = new SelectList(_context.PointDefenseSystems, "PointDefenseSystemGuid", "Title");
            ViewData["ThrusterGuid"] = new SelectList(_context.Thrusters, "ThrusterGuid", "Title");
            ViewData["WeaponFrameGuid"] = new SelectList(_context.WeaponFrames, "WeaponFrameGuid", "Title");
            return View();
        }

        // POST: CarrierConfigurations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarrierConfigurationGuid,UserGuid,CarrierGuid,WeaponFrameGuid,PayloadGuid,ArmorGuid,PointDefenseSystemGuid,FuelSystemGuid,ThrusterGuid")] CarrierConfiguration carrierConfiguration) {
            if (ModelState.IsValid) {
                carrierConfiguration.CarrierConfigurationGuid = Guid.NewGuid();
                _context.Add(carrierConfiguration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArmorGuid"] = new SelectList(_context.Armors, "ArmorGuid", "Title", carrierConfiguration.ArmorGuid);
            ViewData["CarrierGuid"] = new SelectList(_context.Carriers, "CarrierGuid", "Class", carrierConfiguration.CarrierGuid);
            ViewData["UserGuid"] = new SelectList(_context.Users, "UserGuid", "Username", carrierConfiguration.UserGuid);
            ViewData["FuelSystemGuid"] = new SelectList(_context.FuelSystems, "FuelSystemGuid", "Title", carrierConfiguration.FuelSystemGuid);
            ViewData["PayloadGuid"] = new SelectList(_context.Payloads, "PayloadGuid", "Title", carrierConfiguration.PayloadGuid);
            ViewData["PointDefenseSystemGuid"] = new SelectList(_context.PointDefenseSystems, "PointDefenseSystemGuid", "Title", carrierConfiguration.PointDefenseSystemGuid);
            ViewData["ThrusterGuid"] = new SelectList(_context.Thrusters, "ThrusterGuid", "Title", carrierConfiguration.ThrusterGuid);
            ViewData["WeaponFrameGuid"] = new SelectList(_context.WeaponFrames, "WeaponFrameGuid", "Title", carrierConfiguration.WeaponFrameGuid);
            return View(carrierConfiguration);
        }

        // GET: CarrierConfigurations/Edit/5
        public async Task<IActionResult> Edit(Guid? id) {
            if (id == null || _context.CarrierConfigurations == null) {
                return NotFound();
            }

            var carrierConfiguration = await _context.CarrierConfigurations.FindAsync(id);
            if (carrierConfiguration == null) {
                return NotFound();
            }
            ViewData["ArmorGuid"] = new SelectList(_context.Armors, "ArmorGuid", "Title", carrierConfiguration.ArmorGuid);
            ViewData["CarrierGuid"] = new SelectList(_context.Carriers, "CarrierGuid", "Class", carrierConfiguration.CarrierGuid);
            ViewData["UserGuid"] = new SelectList(_context.Users, "UserGuid", "Username", carrierConfiguration.UserGuid);
            ViewData["FuelSystemGuid"] = new SelectList(_context.FuelSystems, "FuelSystemGuid", "Title", carrierConfiguration.FuelSystemGuid);
            ViewData["PayloadGuid"] = new SelectList(_context.Payloads, "PayloadGuid", "Title", carrierConfiguration.PayloadGuid);
            ViewData["PointDefenseSystemGuid"] = new SelectList(_context.PointDefenseSystems, "PointDefenseSystemGuid", "Title", carrierConfiguration.PointDefenseSystemGuid);
            ViewData["ThrusterGuid"] = new SelectList(_context.Thrusters, "ThrusterGuid", "Title", carrierConfiguration.ThrusterGuid);
            ViewData["WeaponFrameGuid"] = new SelectList(_context.WeaponFrames, "WeaponFrameGuid", "Title", carrierConfiguration.WeaponFrameGuid);
            return View(carrierConfiguration);
        }

        // POST: CarrierConfigurations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CarrierConfigurationGuid,UserGuid,CarrierGuid,WeaponFrameGuid,PayloadGuid,ArmorGuid,PointDefenseSystemGuid,FuelSystemGuid,ThrusterGuid")] CarrierConfiguration carrierConfiguration) {
            if (id != carrierConfiguration.CarrierConfigurationGuid) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(carrierConfiguration);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!CarrierConfigurationExists(carrierConfiguration.CarrierConfigurationGuid)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArmorGuid"] = new SelectList(_context.Armors, "ArmorGuid", "Title", carrierConfiguration.ArmorGuid);
            ViewData["CarrierGuid"] = new SelectList(_context.Carriers, "CarrierGuid", "Class", carrierConfiguration.CarrierGuid);
            ViewData["UserGuid"] = new SelectList(_context.Users, "UserGuid", "Username", carrierConfiguration.UserGuid);
            ViewData["FuelSystemGuid"] = new SelectList(_context.FuelSystems, "FuelSystemGuid", "Title", carrierConfiguration.FuelSystemGuid);
            ViewData["PayloadGuid"] = new SelectList(_context.Payloads, "PayloadGuid", "Title", carrierConfiguration.PayloadGuid);
            ViewData["PointDefenseSystemGuid"] = new SelectList(_context.PointDefenseSystems, "PointDefenseSystemGuid", "Title", carrierConfiguration.PointDefenseSystemGuid);
            ViewData["ThrusterGuid"] = new SelectList(_context.Thrusters, "ThrusterGuid", "Title", carrierConfiguration.ThrusterGuid);
            ViewData["WeaponFrameGuid"] = new SelectList(_context.WeaponFrames, "WeaponFrameGuid", "Title", carrierConfiguration.WeaponFrameGuid);
            return View(carrierConfiguration);
        }

        // GET: CarrierConfigurations/Delete/5
        public async Task<IActionResult> Delete(Guid? id) {
            if (id == null || _context.CarrierConfigurations == null) {
                return NotFound();
            }

            var carrierConfiguration = await _context.CarrierConfigurations
                .Include(f => f.Armor)
                .Include(f => f.Carrier)
                .Include(f => f.User)
                .Include(f => f.FuelSystem)
                .Include(f => f.Payload)
                .Include(f => f.PointDefenseSystem)
                .Include(f => f.Thruster)
                .Include(f => f.WeaponFrame)
                .FirstOrDefaultAsync(m => m.CarrierConfigurationGuid == id);
            if (carrierConfiguration == null) {
                return NotFound();
            }

            return View(carrierConfiguration);
        }

        // POST: CarrierConfigurations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id) {
            if (_context.CarrierConfigurations == null) {
                return Problem("Entity set 'CelestialFrontier_DevContext.CarrierConfigurations'  is null.");
            }
            var carrierConfiguration = await _context.CarrierConfigurations.FindAsync(id);
            if (carrierConfiguration != null) {
                _context.CarrierConfigurations.Remove(carrierConfiguration);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarrierConfigurationExists(Guid id) {
            return _context.CarrierConfigurations.Any(e => e.CarrierConfigurationGuid == id);
        }
    }
}
