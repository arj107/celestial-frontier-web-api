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
    public class FleetCorvettesController : Controller
    {
        private readonly CelestialFrontier_DevContext _context;

        public FleetCorvettesController(CelestialFrontier_DevContext context)
        {
            _context = context;
        }

        // GET: FleetCorvettes
        public async Task<IActionResult> Index()
        {
            var celestialFrontier_DevContext = _context.FleetCorvettes.Include(f => f.CorvetteConfiguration).Include(f => f.FleetCarrier);
            return View(await celestialFrontier_DevContext.ToListAsync());
        }

        // GET: FleetCorvettes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.FleetCorvettes == null)
            {
                return NotFound();
            }

            var fleetCorvette = await _context.FleetCorvettes
                .Include(f => f.CorvetteConfiguration)
                .Include(f => f.FleetCarrier)
                .FirstOrDefaultAsync(m => m.FleetCorvetteGuid == id);
            if (fleetCorvette == null)
            {
                return NotFound();
            }

            return View(fleetCorvette);
        }

        // GET: FleetCorvettes/Create
        public IActionResult Create()
        {
            ViewData["CorvetteConfigurationGuid"] = new SelectList(_context.CorvetteConfigurations, "CorvetteConfigurationGuid", "Name");
            ViewData["FleetCarrierGuid"] = new SelectList(_context.FleetCarriers, "FleetCarrierGuid", "FleetCarrierGuid");
            return View();
        }

        // POST: FleetCorvettes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FleetCorvetteGuid,CorvetteConfigurationGuid,FleetCarrierGuid")] FleetCorvette fleetCorvette)
        {
            if (ModelState.IsValid)
            {
                fleetCorvette.FleetCorvetteGuid = Guid.NewGuid();
                _context.Add(fleetCorvette);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CorvetteConfigurationGuid"] = new SelectList(_context.CorvetteConfigurations, "CorvetteConfigurationGuid", "Name", fleetCorvette.CorvetteConfigurationGuid);
            ViewData["FleetCarrierGuid"] = new SelectList(_context.FleetCarriers, "FleetCarrierGuid", "FleetCarrierGuid", fleetCorvette.FleetCarrierGuid);
            return View(fleetCorvette);
        }

        // GET: FleetCorvettes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.FleetCorvettes == null)
            {
                return NotFound();
            }

            var fleetCorvette = await _context.FleetCorvettes.FindAsync(id);
            if (fleetCorvette == null)
            {
                return NotFound();
            }
            ViewData["CorvetteConfigurationGuid"] = new SelectList(_context.CorvetteConfigurations, "CorvetteConfigurationGuid", "Name", fleetCorvette.CorvetteConfigurationGuid);
            ViewData["FleetCarrierGuid"] = new SelectList(_context.FleetCarriers, "FleetCarrierGuid", "FleetCarrierGuid", fleetCorvette.FleetCarrierGuid);
            return View(fleetCorvette);
        }

        // POST: FleetCorvettes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("FleetCorvetteGuid,CorvetteConfigurationGuid,FleetCarrierGuid")] FleetCorvette fleetCorvette)
        {
            if (id != fleetCorvette.FleetCorvetteGuid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fleetCorvette);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FleetCorvetteExists(fleetCorvette.FleetCorvetteGuid))
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
            ViewData["CorvetteConfigurationGuid"] = new SelectList(_context.CorvetteConfigurations, "CorvetteConfigurationGuid", "Name", fleetCorvette.CorvetteConfigurationGuid);
            ViewData["FleetCarrierGuid"] = new SelectList(_context.FleetCarriers, "FleetCarrierGuid", "FleetCarrierGuid", fleetCorvette.FleetCarrierGuid);
            return View(fleetCorvette);
        }

        // GET: FleetCorvettes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.FleetCorvettes == null)
            {
                return NotFound();
            }

            var fleetCorvette = await _context.FleetCorvettes
                .Include(f => f.CorvetteConfiguration)
                .Include(f => f.FleetCarrier)
                .FirstOrDefaultAsync(m => m.FleetCorvetteGuid == id);
            if (fleetCorvette == null)
            {
                return NotFound();
            }

            return View(fleetCorvette);
        }

        // POST: FleetCorvettes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.FleetCorvettes == null)
            {
                return Problem("Entity set 'CelestialFrontier_DevContext.FleetCorvettes'  is null.");
            }
            var fleetCorvette = await _context.FleetCorvettes.FindAsync(id);
            if (fleetCorvette != null)
            {
                _context.FleetCorvettes.Remove(fleetCorvette);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FleetCorvetteExists(Guid id)
        {
          return _context.FleetCorvettes.Any(e => e.FleetCorvetteGuid == id);
        }
    }
}
