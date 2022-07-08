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
    public class FleetCarriersController : Controller
    {
        private readonly CelestialFrontier_DevContext _context;

        public FleetCarriersController(CelestialFrontier_DevContext context)
        {
            _context = context;
        }

        // GET: FleetCarriers
        public async Task<IActionResult> Index()
        {
            var celestialFrontier_DevContext = _context.FleetCarriers.Include(f => f.CarrierConfiguration).Include(f => f.Fleet);
            return View(await celestialFrontier_DevContext.ToListAsync());
        }

        // GET: FleetCarriers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.FleetCarriers == null)
            {
                return NotFound();
            }

            var fleetCarrier = await _context.FleetCarriers
                .Include(f => f.CarrierConfiguration)
                .Include(f => f.Fleet)
                .FirstOrDefaultAsync(m => m.FleetCarrierGuid == id);
            if (fleetCarrier == null)
            {
                return NotFound();
            }

            return View(fleetCarrier);
        }

        // GET: FleetCarriers/Create
        public IActionResult Create()
        {
            ViewData["CarrierConfigurationGuid"] = new SelectList(_context.CarrierConfigurations, "CarrierConfigurationGuid", "Name");
            ViewData["FleetGuid"] = new SelectList(_context.Fleets, "FleetGuid", "Name");
            return View();
        }

        // POST: FleetCarriers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FleetCarrierGuid,CarrierConfigurationGuid,FleetGuid")] FleetCarrier fleetCarrier)
        {
            if (ModelState.IsValid)
            {
                fleetCarrier.FleetCarrierGuid = Guid.NewGuid();
                _context.Add(fleetCarrier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarrierConfigurationGuid"] = new SelectList(_context.CarrierConfigurations, "CarrierConfigurationGuid", "Name", fleetCarrier.CarrierConfigurationGuid);
            ViewData["FleetGuid"] = new SelectList(_context.Fleets, "FleetGuid", "Name", fleetCarrier.FleetGuid);
            return View(fleetCarrier);
        }

        // GET: FleetCarriers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.FleetCarriers == null)
            {
                return NotFound();
            }

            var fleetCarrier = await _context.FleetCarriers.FindAsync(id);
            if (fleetCarrier == null)
            {
                return NotFound();
            }
            ViewData["CarrierConfigurationGuid"] = new SelectList(_context.CarrierConfigurations, "CarrierConfigurationGuid", "Name", fleetCarrier.CarrierConfigurationGuid);
            ViewData["FleetGuid"] = new SelectList(_context.Fleets, "FleetGuid", "Name", fleetCarrier.FleetGuid);
            return View(fleetCarrier);
        }

        // POST: FleetCarriers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("FleetCarrierGuid,CarrierConfigurationGuid,FleetGuid")] FleetCarrier fleetCarrier)
        {
            if (id != fleetCarrier.FleetCarrierGuid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fleetCarrier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FleetCarrierExists(fleetCarrier.FleetCarrierGuid))
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
            ViewData["CarrierConfigurationGuid"] = new SelectList(_context.CarrierConfigurations, "CarrierConfigurationGuid", "Name", fleetCarrier.CarrierConfigurationGuid);
            ViewData["FleetGuid"] = new SelectList(_context.Fleets, "FleetGuid", "Name", fleetCarrier.FleetGuid);
            return View(fleetCarrier);
        }

        // GET: FleetCarriers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.FleetCarriers == null)
            {
                return NotFound();
            }

            var fleetCarrier = await _context.FleetCarriers
                .Include(f => f.CarrierConfiguration)
                .Include(f => f.Fleet)
                .FirstOrDefaultAsync(m => m.FleetCarrierGuid == id);
            if (fleetCarrier == null)
            {
                return NotFound();
            }

            return View(fleetCarrier);
        }

        // POST: FleetCarriers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.FleetCarriers == null)
            {
                return Problem("Entity set 'CelestialFrontier_DevContext.FleetCarriers'  is null.");
            }
            var fleetCarrier = await _context.FleetCarriers.FindAsync(id);
            if (fleetCarrier != null)
            {
                _context.FleetCarriers.Remove(fleetCarrier);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FleetCarrierExists(Guid id)
        {
          return _context.FleetCarriers.Any(e => e.FleetCarrierGuid == id);
        }
    }
}
