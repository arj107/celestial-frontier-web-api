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
    public class SpaceshipTypesController : Controller
    {
        private readonly CelestialFrontierContext _context;

        public SpaceshipTypesController(CelestialFrontierContext context)
        {
            _context = context;
        }

        // GET: SpaceshipTypes
        public async Task<IActionResult> Index()
        {
              return _context.SpaceshipTypes != null ? 
                          View(await _context.SpaceshipTypes.ToListAsync()) :
                          Problem("Entity set 'CelestialFrontierContext.SpaceshipTypes'  is null.");
        }

        // GET: SpaceshipTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.SpaceshipTypes == null)
            {
                return NotFound();
            }

            var spaceshipType = await _context.SpaceshipTypes
                .FirstOrDefaultAsync(m => m.SpaceshipTypeGuid == id);
            if (spaceshipType == null)
            {
                return NotFound();
            }

            return View(spaceshipType);
        }

        // GET: SpaceshipTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SpaceshipTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SpaceshipTypeGuid,Text")] SpaceshipType spaceshipType)
        {
            if (ModelState.IsValid)
            {
                spaceshipType.SpaceshipTypeGuid = Guid.NewGuid();
                _context.Add(spaceshipType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(spaceshipType);
        }

        // GET: SpaceshipTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.SpaceshipTypes == null)
            {
                return NotFound();
            }

            var spaceshipType = await _context.SpaceshipTypes.FindAsync(id);
            if (spaceshipType == null)
            {
                return NotFound();
            }
            return View(spaceshipType);
        }

        // POST: SpaceshipTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("SpaceshipTypeGuid,Text")] SpaceshipType spaceshipType)
        {
            if (id != spaceshipType.SpaceshipTypeGuid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(spaceshipType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpaceshipTypeExists(spaceshipType.SpaceshipTypeGuid))
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
            return View(spaceshipType);
        }

        // GET: SpaceshipTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.SpaceshipTypes == null)
            {
                return NotFound();
            }

            var spaceshipType = await _context.SpaceshipTypes
                .FirstOrDefaultAsync(m => m.SpaceshipTypeGuid == id);
            if (spaceshipType == null)
            {
                return NotFound();
            }

            return View(spaceshipType);
        }

        // POST: SpaceshipTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.SpaceshipTypes == null)
            {
                return Problem("Entity set 'CelestialFrontierContext.SpaceshipTypes'  is null.");
            }
            var spaceshipType = await _context.SpaceshipTypes.FindAsync(id);
            if (spaceshipType != null)
            {
                _context.SpaceshipTypes.Remove(spaceshipType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpaceshipTypeExists(Guid id)
        {
          return (_context.SpaceshipTypes?.Any(e => e.SpaceshipTypeGuid == id)).GetValueOrDefault();
        }
    }
}
