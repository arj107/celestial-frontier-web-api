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
    public class ThrustersController : Controller
    {
        private readonly CelestialFrontierContext _context;

        public ThrustersController(CelestialFrontierContext context)
        {
            _context = context;
        }

        // GET: Thrusters
        public async Task<IActionResult> Index()
        {
            var celestialFrontierContext = _context.Thrusters.Include(t => t.SpaceshipType);
            return View(await celestialFrontierContext.ToListAsync());
        }

        // GET: Thrusters/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Thrusters == null)
            {
                return NotFound();
            }

            var thruster = await _context.Thrusters
                .Include(t => t.SpaceshipType)
                .FirstOrDefaultAsync(m => m.ThrusterGuid == id);
            if (thruster == null)
            {
                return NotFound();
            }

            return View(thruster);
        }

        // GET: Thrusters/Create
        public IActionResult Create()
        {
            ViewData["SpaceshipTypeGuid"] = new SelectList(_context.SpaceshipTypes, "SpaceshipTypeGuid", "Text");
            return View();
        }

        // POST: Thrusters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ThrusterGuid,SpaceshipTypeGuid,ModulePointsCost,Mass,SpecificHeatCapacity,Thrust,Efficiency,Title,Description")] Thruster thruster)
        {
            if (ModelState.IsValid)
            {
                thruster.ThrusterGuid = Guid.NewGuid();
                _context.Add(thruster);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SpaceshipTypeGuid"] = new SelectList(_context.SpaceshipTypes, "SpaceshipTypeGuid", "Text", thruster.SpaceshipTypeGuid);
            return View(thruster);
        }

        // GET: Thrusters/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Thrusters == null)
            {
                return NotFound();
            }

            var thruster = await _context.Thrusters.FindAsync(id);
            if (thruster == null)
            {
                return NotFound();
            }
            ViewData["SpaceshipTypeGuid"] = new SelectList(_context.SpaceshipTypes, "SpaceshipTypeGuid", "Text", thruster.SpaceshipTypeGuid);
            return View(thruster);
        }

        // POST: Thrusters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ThrusterGuid,SpaceshipTypeGuid,ModulePointsCost,Mass,SpecificHeatCapacity,Thrust,Efficiency,Title,Description")] Thruster thruster)
        {
            if (id != thruster.ThrusterGuid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(thruster);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThrusterExists(thruster.ThrusterGuid))
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
            ViewData["SpaceshipTypeGuid"] = new SelectList(_context.SpaceshipTypes, "SpaceshipTypeGuid", "Text", thruster.SpaceshipTypeGuid);
            return View(thruster);
        }

        // GET: Thrusters/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Thrusters == null)
            {
                return NotFound();
            }

            var thruster = await _context.Thrusters
                .Include(t => t.SpaceshipType)
                .FirstOrDefaultAsync(m => m.ThrusterGuid == id);
            if (thruster == null)
            {
                return NotFound();
            }

            return View(thruster);
        }

        // POST: Thrusters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Thrusters == null)
            {
                return Problem("Entity set 'CelestialFrontierContext.Thrusters'  is null.");
            }
            var thruster = await _context.Thrusters.FindAsync(id);
            if (thruster != null)
            {
                _context.Thrusters.Remove(thruster);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThrusterExists(Guid id)
        {
          return (_context.Thrusters?.Any(e => e.ThrusterGuid == id)).GetValueOrDefault();
        }
    }
}
