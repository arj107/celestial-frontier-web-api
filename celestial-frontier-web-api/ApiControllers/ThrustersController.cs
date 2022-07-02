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
    public class ThrustersController : ControllerBase
    {
        private readonly CelestialFrontier_DevContext _context;

        public ThrustersController(CelestialFrontier_DevContext context)
        {
            _context = context;
        }

        // GET: api/Thrusters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ThrusterDto>>> GetThrusters()
        {
            if (_context.Thrusters == null)
            {
                return NotFound();
            }

            List<Thruster> thrusterEntities = await _context.Thrusters
              .Include(s => s.SpaceshipType)
              .ToListAsync();

            List<ThrusterDto> thrusterDtos = new List<ThrusterDto>();
            foreach (Thruster thruster in thrusterEntities) {
                thrusterDtos.Add(new ThrusterDto(thruster));
            }

            return thrusterDtos;
        }

        // GET: api/Thrusters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Thruster>> GetThruster(Guid id)
        {
          if (_context.Thrusters == null)
          {
              return NotFound();
          }
            var thruster = await _context.Thrusters.FindAsync(id);

            if (thruster == null)
            {
                return NotFound();
            }

            return thruster;
        }

        // PUT: api/Thrusters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutThruster(Guid id, Thruster thruster)
        {
            if (id != thruster.ThrusterGuid)
            {
                return BadRequest();
            }

            _context.Entry(thruster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ThrusterExists(id))
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

        // POST: api/Thrusters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Thruster>> PostThruster(Thruster thruster)
        {
          if (_context.Thrusters == null)
          {
              return Problem("Entity set 'CelestialFrontier_DevContext.Thrusters'  is null.");
          }
            _context.Thrusters.Add(thruster);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ThrusterExists(thruster.ThrusterGuid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetThruster", new { id = thruster.ThrusterGuid }, thruster);
        }

        // DELETE: api/Thrusters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteThruster(Guid id)
        {
            if (_context.Thrusters == null)
            {
                return NotFound();
            }
            var thruster = await _context.Thrusters.FindAsync(id);
            if (thruster == null)
            {
                return NotFound();
            }

            _context.Thrusters.Remove(thruster);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ThrusterExists(Guid id)
        {
            return (_context.Thrusters?.Any(e => e.ThrusterGuid == id)).GetValueOrDefault();
        }
    }
}
