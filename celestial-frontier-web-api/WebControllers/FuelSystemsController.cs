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
    public class FuelSystemsController : Controller
    {
        private readonly CelestialFrontierContext _context;

        public FuelSystemsController(CelestialFrontierContext context)
        {
            _context = context;
        }

        // GET: FuelSystems
        public async Task<IActionResult> Index()
        {
            var celestialFrontierContext = _context.FuelSystems.Include(f => f.SpaceshipType);
            return View(await celestialFrontierContext.ToListAsync());
        }

        // GET: FuelSystems/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.FuelSystems == null)
            {
                return NotFound();
            }

            var fuelSystem = await _context.FuelSystems
                .Include(f => f.SpaceshipType)
                .FirstOrDefaultAsync(m => m.FuelSystemGuid == id);
            if (fuelSystem == null)
            {
                return NotFound();
            }

            return View(fuelSystem);
        }

        // GET: FuelSystems/Create
        public IActionResult Create()
        {
            ViewData["SpaceshipTypeGuid"] = new SelectList(_context.SpaceshipTypes, "SpaceshipTypeGuid", "Text");
            return View();
        }

        // POST: FuelSystems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FuelSystemGuid,SpaceshipTypeGuid,ModulePointsCost,Mass,SpecificHeatCapacity,FuelCapacity,FuelRegenerationRate,Title,Description")] FuelSystem fuelSystem)
        {
            if (ModelState.IsValid)
            {
                fuelSystem.FuelSystemGuid = Guid.NewGuid();
                _context.Add(fuelSystem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SpaceshipTypeGuid"] = new SelectList(_context.SpaceshipTypes, "SpaceshipTypeGuid", "Text", fuelSystem.SpaceshipTypeGuid);
            return View(fuelSystem);
        }

        // GET: FuelSystems/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.FuelSystems == null)
            {
                return NotFound();
            }

            var fuelSystem = await _context.FuelSystems.FindAsync(id);
            if (fuelSystem == null)
            {
                return NotFound();
            }
            ViewData["SpaceshipTypeGuid"] = new SelectList(_context.SpaceshipTypes, "SpaceshipTypeGuid", "Text", fuelSystem.SpaceshipTypeGuid);
            return View(fuelSystem);
        }

        // POST: FuelSystems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("FuelSystemGuid,SpaceshipTypeGuid,ModulePointsCost,Mass,SpecificHeatCapacity,FuelCapacity,FuelRegenerationRate,Title,Description")] FuelSystem fuelSystem)
        {
            if (id != fuelSystem.FuelSystemGuid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fuelSystem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuelSystemExists(fuelSystem.FuelSystemGuid))
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
            ViewData["SpaceshipTypeGuid"] = new SelectList(_context.SpaceshipTypes, "SpaceshipTypeGuid", "Text", fuelSystem.SpaceshipTypeGuid);
            return View(fuelSystem);
        }

        // GET: FuelSystems/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.FuelSystems == null)
            {
                return NotFound();
            }

            var fuelSystem = await _context.FuelSystems
                .Include(f => f.SpaceshipType)
                .FirstOrDefaultAsync(m => m.FuelSystemGuid == id);
            if (fuelSystem == null)
            {
                return NotFound();
            }

            return View(fuelSystem);
        }

        // POST: FuelSystems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.FuelSystems == null)
            {
                return Problem("Entity set 'CelestialFrontierContext.FuelSystems'  is null.");
            }
            var fuelSystem = await _context.FuelSystems.FindAsync(id);
            if (fuelSystem != null)
            {
                _context.FuelSystems.Remove(fuelSystem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FuelSystemExists(Guid id)
        {
          return (_context.FuelSystems?.Any(e => e.FuelSystemGuid == id)).GetValueOrDefault();
        }
    }
}
