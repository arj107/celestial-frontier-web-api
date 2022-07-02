using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using celestial_frontier_web_api.Models;

namespace celestial_frontier_web_api.WebControllers {
    public class FleetDestroyersController : Controller {
        private readonly CelestialFrontier_DevContext _context;

        public FleetDestroyersController(CelestialFrontier_DevContext context) {
            _context = context;
        }

        // GET: FleetDestroyers
        public async Task<IActionResult> Index() {
            var celestialFrontier_DevContext = _context.FleetDestroyers.Include(f => f.Armor).Include(f => f.Destroyer).Include(f => f.Fleet).Include(f => f.FuelSystem).Include(f => f.Payload1).Include(f => f.Payload2).Include(f => f.PointDefenseSystem).Include(f => f.Thruster).Include(f => f.WeaponFrame1).Include(f => f.WeaponFrame2);
            return View(await celestialFrontier_DevContext.ToListAsync());
        }

        // GET: FleetDestroyers/Details/5
        public async Task<IActionResult> Details(Guid? id) {
            if (id == null || _context.FleetDestroyers == null) {
                return NotFound();
            }

            var fleetDestroyer = await _context.FleetDestroyers
                .Include(f => f.Armor)
                .Include(f => f.Destroyer)
                .Include(f => f.Fleet)
                .Include(f => f.FuelSystem)
                .Include(f => f.Payload1)
                .Include(f => f.Payload2)
                .Include(f => f.PointDefenseSystem)
                .Include(f => f.Thruster)
                .Include(f => f.WeaponFrame1)
                .Include(f => f.WeaponFrame2)
                .FirstOrDefaultAsync(m => m.FleetDestroyerGuid == id);
            if (fleetDestroyer == null) {
                return NotFound();
            }

            return View(fleetDestroyer);
        }

        // GET: FleetDestroyers/Create
        public IActionResult Create() {
            ViewData["ArmorGuid"] = new SelectList(_context.Armors, "ArmorGuid", "Title");
            ViewData["DestroyerGuid"] = new SelectList(_context.Destroyers, "DestroyerGuid", "Class");
            ViewData["FleetGuid"] = new SelectList(_context.Fleets, "FleetGuid", "Name");
            ViewData["FuelSystemGuid"] = new SelectList(_context.FuelSystems, "FuelSystemGuid", "Title");
            ViewData["Payload1Guid"] = new SelectList(_context.Payloads, "PayloadGuid", "Title");
            ViewData["Payload2Guid"] = new SelectList(_context.Payloads, "PayloadGuid", "Title");
            ViewData["PointDefenseSystemGuid"] = new SelectList(_context.PointDefenseSystems, "PointDefenseSystemGuid", "Title");
            ViewData["ThrusterGuid"] = new SelectList(_context.Thrusters, "ThrusterGuid", "Title");
            ViewData["WeaponFrame1Guid"] = new SelectList(_context.WeaponFrames, "WeaponFrameGuid", "Title");
            ViewData["WeaponFrame2Guid"] = new SelectList(_context.WeaponFrames, "WeaponFrameGuid", "Title");
            return View();
        }

        // POST: FleetDestroyers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FleetDestroyerGuid,FleetGuid,DestroyerGuid,WeaponFrame1Guid,Payload1Guid,WeaponFrame2Guid,Payload2Guid,ArmorGuid,PointDefenseSystemGuid,FuelSystemGuid,ThrusterGuid")] FleetDestroyer fleetDestroyer) {
            if (ModelState.IsValid) {
                fleetDestroyer.FleetDestroyerGuid = Guid.NewGuid();
                _context.Add(fleetDestroyer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArmorGuid"] = new SelectList(_context.Armors, "ArmorGuid", "Title", fleetDestroyer.ArmorGuid);
            ViewData["DestroyerGuid"] = new SelectList(_context.Destroyers, "DestroyerGuid", "Class", fleetDestroyer.DestroyerGuid);
            ViewData["FleetGuid"] = new SelectList(_context.Fleets, "FleetGuid", "Name", fleetDestroyer.FleetGuid);
            ViewData["FuelSystemGuid"] = new SelectList(_context.FuelSystems, "FuelSystemGuid", "Title", fleetDestroyer.FuelSystemGuid);
            ViewData["Payload1Guid"] = new SelectList(_context.Payloads, "PayloadGuid", "Title", fleetDestroyer.Payload1Guid);
            ViewData["Payload2Guid"] = new SelectList(_context.Payloads, "PayloadGuid", "Title", fleetDestroyer.Payload2Guid);
            ViewData["PointDefenseSystemGuid"] = new SelectList(_context.PointDefenseSystems, "PointDefenseSystemGuid", "Title", fleetDestroyer.PointDefenseSystemGuid);
            ViewData["ThrusterGuid"] = new SelectList(_context.Thrusters, "ThrusterGuid", "Title", fleetDestroyer.ThrusterGuid);
            ViewData["WeaponFrame1Guid"] = new SelectList(_context.WeaponFrames, "WeaponFrameGuid", "Title", fleetDestroyer.WeaponFrame1Guid);
            ViewData["WeaponFrame2Guid"] = new SelectList(_context.WeaponFrames, "WeaponFrameGuid", "Title", fleetDestroyer.WeaponFrame2Guid);
            return View(fleetDestroyer);
        }

        // GET: FleetDestroyers/Edit/5
        public async Task<IActionResult> Edit(Guid? id) {
            if (id == null || _context.FleetDestroyers == null) {
                return NotFound();
            }

            var fleetDestroyer = await _context.FleetDestroyers.FindAsync(id);
            if (fleetDestroyer == null) {
                return NotFound();
            }
            ViewData["ArmorGuid"] = new SelectList(_context.Armors, "ArmorGuid", "Title", fleetDestroyer.ArmorGuid);
            ViewData["DestroyerGuid"] = new SelectList(_context.Destroyers, "DestroyerGuid", "Class", fleetDestroyer.DestroyerGuid);
            ViewData["FleetGuid"] = new SelectList(_context.Fleets, "FleetGuid", "Name", fleetDestroyer.FleetGuid);
            ViewData["FuelSystemGuid"] = new SelectList(_context.FuelSystems, "FuelSystemGuid", "Title", fleetDestroyer.FuelSystemGuid);
            ViewData["Payload1Guid"] = new SelectList(_context.Payloads, "PayloadGuid", "Title", fleetDestroyer.Payload1Guid);
            ViewData["Payload2Guid"] = new SelectList(_context.Payloads, "PayloadGuid", "Title", fleetDestroyer.Payload2Guid);
            ViewData["PointDefenseSystemGuid"] = new SelectList(_context.PointDefenseSystems, "PointDefenseSystemGuid", "Title", fleetDestroyer.PointDefenseSystemGuid);
            ViewData["ThrusterGuid"] = new SelectList(_context.Thrusters, "ThrusterGuid", "Title", fleetDestroyer.ThrusterGuid);
            ViewData["WeaponFrame1Guid"] = new SelectList(_context.WeaponFrames, "WeaponFrameGuid", "Title", fleetDestroyer.WeaponFrame1Guid);
            ViewData["WeaponFrame2Guid"] = new SelectList(_context.WeaponFrames, "WeaponFrameGuid", "Title", fleetDestroyer.WeaponFrame2Guid);
            return View(fleetDestroyer);
        }

        // POST: FleetDestroyers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("FleetDestroyerGuid,FleetGuid,DestroyerGuid,WeaponFrame1Guid,Payload1Guid,WeaponFrame2Guid,Payload2Guid,ArmorGuid,PointDefenseSystemGuid,FuelSystemGuid,ThrusterGuid")] FleetDestroyer fleetDestroyer) {
            if (id != fleetDestroyer.FleetDestroyerGuid) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(fleetDestroyer);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!FleetDestroyerExists(fleetDestroyer.FleetDestroyerGuid)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArmorGuid"] = new SelectList(_context.Armors, "ArmorGuid", "Title", fleetDestroyer.ArmorGuid);
            ViewData["DestroyerGuid"] = new SelectList(_context.Destroyers, "DestroyerGuid", "Class", fleetDestroyer.DestroyerGuid);
            ViewData["FleetGuid"] = new SelectList(_context.Fleets, "FleetGuid", "Name", fleetDestroyer.FleetGuid);
            ViewData["FuelSystemGuid"] = new SelectList(_context.FuelSystems, "FuelSystemGuid", "Title", fleetDestroyer.FuelSystemGuid);
            ViewData["Payload1Guid"] = new SelectList(_context.Payloads, "PayloadGuid", "Title", fleetDestroyer.Payload1Guid);
            ViewData["Payload2Guid"] = new SelectList(_context.Payloads, "PayloadGuid", "Title", fleetDestroyer.Payload2Guid);
            ViewData["PointDefenseSystemGuid"] = new SelectList(_context.PointDefenseSystems, "PointDefenseSystemGuid", "Title", fleetDestroyer.PointDefenseSystemGuid);
            ViewData["ThrusterGuid"] = new SelectList(_context.Thrusters, "ThrusterGuid", "Title", fleetDestroyer.ThrusterGuid);
            ViewData["WeaponFrame1Guid"] = new SelectList(_context.WeaponFrames, "WeaponFrameGuid", "Title", fleetDestroyer.WeaponFrame1Guid);
            ViewData["WeaponFrame2Guid"] = new SelectList(_context.WeaponFrames, "WeaponFrameGuid", "Title", fleetDestroyer.WeaponFrame2Guid);
            return View(fleetDestroyer);
        }

        // GET: FleetDestroyers/Delete/5
        public async Task<IActionResult> Delete(Guid? id) {
            if (id == null || _context.FleetDestroyers == null) {
                return NotFound();
            }

            var fleetDestroyer = await _context.FleetDestroyers
                .Include(f => f.Armor)
                .Include(f => f.Destroyer)
                .Include(f => f.Fleet)
                .Include(f => f.FuelSystem)
                .Include(f => f.Payload1)
                .Include(f => f.Payload2)
                .Include(f => f.PointDefenseSystem)
                .Include(f => f.Thruster)
                .Include(f => f.WeaponFrame1)
                .Include(f => f.WeaponFrame2)
                .FirstOrDefaultAsync(m => m.FleetDestroyerGuid == id);
            if (fleetDestroyer == null) {
                return NotFound();
            }

            return View(fleetDestroyer);
        }

        // POST: FleetDestroyers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id) {
            if (_context.FleetDestroyers == null) {
                return Problem("Entity set 'CelestialFrontier_DevContext.FleetDestroyers'  is null.");
            }
            var fleetDestroyer = await _context.FleetDestroyers.FindAsync(id);
            if (fleetDestroyer != null) {
                _context.FleetDestroyers.Remove(fleetDestroyer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FleetDestroyerExists(Guid id) {
            return _context.FleetDestroyers.Any(e => e.FleetDestroyerGuid == id);
        }
    }
}
