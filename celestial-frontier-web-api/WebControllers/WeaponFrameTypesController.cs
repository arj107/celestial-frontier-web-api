using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using celestial_frontier_web_api.Models;

namespace celestial_frontier_web_api.WebControllers {
    public class WeaponFrameTypesController : Controller {
        private readonly CelestialFrontier_DevContext _context;

        public WeaponFrameTypesController(CelestialFrontier_DevContext context) {
            _context = context;
        }

        // GET: WeaponFrameTypes
        public async Task<IActionResult> Index() {
            return _context.WeaponFrameTypes != null ?
                        View(await _context.WeaponFrameTypes.ToListAsync()) :
                        Problem("Entity set 'CelestialFrontier_DevContext.WeaponFrameTypes'  is null.");
        }

        // GET: WeaponFrameTypes/Details/5
        public async Task<IActionResult> Details(Guid? id) {
            if (id == null || _context.WeaponFrameTypes == null) {
                return NotFound();
            }

            var weaponFrameType = await _context.WeaponFrameTypes
                .FirstOrDefaultAsync(m => m.WeaponFrameTypeGuid == id);
            if (weaponFrameType == null) {
                return NotFound();
            }

            return View(weaponFrameType);
        }

        // GET: WeaponFrameTypes/Create
        public IActionResult Create() {
            return View();
        }

        // POST: WeaponFrameTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WeaponFrameTypeGuid,Text")] WeaponFrameType weaponFrameType) {
            if (ModelState.IsValid) {
                weaponFrameType.WeaponFrameTypeGuid = Guid.NewGuid();
                _context.Add(weaponFrameType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(weaponFrameType);
        }

        // GET: WeaponFrameTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id) {
            if (id == null || _context.WeaponFrameTypes == null) {
                return NotFound();
            }

            var weaponFrameType = await _context.WeaponFrameTypes.FindAsync(id);
            if (weaponFrameType == null) {
                return NotFound();
            }
            return View(weaponFrameType);
        }

        // POST: WeaponFrameTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("WeaponFrameTypeGuid,Text")] WeaponFrameType weaponFrameType) {
            if (id != weaponFrameType.WeaponFrameTypeGuid) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(weaponFrameType);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!WeaponFrameTypeExists(weaponFrameType.WeaponFrameTypeGuid)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(weaponFrameType);
        }

        // GET: WeaponFrameTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id) {
            if (id == null || _context.WeaponFrameTypes == null) {
                return NotFound();
            }

            var weaponFrameType = await _context.WeaponFrameTypes
                .FirstOrDefaultAsync(m => m.WeaponFrameTypeGuid == id);
            if (weaponFrameType == null) {
                return NotFound();
            }

            return View(weaponFrameType);
        }

        // POST: WeaponFrameTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id) {
            if (_context.WeaponFrameTypes == null) {
                return Problem("Entity set 'CelestialFrontier_DevContext.WeaponFrameTypes'  is null.");
            }
            var weaponFrameType = await _context.WeaponFrameTypes.FindAsync(id);
            if (weaponFrameType != null) {
                _context.WeaponFrameTypes.Remove(weaponFrameType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WeaponFrameTypeExists(Guid id) {
            return (_context.WeaponFrameTypes?.Any(e => e.WeaponFrameTypeGuid == id)).GetValueOrDefault();
        }
    }
}
