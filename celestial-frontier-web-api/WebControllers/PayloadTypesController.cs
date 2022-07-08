using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using celestial_frontier_web_api.Models;

namespace celestial_frontier_web_api.WebControllers {
    public class PayloadTypesController : Controller {
        private readonly CelestialFrontier_DevContext _context;

        public PayloadTypesController(CelestialFrontier_DevContext context) {
            _context = context;
        }

        // GET: PayloadTypes
        public async Task<IActionResult> Index() {
            return _context.PayloadTypes != null ?
                        View(await _context.PayloadTypes.ToListAsync()) :
                        Problem("Entity set 'CelestialFrontier_DevContext.PayloadTypes'  is null.");
        }

        // GET: PayloadTypes/Details/5
        public async Task<IActionResult> Details(Guid? id) {
            if (id == null || _context.PayloadTypes == null) {
                return NotFound();
            }

            var payloadType = await _context.PayloadTypes
                .FirstOrDefaultAsync(m => m.PayloadTypeGuid == id);
            if (payloadType == null) {
                return NotFound();
            }

            return View(payloadType);
        }

        // GET: PayloadTypes/Create
        public IActionResult Create() {
            return View();
        }

        // POST: PayloadTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PayloadTypeGuid,Text")] PayloadType payloadType) {
            if (ModelState.IsValid) {
                payloadType.PayloadTypeGuid = Guid.NewGuid();
                _context.Add(payloadType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(payloadType);
        }

        // GET: PayloadTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id) {
            if (id == null || _context.PayloadTypes == null) {
                return NotFound();
            }

            var payloadType = await _context.PayloadTypes.FindAsync(id);
            if (payloadType == null) {
                return NotFound();
            }
            return View(payloadType);
        }

        // POST: PayloadTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PayloadTypeGuid,Text")] PayloadType payloadType) {
            if (id != payloadType.PayloadTypeGuid) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(payloadType);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!PayloadTypeExists(payloadType.PayloadTypeGuid)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(payloadType);
        }

        // GET: PayloadTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id) {
            if (id == null || _context.PayloadTypes == null) {
                return NotFound();
            }

            var payloadType = await _context.PayloadTypes
                .FirstOrDefaultAsync(m => m.PayloadTypeGuid == id);
            if (payloadType == null) {
                return NotFound();
            }

            return View(payloadType);
        }

        // POST: PayloadTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id) {
            if (_context.PayloadTypes == null) {
                return Problem("Entity set 'CelestialFrontier_DevContext.PayloadTypes'  is null.");
            }
            var payloadType = await _context.PayloadTypes.FindAsync(id);
            if (payloadType != null) {
                _context.PayloadTypes.Remove(payloadType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PayloadTypeExists(Guid id) {
            return (_context.PayloadTypes?.Any(e => e.PayloadTypeGuid == id)).GetValueOrDefault();
        }
    }
}
