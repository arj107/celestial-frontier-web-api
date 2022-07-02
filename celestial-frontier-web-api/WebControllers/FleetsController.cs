using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using celestial_frontier_web_api.Models;

namespace celestial_frontier_web_api.WebControllers {
    public class FleetsController : Controller {
        private readonly CelestialFrontier_DevContext _context;

        public FleetsController(CelestialFrontier_DevContext context) {
            _context = context;
        }

        // GET: Fleets
        public async Task<IActionResult> Index() {
            var celestialFrontierContext = _context.Fleets.Include(a => a.User);
            return View(await celestialFrontierContext.ToListAsync());
        }

        // GET: Fleets/Details/5
        public async Task<IActionResult> Details(Guid? id) {
            if (id == null || _context.Fleets == null) {
                return NotFound();
            }

            var fleet = await _context.Fleets
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.FleetGuid == id);
            if (fleet == null) {
                return NotFound();
            }

            return View(fleet);
        }

        // GET: Fleets/Create
        public IActionResult Create() {
            ViewData["UserGuid"] = new SelectList(_context.Users, "UserGuid", "Username");
            return View();
        }

        // POST: Fleets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FleetGuid,UserGuid,Name,CrewCapacity")] Fleet fleet) {
            if (ModelState.IsValid) {
                fleet.FleetGuid = Guid.NewGuid();
                _context.Add(fleet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserGuid"] = new SelectList(_context.Users, "UserGuid", "Username");
            return View(fleet);
        }

        // GET: Fleets/Edit/5
        public async Task<IActionResult> Edit(Guid? id) {
            if (id == null || _context.Fleets == null) {
                return NotFound();
            }

            var fleet = await _context.Fleets.FindAsync(id);
            if (fleet == null) {
                return NotFound();
            }
            ViewData["UserGuid"] = new SelectList(_context.Users, "UserGuid", "Username");
            return View(fleet);
        }

        // POST: Fleets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("FleetGuid,UserGuid,Name,CrewCapacity")] Fleet fleet) {
            if (id != fleet.FleetGuid) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(fleet);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!FleetExists(fleet.FleetGuid)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserGuid"] = new SelectList(_context.Users, "UserGuid", "Username");
            return View(fleet);
        }

        // GET: Fleets/Delete/5
        public async Task<IActionResult> Delete(Guid? id) {
            if (id == null || _context.Fleets == null) {
                return NotFound();
            }

            var fleet = await _context.Fleets
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.FleetGuid == id);
            if (fleet == null) {
                return NotFound();
            }

            return View(fleet);
        }

        // POST: Fleets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id) {
            if (_context.Fleets == null) {
                return Problem("Entity set 'CelestialFrontier_DevContext.Fleets'  is null.");
            }
            var fleet = await _context.Fleets.FindAsync(id);
            if (fleet != null) {
                _context.Fleets.Remove(fleet);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FleetExists(Guid id) {
            return _context.Fleets.Any(e => e.FleetGuid == id);
        }
    }
}
