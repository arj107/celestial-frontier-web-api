using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using celestial_frontier_web_api.Models;

namespace celestial_frontier_web_api.WebControllers {
    public class FleetFightersController : Controller {
        private readonly CelestialFrontier_DevContext _context;

        public FleetFightersController(CelestialFrontier_DevContext context) {
            _context = context;
        }

        // GET: FleetFighters
        public async Task<IActionResult> Index() {
            var celestialFrontier_DevContext = _context.FleetFighters.Include(f => f.Armor).Include(f => f.Fighter).Include(f => f.Fleet).Include(f => f.FuelSystem).Include(f => f.Payload).Include(f => f.PointDefenseSystem).Include(f => f.Thruster).Include(f => f.WeaponFrame);
            return View(await celestialFrontier_DevContext.ToListAsync());
        }

        // GET: FleetFighters/Details/5
        public async Task<IActionResult> Details(Guid? id) {
            if (id == null || _context.FleetFighters == null) {
                return NotFound();
            }

            var fleetFighter = await _context.FleetFighters
                .Include(f => f.Armor)
                .Include(f => f.Fighter)
                .Include(f => f.Fleet)
                .Include(f => f.FuelSystem)
                .Include(f => f.Payload)
                .Include(f => f.PointDefenseSystem)
                .Include(f => f.Thruster)
                .Include(f => f.WeaponFrame)
                .FirstOrDefaultAsync(m => m.FleetFighterGuid == id);
            if (fleetFighter == null) {
                return NotFound();
            }

            return View(fleetFighter);
        }

        // GET: FleetFighters/Create
        public IActionResult Create() {
            ViewData["ArmorGuid"] = new SelectList(_context.Armors, "ArmorGuid", "Title");
            ViewData["FighterGuid"] = new SelectList(_context.Fighters, "FighterGuid", "Class");
            ViewData["FleetGuid"] = new SelectList(_context.Fleets, "FleetGuid", "Name");
            ViewData["FuelSystemGuid"] = new SelectList(_context.FuelSystems, "FuelSystemGuid", "Title");
            ViewData["PayloadGuid"] = new SelectList(_context.Payloads, "PayloadGuid", "Title");
            ViewData["PointDefenseSystemGuid"] = new SelectList(_context.PointDefenseSystems, "PointDefenseSystemGuid", "Title");
            ViewData["ThrusterGuid"] = new SelectList(_context.Thrusters, "ThrusterGuid", "Title");
            ViewData["WeaponFrameGuid"] = new SelectList(_context.WeaponFrames, "WeaponFrameGuid", "Title");
            return View();
        }

        // POST: FleetFighters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FleetFighterGuid,FleetGuid,FighterGuid,WeaponFrameGuid,PayloadGuid,ArmorGuid,PointDefenseSystemGuid,FuelSystemGuid,ThrusterGuid")] FleetFighter fleetFighter) {
            if (ModelState.IsValid) {
                fleetFighter.FleetFighterGuid = Guid.NewGuid();
                _context.Add(fleetFighter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArmorGuid"] = new SelectList(_context.Armors, "ArmorGuid", "Title", fleetFighter.ArmorGuid);
            ViewData["FighterGuid"] = new SelectList(_context.Fighters, "FighterGuid", "Class", fleetFighter.FighterGuid);
            ViewData["FleetGuid"] = new SelectList(_context.Fleets, "FleetGuid", "Name", fleetFighter.FleetGuid);
            ViewData["FuelSystemGuid"] = new SelectList(_context.FuelSystems, "FuelSystemGuid", "Title", fleetFighter.FuelSystemGuid);
            ViewData["PayloadGuid"] = new SelectList(_context.Payloads, "PayloadGuid", "Title", fleetFighter.PayloadGuid);
            ViewData["PointDefenseSystemGuid"] = new SelectList(_context.PointDefenseSystems, "PointDefenseSystemGuid", "Title", fleetFighter.PointDefenseSystemGuid);
            ViewData["ThrusterGuid"] = new SelectList(_context.Thrusters, "ThrusterGuid", "Title", fleetFighter.ThrusterGuid);
            ViewData["WeaponFrameGuid"] = new SelectList(_context.WeaponFrames, "WeaponFrameGuid", "Title", fleetFighter.WeaponFrameGuid);
            return View(fleetFighter);
        }

        // GET: FleetFighters/Edit/5
        public async Task<IActionResult> Edit(Guid? id) {
            if (id == null || _context.FleetFighters == null) {
                return NotFound();
            }

            var fleetFighter = await _context.FleetFighters.FindAsync(id);
            if (fleetFighter == null) {
                return NotFound();
            }
            ViewData["ArmorGuid"] = new SelectList(_context.Armors, "ArmorGuid", "Title", fleetFighter.ArmorGuid);
            ViewData["FighterGuid"] = new SelectList(_context.Fighters, "FighterGuid", "Class", fleetFighter.FighterGuid);
            ViewData["FleetGuid"] = new SelectList(_context.Fleets, "FleetGuid", "Name", fleetFighter.FleetGuid);
            ViewData["FuelSystemGuid"] = new SelectList(_context.FuelSystems, "FuelSystemGuid", "Title", fleetFighter.FuelSystemGuid);
            ViewData["PayloadGuid"] = new SelectList(_context.Payloads, "PayloadGuid", "Title", fleetFighter.PayloadGuid);
            ViewData["PointDefenseSystemGuid"] = new SelectList(_context.PointDefenseSystems, "PointDefenseSystemGuid", "Title", fleetFighter.PointDefenseSystemGuid);
            ViewData["ThrusterGuid"] = new SelectList(_context.Thrusters, "ThrusterGuid", "Title", fleetFighter.ThrusterGuid);
            ViewData["WeaponFrameGuid"] = new SelectList(_context.WeaponFrames, "WeaponFrameGuid", "Title", fleetFighter.WeaponFrameGuid);
            return View(fleetFighter);
        }

        // POST: FleetFighters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("FleetFighterGuid,FleetGuid,FighterGuid,WeaponFrameGuid,PayloadGuid,ArmorGuid,PointDefenseSystemGuid,FuelSystemGuid,ThrusterGuid")] FleetFighter fleetFighter) {
            if (id != fleetFighter.FleetFighterGuid) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(fleetFighter);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!FleetFighterExists(fleetFighter.FleetFighterGuid)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArmorGuid"] = new SelectList(_context.Armors, "ArmorGuid", "Title", fleetFighter.ArmorGuid);
            ViewData["FighterGuid"] = new SelectList(_context.Fighters, "FighterGuid", "Class", fleetFighter.FighterGuid);
            ViewData["FleetGuid"] = new SelectList(_context.Fleets, "FleetGuid", "Name", fleetFighter.FleetGuid);
            ViewData["FuelSystemGuid"] = new SelectList(_context.FuelSystems, "FuelSystemGuid", "Title", fleetFighter.FuelSystemGuid);
            ViewData["PayloadGuid"] = new SelectList(_context.Payloads, "PayloadGuid", "Title", fleetFighter.PayloadGuid);
            ViewData["PointDefenseSystemGuid"] = new SelectList(_context.PointDefenseSystems, "PointDefenseSystemGuid", "Title", fleetFighter.PointDefenseSystemGuid);
            ViewData["ThrusterGuid"] = new SelectList(_context.Thrusters, "ThrusterGuid", "Title", fleetFighter.ThrusterGuid);
            ViewData["WeaponFrameGuid"] = new SelectList(_context.WeaponFrames, "WeaponFrameGuid", "Title", fleetFighter.WeaponFrameGuid);
            return View(fleetFighter);
        }

        // GET: FleetFighters/Delete/5
        public async Task<IActionResult> Delete(Guid? id) {
            if (id == null || _context.FleetFighters == null) {
                return NotFound();
            }

            var fleetFighter = await _context.FleetFighters
                .Include(f => f.Armor)
                .Include(f => f.Fighter)
                .Include(f => f.Fleet)
                .Include(f => f.FuelSystem)
                .Include(f => f.Payload)
                .Include(f => f.PointDefenseSystem)
                .Include(f => f.Thruster)
                .Include(f => f.WeaponFrame)
                .FirstOrDefaultAsync(m => m.FleetFighterGuid == id);
            if (fleetFighter == null) {
                return NotFound();
            }

            return View(fleetFighter);
        }

        // POST: FleetFighters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id) {
            if (_context.FleetFighters == null) {
                return Problem("Entity set 'CelestialFrontier_DevContext.FleetFighters'  is null.");
            }
            var fleetFighter = await _context.FleetFighters.FindAsync(id);
            if (fleetFighter != null) {
                _context.FleetFighters.Remove(fleetFighter);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FleetFighterExists(Guid id) {
            return _context.FleetFighters.Any(e => e.FleetFighterGuid == id);
        }
    }
}
