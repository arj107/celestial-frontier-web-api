using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using celestial_frontier_web_api.Models;
using celestial_frontier_web_api.Models.DTOs;

namespace celestial_frontier_web_api.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PointDefenseSystemsController : ControllerBase
    {
        private readonly CelestialFrontierContext _context;

        public PointDefenseSystemsController(CelestialFrontierContext context)
        {
            _context = context;
        }

        // GET: api/PointDefenseSystems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PointDefenseSystemDto>>> GetPointDefenseSystems()
        {
            if (_context.PointDefenseSystems == null)
            {
                return NotFound();
            }

            List<PointDefenseSystem> pointDefenseSystemEntities = await _context.PointDefenseSystems
              .Include(s => s.PowerLevel)
              .ToListAsync();

            List<PointDefenseSystemDto> pointDefenseSystemDtos = new List<PointDefenseSystemDto>();
            foreach (PointDefenseSystem pointDefenseSystem in pointDefenseSystemEntities) {
                pointDefenseSystemDtos.Add(new PointDefenseSystemDto(pointDefenseSystem));
            }

            return pointDefenseSystemDtos;
        }

        // GET: api/PointDefenseSystems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PointDefenseSystem>> GetPointDefenseSystem(Guid id)
        {
          if (_context.PointDefenseSystems == null)
          {
              return NotFound();
          }
            var pointDefenseSystem = await _context.PointDefenseSystems.FindAsync(id);

            if (pointDefenseSystem == null)
            {
                return NotFound();
            }

            return pointDefenseSystem;
        }

        // PUT: api/PointDefenseSystems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPointDefenseSystem(Guid id, PointDefenseSystem pointDefenseSystem)
        {
            if (id != pointDefenseSystem.PointDefenseSystemGuid)
            {
                return BadRequest();
            }

            _context.Entry(pointDefenseSystem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PointDefenseSystemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PointDefenseSystems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PointDefenseSystem>> PostPointDefenseSystem(PointDefenseSystem pointDefenseSystem)
        {
          if (_context.PointDefenseSystems == null)
          {
              return Problem("Entity set 'CelestialFrontierContext.PointDefenseSystems'  is null.");
          }
            _context.PointDefenseSystems.Add(pointDefenseSystem);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PointDefenseSystemExists(pointDefenseSystem.PointDefenseSystemGuid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPointDefenseSystem", new { id = pointDefenseSystem.PointDefenseSystemGuid }, pointDefenseSystem);
        }

        // DELETE: api/PointDefenseSystems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePointDefenseSystem(Guid id)
        {
            if (_context.PointDefenseSystems == null)
            {
                return NotFound();
            }
            var pointDefenseSystem = await _context.PointDefenseSystems.FindAsync(id);
            if (pointDefenseSystem == null)
            {
                return NotFound();
            }

            _context.PointDefenseSystems.Remove(pointDefenseSystem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PointDefenseSystemExists(Guid id)
        {
            return (_context.PointDefenseSystems?.Any(e => e.PointDefenseSystemGuid == id)).GetValueOrDefault();
        }
    }
}
