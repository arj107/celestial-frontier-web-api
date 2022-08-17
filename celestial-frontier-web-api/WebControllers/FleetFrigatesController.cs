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
    public class FleetFrigatesController : Controller
    {
        private readonly CelestialFrontier_DevContext _context;

        public FleetFrigatesController(CelestialFrontier_DevContext context)
        {
            _context = context;
        }

        // GET: FleetFrigates
        public async Task<IActionResult> Index()
        {
            var celestialFrontier_DevContext = _context.FleetFrigates.Include(f => f.FleetCarrier).Include(f => f.FrigateConfiguration);
            return View(await celestialFrontier_DevContext.ToListAsync());
        }

        // GET: FleetFrigates/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.FleetFrigates == null)
            {
                return NotFound();
            }

            var fleetFrigate = await _context.FleetFrigates
                .Include(f => f.FleetCarrier)
                .Include(f => f.FrigateConfiguration)
                .FirstOrDefaultAsync(m => m.FleetFrigateGuid == id);
            if (fleetFrigate == null)
            {
                return NotFound();
            }

            return View(fleetFrigate);
        }

        // GET: FleetFrigates/Create
        public IActionResult Create()
        {
            ViewData["FleetCarrierGuid"] = new SelectList(_context.FleetCarriers, "FleetCarrierGuid", "FleetCarrierGuid");
            ViewData["FrigateConfigurationGuid"] = new SelectList(_context.FrigateConfigurations, "FrigateConfigurationGuid", "Name");
            return View();
        }

        // POST: FleetFrigates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FleetFrigateGuid,FrigateConfigurationGuid,FleetCarrierGuid")] FleetFrigate fleetFrigate)
        {
            if (ModelState.IsValid)
            {
                fleetFrigate.FleetFrigateGuid = Guid.NewGuid();
                _context.Add(fleetFrigate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FleetCarrierGuid"] = new SelectList(_context.FleetCarriers, "FleetCarrierGuid", "FleetCarrierGuid", fleetFrigate.FleetCarrierGuid);
            ViewData["FrigateConfigurationGuid"] = new SelectList(_context.FrigateConfigurations, "FrigateConfigurationGuid", "Name", fleetFrigate.FrigateConfigurationGuid);
            return View(fleetFrigate);
        }

        // GET: FleetFrigates/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.FleetFrigates == null)
            {
                return NotFound();
            }

            var fleetFrigate = await _context.FleetFrigates.FindAsync(id);
            if (fleetFrigate == null)
            {
                return NotFound();
            }
            ViewData["FleetCarrierGuid"] = new SelectList(_context.FleetCarriers, "FleetCarrierGuid", "FleetCarrierGuid", fleetFrigate.FleetCarrierGuid);
            ViewData["FrigateConfigurationGuid"] = new SelectList(_context.FrigateConfigurations, "FrigateConfigurationGuid", "Name", fleetFrigate.FrigateConfigurationGuid);
            return View(fleetFrigate);
        }

        // POST: FleetFrigates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("FleetFrigateGuid,FrigateConfigurationGuid,FleetCarrierGuid")] FleetFrigate fleetFrigate)
        {
            if (id != fleetFrigate.FleetFrigateGuid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fleetFrigate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FleetFrigateExists(fleetFrigate.FleetFrigateGuid))
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
            ViewData["FleetCarrierGuid"] = new SelectList(_context.FleetCarriers, "FleetCarrierGuid", "FleetCarrierGuid", fleetFrigate.FleetCarrierGuid);
            ViewData["FrigateConfigurationGuid"] = new SelectList(_context.FrigateConfigurations, "FrigateConfigurationGuid", "Name", fleetFrigate.FrigateConfigurationGuid);
            return View(fleetFrigate);
        }

        // GET: FleetFrigates/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.FleetFrigates == null)
            {
                return NotFound();
            }

            var fleetFrigate = await _context.FleetFrigates
                .Include(f => f.FleetCarrier)
                .Include(f => f.FrigateConfiguration)
                .FirstOrDefaultAsync(m => m.FleetFrigateGuid == id);
            if (fleetFrigate == null)
            {
                return NotFound();
            }

            return View(fleetFrigate);
        }

        // POST: FleetFrigates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.FleetFrigates == null)
            {
                return Problem("Entity set 'CelestialFrontier_DevContext.FleetFrigates'  is null.");
            }
            var fleetFrigate = await _context.FleetFrigates.FindAsync(id);
            if (fleetFrigate != null)
            {
                _context.FleetFrigates.Remove(fleetFrigate);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FleetFrigateExists(Guid id)
        {
          return _context.FleetFrigates.Any(e => e.FleetFrigateGuid == id);
        }
    }
}
