using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using celestial_frontier_web_api.Models;

namespace celestial_frontier_web_api.WebControllers
{
    public class FleetBattlecruisersController : Controller
    {
        private readonly CelestialFrontier_DevContext _context;

        public FleetBattlecruisersController(CelestialFrontier_DevContext context)
        {
            _context = context;
        }

        // GET: FleetBattlecruisers
        public async Task<IActionResult> Index()
        {
            var celestialFrontier_DevContext = _context.FleetBattlecruisers.Include(f => f.BattlecruiserConfiguration).Include(f => f.Fleet);
            return View(await celestialFrontier_DevContext.ToListAsync());
        }

        // GET: FleetBattlecruisers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.FleetBattlecruisers == null)
            {
                return NotFound();
            }

            var fleetBattlecruiser = await _context.FleetBattlecruisers
                .Include(f => f.BattlecruiserConfiguration)
                .Include(f => f.Fleet)
                .FirstOrDefaultAsync(m => m.FleetBattlecruiserGuid == id);
            if (fleetBattlecruiser == null)
            {
                return NotFound();
            }

            return View(fleetBattlecruiser);
        }

        // GET: FleetBattlecruisers/Create
        public IActionResult Create()
        {
            ViewData["BattlecruiserConfigurationGuid"] = new SelectList(_context.BattlecruiserConfigurations, "BattlecruiserConfigurationGuid", "Name");
            ViewData["FleetGuid"] = new SelectList(_context.Fleets, "FleetGuid", "Name");
            return View();
        }

        // POST: FleetBattlecruisers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FleetBattlecruiserGuid,BattlecruiserConfigurationGuid,FleetGuid")] FleetBattlecruiser fleetBattlecruiser)
        {
            if (ModelState.IsValid)
            {
                fleetBattlecruiser.FleetBattlecruiserGuid = Guid.NewGuid();
                _context.Add(fleetBattlecruiser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BattlecruiserConfigurationGuid"] = new SelectList(_context.BattlecruiserConfigurations, "BattlecruiserConfigurationGuid", "Name", fleetBattlecruiser.BattlecruiserConfigurationGuid);
            ViewData["FleetGuid"] = new SelectList(_context.Fleets, "FleetGuid", "Name", fleetBattlecruiser.FleetGuid);
            return View(fleetBattlecruiser);
        }

        // GET: FleetBattlecruisers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.FleetBattlecruisers == null)
            {
                return NotFound();
            }

            var fleetBattlecruiser = await _context.FleetBattlecruisers.FindAsync(id);
            if (fleetBattlecruiser == null)
            {
                return NotFound();
            }
            ViewData["BattlecruiserConfigurationGuid"] = new SelectList(_context.BattlecruiserConfigurations, "BattlecruiserConfigurationGuid", "Name", fleetBattlecruiser.BattlecruiserConfigurationGuid);
            ViewData["FleetGuid"] = new SelectList(_context.Fleets, "FleetGuid", "Name", fleetBattlecruiser.FleetGuid);
            return View(fleetBattlecruiser);
        }

        // POST: FleetBattlecruisers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("FleetBattlecruiserGuid,BattlecruiserConfigurationGuid,FleetGuid")] FleetBattlecruiser fleetBattlecruiser)
        {
            if (id != fleetBattlecruiser.FleetBattlecruiserGuid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fleetBattlecruiser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FleetBattlecruiserExists(fleetBattlecruiser.FleetBattlecruiserGuid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BattlecruiserConfigurationGuid"] = new SelectList(_context.BattlecruiserConfigurations, "BattlecruiserConfigurationGuid", "Name", fleetBattlecruiser.BattlecruiserConfigurationGuid);
            ViewData["FleetGuid"] = new SelectList(_context.Fleets, "FleetGuid", "Name", fleetBattlecruiser.FleetGuid);
            return View(fleetBattlecruiser);
        }

        // GET: FleetBattlecruisers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.FleetBattlecruisers == null)
            {
                return NotFound();
            }

            var fleetBattlecruiser = await _context.FleetBattlecruisers
                .Include(f => f.BattlecruiserConfiguration)
                .Include(f => f.Fleet)
                .FirstOrDefaultAsync(m => m.FleetBattlecruiserGuid == id);
            if (fleetBattlecruiser == null)
            {
                return NotFound();
            }

            return View(fleetBattlecruiser);
        }

        // POST: FleetBattlecruisers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.FleetBattlecruisers == null)
            {
                return Problem("Entity set 'CelestialFrontier_DevContext.FleetBattlecruisers'  is null.");
            }
            var fleetBattlecruiser = await _context.FleetBattlecruisers.FindAsync(id);
            if (fleetBattlecruiser != null)
            {
                _context.FleetBattlecruisers.Remove(fleetBattlecruiser);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FleetBattlecruiserExists(Guid id)
        {
          return _context.FleetBattlecruisers.Any(e => e.FleetBattlecruiserGuid == id);
        }
    }
}
