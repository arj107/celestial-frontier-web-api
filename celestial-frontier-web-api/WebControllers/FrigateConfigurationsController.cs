using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using celestial_frontier_web_api.Models;

namespace celestial_frontier_web_api.WebControllers {
    public class FrigateConfigurationsController : Controller {
        private readonly CelestialFrontier_DevContext _context;

        public FrigateConfigurationsController(CelestialFrontier_DevContext context) {
            _context = context;
        }

        // GET: FrigateConfigurations
        public async Task<IActionResult> Index() {
            var celestialFrontier_DevContext = _context.FrigateConfigurations.Include(f => f.Armor).Include(f => f.User).Include(f => f.Frigate).Include(f => f.FuelSystem).Include(f => f.Payload).Include(f => f.PointDefenseSystem).Include(f => f.Thruster).Include(f => f.WeaponFrame);
            return View(await celestialFrontier_DevContext.ToListAsync());
        }

        // GET: FrigateConfigurations/Details/5
        public async Task<IActionResult> Details(Guid? id) {
            if (id == null || _context.FrigateConfigurations == null) {
                return NotFound();
            }

            var frigateConfiguration = await _context.FrigateConfigurations
                .Include(f => f.Armor)
                .Include(f => f.User)
                .Include(f => f.Frigate)
                .Include(f => f.FuelSystem)
                .Include(f => f.Payload)
                .Include(f => f.PointDefenseSystem)
                .Include(f => f.Thruster)
                .Include(f => f.WeaponFrame)
                .FirstOrDefaultAsync(m => m.FrigateConfigurationGuid == id);
            if (frigateConfiguration == null) {
                return NotFound();
            }

            return View(frigateConfiguration);
        }

        // GET: FrigateConfigurations/Create
        public IActionResult Create() {
            ViewData["ArmorGuid"] = new SelectList(_context.Armors, "ArmorGuid", "Title");
            ViewData["UserGuid"] = new SelectList(_context.Users, "UserGuid", "Username");
            ViewData["FrigateGuid"] = new SelectList(_context.Frigates, "FrigateGuid", "Class");
            ViewData["FuelSystemGuid"] = new SelectList(_context.FuelSystems, "FuelSystemGuid", "Title");
            ViewData["PayloadGuid"] = new SelectList(_context.Payloads, "PayloadGuid", "Title");
            ViewData["PointDefenseSystemGuid"] = new SelectList(_context.PointDefenseSystems, "PointDefenseSystemGuid", "Title");
            ViewData["ThrusterGuid"] = new SelectList(_context.Thrusters, "ThrusterGuid", "Title");
            ViewData["WeaponFrameGuid"] = new SelectList(_context.WeaponFrames, "WeaponFrameGuid", "Title");
            return View();
        }

        // POST: FrigateConfigurations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FrigateConfigurationGuid,UserGuid,FrigateGuid,WeaponFrameGuid,PayloadGuid,ArmorGuid,PointDefenseSystemGuid,FuelSystemGuid,ThrusterGuid")] FrigateConfiguration frigateConfiguration) {
            if (ModelState.IsValid) {
                frigateConfiguration.FrigateConfigurationGuid = Guid.NewGuid();
                _context.Add(frigateConfiguration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArmorGuid"] = new SelectList(_context.Armors, "ArmorGuid", "Title", frigateConfiguration.ArmorGuid);
            ViewData["UserGuid"] = new SelectList(_context.Users, "UserGuid", "Username", frigateConfiguration.UserGuid);
            ViewData["FrigateGuid"] = new SelectList(_context.Frigates, "FrigateGuid", "Class", frigateConfiguration.FrigateGuid);
            ViewData["FuelSystemGuid"] = new SelectList(_context.FuelSystems, "FuelSystemGuid", "Title", frigateConfiguration.FuelSystemGuid);
            ViewData["PayloadGuid"] = new SelectList(_context.Payloads, "PayloadGuid", "Title", frigateConfiguration.PayloadGuid);
            ViewData["PointDefenseSystemGuid"] = new SelectList(_context.PointDefenseSystems, "PointDefenseSystemGuid", "Title", frigateConfiguration.PointDefenseSystemGuid);
            ViewData["ThrusterGuid"] = new SelectList(_context.Thrusters, "ThrusterGuid", "Title", frigateConfiguration.ThrusterGuid);
            ViewData["WeaponFrameGuid"] = new SelectList(_context.WeaponFrames, "WeaponFrameGuid", "Title", frigateConfiguration.WeaponFrameGuid);
            return View(frigateConfiguration);
        }

        // GET: FrigateConfigurations/Edit/5
        public async Task<IActionResult> Edit(Guid? id) {
            if (id == null || _context.FrigateConfigurations == null) {
                return NotFound();
            }

            var frigateConfiguration = await _context.FrigateConfigurations.FindAsync(id);
            if (frigateConfiguration == null) {
                return NotFound();
            }
            ViewData["ArmorGuid"] = new SelectList(_context.Armors, "ArmorGuid", "Title", frigateConfiguration.ArmorGuid);
            ViewData["UserGuid"] = new SelectList(_context.Users, "UserGuid", "Username", frigateConfiguration.UserGuid);
            ViewData["FrigateGuid"] = new SelectList(_context.Frigates, "FrigateGuid", "Class", frigateConfiguration.FrigateGuid);
            ViewData["FuelSystemGuid"] = new SelectList(_context.FuelSystems, "FuelSystemGuid", "Title", frigateConfiguration.FuelSystemGuid);
            ViewData["PayloadGuid"] = new SelectList(_context.Payloads, "PayloadGuid", "Title", frigateConfiguration.PayloadGuid);
            ViewData["PointDefenseSystemGuid"] = new SelectList(_context.PointDefenseSystems, "PointDefenseSystemGuid", "Title", frigateConfiguration.PointDefenseSystemGuid);
            ViewData["ThrusterGuid"] = new SelectList(_context.Thrusters, "ThrusterGuid", "Title", frigateConfiguration.ThrusterGuid);
            ViewData["WeaponFrameGuid"] = new SelectList(_context.WeaponFrames, "WeaponFrameGuid", "Title", frigateConfiguration.WeaponFrameGuid);
            return View(frigateConfiguration);
        }

        // POST: FrigateConfigurations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("FrigateConfigurationGuid,UserGuid,FrigateGuid,WeaponFrameGuid,PayloadGuid,ArmorGuid,PointDefenseSystemGuid,FuelSystemGuid,ThrusterGuid")] FrigateConfiguration frigateConfiguration) {
            if (id != frigateConfiguration.FrigateConfigurationGuid) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(frigateConfiguration);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!FrigateConfigurationExists(frigateConfiguration.FrigateConfigurationGuid)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArmorGuid"] = new SelectList(_context.Armors, "ArmorGuid", "Title", frigateConfiguration.ArmorGuid);
            ViewData["UserGuid"] = new SelectList(_context.Users, "UserGuid", "Username", frigateConfiguration.UserGuid);
            ViewData["FrigateGuid"] = new SelectList(_context.Frigates, "FrigateGuid", "Class", frigateConfiguration.FrigateGuid);
            ViewData["FuelSystemGuid"] = new SelectList(_context.FuelSystems, "FuelSystemGuid", "Title", frigateConfiguration.FuelSystemGuid);
            ViewData["PayloadGuid"] = new SelectList(_context.Payloads, "PayloadGuid", "Title", frigateConfiguration.PayloadGuid);
            ViewData["PointDefenseSystemGuid"] = new SelectList(_context.PointDefenseSystems, "PointDefenseSystemGuid", "Title", frigateConfiguration.PointDefenseSystemGuid);
            ViewData["ThrusterGuid"] = new SelectList(_context.Thrusters, "ThrusterGuid", "Title", frigateConfiguration.ThrusterGuid);
            ViewData["WeaponFrameGuid"] = new SelectList(_context.WeaponFrames, "WeaponFrameGuid", "Title", frigateConfiguration.WeaponFrameGuid);
            return View(frigateConfiguration);
        }

        // GET: FrigateConfigurations/Delete/5
        public async Task<IActionResult> Delete(Guid? id) {
            if (id == null || _context.FrigateConfigurations == null) {
                return NotFound();
            }

            var frigateConfiguration = await _context.FrigateConfigurations
                .Include(f => f.Armor)
                .Include(f => f.User)
                .Include(f => f.Frigate)
                .Include(f => f.FuelSystem)
                .Include(f => f.Payload)
                .Include(f => f.PointDefenseSystem)
                .Include(f => f.Thruster)
                .Include(f => f.WeaponFrame)
                .FirstOrDefaultAsync(m => m.FrigateConfigurationGuid == id);
            if (frigateConfiguration == null) {
                return NotFound();
            }

            return View(frigateConfiguration);
        }

        // POST: FrigateConfigurations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id) {
            if (_context.FrigateConfigurations == null) {
                return Problem("Entity set 'CelestialFrontier_DevContext.FrigateConfigurations'  is null.");
            }
            var frigateConfiguration = await _context.FrigateConfigurations.FindAsync(id);
            if (frigateConfiguration != null) {
                _context.FrigateConfigurations.Remove(frigateConfiguration);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FrigateConfigurationExists(Guid id) {
            return _context.FrigateConfigurations.Any(e => e.FrigateConfigurationGuid == id);
        }
    }
}
