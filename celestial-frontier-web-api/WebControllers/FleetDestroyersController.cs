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
    public class FleetDestroyersController : Controller
    {
        private readonly CelestialFrontier_DevContext _context;

        public FleetDestroyersController(CelestialFrontier_DevContext context)
        {
            _context = context;
        }

        // GET: FleetDestroyers
        public async Task<IActionResult> Index()
        {
            var celestialFrontier_DevContext = _context.FleetDestroyers.Include(f => f.DestroyerConfiguration).Include(f => f.FleetCarrier);
            return View(await celestialFrontier_DevContext.ToListAsync());
        }

        // GET: FleetDestroyers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.FleetDestroyers == null)
            {
                return NotFound();
            }

            var fleetDestroyer = await _context.FleetDestroyers
                .Include(f => f.DestroyerConfiguration)
                .Include(f => f.FleetCarrier)
                .FirstOrDefaultAsync(m => m.FleetDestroyerGuid == id);
            if (fleetDestroyer == null)
            {
                return NotFound();
            }

            return View(fleetDestroyer);
        }

        // GET: FleetDestroyers/Create
        public IActionResult Create()
        {
            ViewData["DestroyerConfigurationGuid"] = new SelectList(_context.DestroyerConfigurations, "DestroyerConfigurationGuid", "Name");
            ViewData["FleetCarrierGuid"] = new SelectList(_context.FleetCarriers, "FleetCarrierGuid", "FleetCarrierGuid");
            return View();
        }

        // POST: FleetDestroyers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FleetDestroyerGuid,DestroyerConfigurationGuid,FleetCarrierGuid")] FleetDestroyer fleetDestroyer)
        {
            if (ModelState.IsValid)
            {
                fleetDestroyer.FleetDestroyerGuid = Guid.NewGuid();
                _context.Add(fleetDestroyer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DestroyerConfigurationGuid"] = new SelectList(_context.DestroyerConfigurations, "DestroyerConfigurationGuid", "Name", fleetDestroyer.DestroyerConfigurationGuid);
            ViewData["FleetCarrierGuid"] = new SelectList(_context.FleetCarriers, "FleetCarrierGuid", "FleetCarrierGuid", fleetDestroyer.FleetCarrierGuid);
            return View(fleetDestroyer);
        }

        // GET: FleetDestroyers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.FleetDestroyers == null)
            {
                return NotFound();
            }

            var fleetDestroyer = await _context.FleetDestroyers.FindAsync(id);
            if (fleetDestroyer == null)
            {
                return NotFound();
            }
            ViewData["DestroyerConfigurationGuid"] = new SelectList(_context.DestroyerConfigurations, "DestroyerConfigurationGuid", "Name", fleetDestroyer.DestroyerConfigurationGuid);
            ViewData["FleetCarrierGuid"] = new SelectList(_context.FleetCarriers, "FleetCarrierGuid", "FleetCarrierGuid", fleetDestroyer.FleetCarrierGuid);
            return View(fleetDestroyer);
        }

        // POST: FleetDestroyers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("FleetDestroyerGuid,DestroyerConfigurationGuid,FleetCarrierGuid")] FleetDestroyer fleetDestroyer)
        {
            if (id != fleetDestroyer.FleetDestroyerGuid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fleetDestroyer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FleetDestroyerExists(fleetDestroyer.FleetDestroyerGuid))
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
            ViewData["DestroyerConfigurationGuid"] = new SelectList(_context.DestroyerConfigurations, "DestroyerConfigurationGuid", "Name", fleetDestroyer.DestroyerConfigurationGuid);
            ViewData["FleetCarrierGuid"] = new SelectList(_context.FleetCarriers, "FleetCarrierGuid", "FleetCarrierGuid", fleetDestroyer.FleetCarrierGuid);
            return View(fleetDestroyer);
        }

        // GET: FleetDestroyers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.FleetDestroyers == null)
            {
                return NotFound();
            }

            var fleetDestroyer = await _context.FleetDestroyers
                .Include(f => f.DestroyerConfiguration)
                .Include(f => f.FleetCarrier)
                .FirstOrDefaultAsync(m => m.FleetDestroyerGuid == id);
            if (fleetDestroyer == null)
            {
                return NotFound();
            }

            return View(fleetDestroyer);
        }

        // POST: FleetDestroyers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.FleetDestroyers == null)
            {
                return Problem("Entity set 'CelestialFrontier_DevContext.FleetDestroyers'  is null.");
            }
            var fleetDestroyer = await _context.FleetDestroyers.FindAsync(id);
            if (fleetDestroyer != null)
            {
                _context.FleetDestroyers.Remove(fleetDestroyer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FleetDestroyerExists(Guid id)
        {
          return _context.FleetDestroyers.Any(e => e.FleetDestroyerGuid == id);
        }
    }
}
