using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using celestial_frontier_web_api.Models;

namespace celestial_frontier_web_api.WebControllers {
    public class SpaceshipCategoriesController : Controller {
        private readonly CelestialFrontier_DevContext _context;

        public SpaceshipCategoriesController(CelestialFrontier_DevContext context) {
            _context = context;
        }

        // GET: SpaceshipCategories
        public async Task<IActionResult> Index() {
            return _context.SpaceshipCategories != null ?
                        View(await _context.SpaceshipCategories.ToListAsync()) :
                        Problem("Entity set 'CelestialFrontier_DevContext.SpaceshipCategories'  is null.");
        }

        // GET: SpaceshipCategories/Details/5
        public async Task<IActionResult> Details(Guid? id) {
            if (id == null || _context.SpaceshipCategories == null) {
                return NotFound();
            }

            var spaceshipCategory = await _context.SpaceshipCategories
                .FirstOrDefaultAsync(m => m.SpaceshipCategoryGuid == id);
            if (spaceshipCategory == null) {
                return NotFound();
            }

            return View(spaceshipCategory);
        }

        // GET: SpaceshipCategories/Create
        public IActionResult Create() {
            return View();
        }

        // POST: SpaceshipCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SpaceshipCategoryGuid,Text")] SpaceshipCategory spaceshipCategory) {
            if (ModelState.IsValid) {
                spaceshipCategory.SpaceshipCategoryGuid = Guid.NewGuid();
                _context.Add(spaceshipCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(spaceshipCategory);
        }

        // GET: SpaceshipCategories/Edit/5
        public async Task<IActionResult> Edit(Guid? id) {
            if (id == null || _context.SpaceshipCategories == null) {
                return NotFound();
            }

            var spaceshipCategory = await _context.SpaceshipCategories.FindAsync(id);
            if (spaceshipCategory == null) {
                return NotFound();
            }
            return View(spaceshipCategory);
        }

        // POST: SpaceshipCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("SpaceshipCategoryGuid,Text")] SpaceshipCategory spaceshipCategory) {
            if (id != spaceshipCategory.SpaceshipCategoryGuid) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(spaceshipCategory);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!SpaceshipCategoryExists(spaceshipCategory.SpaceshipCategoryGuid)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(spaceshipCategory);
        }

        // GET: SpaceshipCategories/Delete/5
        public async Task<IActionResult> Delete(Guid? id) {
            if (id == null || _context.SpaceshipCategories == null) {
                return NotFound();
            }

            var spaceshipCategory = await _context.SpaceshipCategories
                .FirstOrDefaultAsync(m => m.SpaceshipCategoryGuid == id);
            if (spaceshipCategory == null) {
                return NotFound();
            }

            return View(spaceshipCategory);
        }

        // POST: SpaceshipCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id) {
            if (_context.SpaceshipCategories == null) {
                return Problem("Entity set 'CelestialFrontier_DevContext.SpaceshipCategories'  is null.");
            }
            var spaceshipCategory = await _context.SpaceshipCategories.FindAsync(id);
            if (spaceshipCategory != null) {
                _context.SpaceshipCategories.Remove(spaceshipCategory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpaceshipCategoryExists(Guid id) {
            return (_context.SpaceshipCategories?.Any(e => e.SpaceshipCategoryGuid == id)).GetValueOrDefault();
        }
    }
}
