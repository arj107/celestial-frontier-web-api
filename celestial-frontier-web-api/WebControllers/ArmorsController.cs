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
    public class ArmorsController : Controller
    {
        private readonly CelestialFrontierContext _context;

        public ArmorsController(CelestialFrontierContext context)
        {
            _context = context;
        }

        // GET: Armors
        public async Task<IActionResult> Index()
        {
            var celestialFrontierContext = _context.Armors.Include(a => a.SpaceshipType);
            return View(await celestialFrontierContext.ToListAsync());
        }

        // GET: Armors/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Armors == null)
            {
                return NotFound();
            }

            var armor = await _context.Armors
                .Include(a => a.SpaceshipType)
                .FirstOrDefaultAsync(m => m.ArmorGuid == id);
            if (armor == null)
            {
                return NotFound();
            }

            return View(armor);
        }

        // GET: Armors/Create
        public IActionResult Create()
        {
            ViewData["SpaceshipTypeGuid"] = new SelectList(_context.SpaceshipTypes, "SpaceshipTypeGuid", "Text");
            return View();
        }

        // POST: Armors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ArmorGuid,SpaceshipTypeGuid,ModulePointsCost,Mass,SpecificHeatCapacity,KineticResistance,EnergyResistance,Title,Description")] Armor armor)
        {
            if (ModelState.IsValid)
            {
                armor.ArmorGuid = Guid.NewGuid();
                _context.Add(armor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SpaceshipTypeGuid"] = new SelectList(_context.SpaceshipTypes, "SpaceshipTypeGuid", "Text", armor.SpaceshipTypeGuid);
            return View(armor);
        }

        // GET: Armors/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Armors == null)
            {
                return NotFound();
            }

            var armor = await _context.Armors.FindAsync(id);
            if (armor == null)
            {
                return NotFound();
            }
            ViewData["SpaceshipTypeGuid"] = new SelectList(_context.SpaceshipTypes, "SpaceshipTypeGuid", "Text", armor.SpaceshipTypeGuid);
            return View(armor);
        }

        // POST: Armors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ArmorGuid,SpaceshipTypeGuid,ModulePointsCost,Mass,SpecificHeatCapacity,KineticResistance,EnergyResistance,Title,Description")] Armor armor)
        {
            if (id != armor.ArmorGuid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(armor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArmorExists(armor.ArmorGuid))
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
            ViewData["SpaceshipTypeGuid"] = new SelectList(_context.SpaceshipTypes, "SpaceshipTypeGuid", "Text", armor.SpaceshipTypeGuid);
            return View(armor);
        }

        // GET: Armors/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Armors == null)
            {
                return NotFound();
            }

            var armor = await _context.Armors
                .Include(a => a.SpaceshipType)
                .FirstOrDefaultAsync(m => m.ArmorGuid == id);
            if (armor == null)
            {
                return NotFound();
            }

            return View(armor);
        }

        // POST: Armors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Armors == null)
            {
                return Problem("Entity set 'CelestialFrontierContext.Armors'  is null.");
            }
            var armor = await _context.Armors.FindAsync(id);
            if (armor != null)
            {
                _context.Armors.Remove(armor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArmorExists(Guid id)
        {
          return (_context.Armors?.Any(e => e.ArmorGuid == id)).GetValueOrDefault();
        }
    }
}
