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
    public class PayloadsController : Controller
    {
        private readonly CelestialFrontierContext _context;

        public PayloadsController(CelestialFrontierContext context)
        {
            _context = context;
        }

        // GET: Payloads
        public async Task<IActionResult> Index()
        {
            var celestialFrontierContext = _context.Payloads.Include(p => p.PayloadType).Include(p => p.PowerLevel).Include(p => p.WeaponFrameType);
            return View(await celestialFrontierContext.ToListAsync());
        }

        // GET: Payloads/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Payloads == null)
            {
                return NotFound();
            }

            var payload = await _context.Payloads
                .Include(p => p.PayloadType)
                .Include(p => p.PowerLevel)
                .Include(p => p.WeaponFrameType)
                .FirstOrDefaultAsync(m => m.PayloadGuid == id);
            if (payload == null)
            {
                return NotFound();
            }

            return View(payload);
        }

        // GET: Payloads/Create
        public IActionResult Create()
        {
            ViewData["PayloadTypeGuid"] = new SelectList(_context.PayloadTypes, "PayloadTypeGuid", "Text");
            ViewData["PowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text");
            ViewData["WeaponFrameTypeGuid"] = new SelectList(_context.WeaponFrameTypes, "WeaponFrameTypeGuid", "Text");
            return View();
        }

        // POST: Payloads/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PayloadGuid,PayloadTypeGuid,WeaponFrameTypeGuid,PowerLevelGuid,ModulePointsCost,KineticDamageBias,CounterSusceptibility,BaseDamage,BaseCriticalHitMultiplier,Title,Description")] Payload payload)
        {
            if (ModelState.IsValid)
            {
                payload.PayloadGuid = Guid.NewGuid();
                _context.Add(payload);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PayloadTypeGuid"] = new SelectList(_context.PayloadTypes, "PayloadTypeGuid", "Text", payload.PayloadTypeGuid);
            ViewData["PowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text", payload.PowerLevelGuid);
            ViewData["WeaponFrameTypeGuid"] = new SelectList(_context.WeaponFrameTypes, "WeaponFrameTypeGuid", "Text", payload.WeaponFrameTypeGuid);
            return View(payload);
        }

        // GET: Payloads/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Payloads == null)
            {
                return NotFound();
            }

            var payload = await _context.Payloads.FindAsync(id);
            if (payload == null)
            {
                return NotFound();
            }
            ViewData["PayloadTypeGuid"] = new SelectList(_context.PayloadTypes, "PayloadTypeGuid", "Text", payload.PayloadTypeGuid);
            ViewData["PowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text", payload.PowerLevelGuid);
            ViewData["WeaponFrameTypeGuid"] = new SelectList(_context.WeaponFrameTypes, "WeaponFrameTypeGuid", "Text", payload.WeaponFrameTypeGuid);
            return View(payload);
        }

        // POST: Payloads/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PayloadGuid,PayloadTypeGuid,WeaponFrameTypeGuid,PowerLevelGuid,ModulePointsCost,KineticDamageBias,CounterSusceptibility,BaseDamage,BaseCriticalHitMultiplier,Title,Description")] Payload payload)
        {
            if (id != payload.PayloadGuid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payload);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PayloadExists(payload.PayloadGuid))
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
            ViewData["PayloadTypeGuid"] = new SelectList(_context.PayloadTypes, "PayloadTypeGuid", "Text", payload.PayloadTypeGuid);
            ViewData["PowerLevelGuid"] = new SelectList(_context.PowerLevels, "PowerLevelGuid", "Text", payload.PowerLevelGuid);
            ViewData["WeaponFrameTypeGuid"] = new SelectList(_context.WeaponFrameTypes, "WeaponFrameTypeGuid", "Text", payload.WeaponFrameTypeGuid);
            return View(payload);
        }

        // GET: Payloads/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Payloads == null)
            {
                return NotFound();
            }

            var payload = await _context.Payloads
                .Include(p => p.PayloadType)
                .Include(p => p.PowerLevel)
                .Include(p => p.WeaponFrameType)
                .FirstOrDefaultAsync(m => m.PayloadGuid == id);
            if (payload == null)
            {
                return NotFound();
            }

            return View(payload);
        }

        // POST: Payloads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Payloads == null)
            {
                return Problem("Entity set 'CelestialFrontierContext.Payloads'  is null.");
            }
            var payload = await _context.Payloads.FindAsync(id);
            if (payload != null)
            {
                _context.Payloads.Remove(payload);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PayloadExists(Guid id)
        {
          return (_context.Payloads?.Any(e => e.PayloadGuid == id)).GetValueOrDefault();
        }
    }
}
