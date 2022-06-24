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
    public class SpaceshipsController : ControllerBase
    {
        private readonly CelestialFrontierContext _context;

        public SpaceshipsController(CelestialFrontierContext context)
        {
            _context = context;
        }

        // GET: api/Spaceships
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpaceshipDto>>> GetSpaceships()
        {
            if (_context.Spaceships == null)
            {
                  return NotFound();
            }
            
            List<Spaceship> spaceshipEntities = await _context.Spaceships
              .Include(s => s.SpaceshipType)
              .Include(s => s.SpaceshipCategory)
              .Include(s => s.PointDefenseSystemPowerLevel)
              .Include(s => s.WeaponPowerLevel)
              .ToListAsync();

            List<SpaceshipDto> spaceshipDtos = new List<SpaceshipDto>();
            foreach (Spaceship spaceship in spaceshipEntities) {
                spaceshipDtos.Add(new SpaceshipDto(spaceship));
            }

            return spaceshipDtos;
        }

        // GET: api/Spaceships/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Spaceship>> GetSpaceship(Guid id)
        {
          if (_context.Spaceships == null)
          {
              return NotFound();
          }
            var spaceship = await _context.Spaceships.FindAsync(id);

            if (spaceship == null)
            {
                return NotFound();
            }

            return spaceship;
        }

        // PUT: api/Spaceships/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpaceship(Guid id, Spaceship spaceship)
        {
            if (id != spaceship.SpaceshipGuid)
            {
                return BadRequest();
            }

            _context.Entry(spaceship).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpaceshipExists(id))
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

        // POST: api/Spaceships
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Spaceship>> PostSpaceship(Spaceship spaceship)
        {
          if (_context.Spaceships == null)
          {
              return Problem("Entity set 'CelestialFrontierContext.Spaceships'  is null.");
          }
            _context.Spaceships.Add(spaceship);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SpaceshipExists(spaceship.SpaceshipGuid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSpaceship", new { id = spaceship.SpaceshipGuid }, spaceship);
        }

        // DELETE: api/Spaceships/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpaceship(Guid id)
        {
            if (_context.Spaceships == null)
            {
                return NotFound();
            }
            var spaceship = await _context.Spaceships.FindAsync(id);
            if (spaceship == null)
            {
                return NotFound();
            }

            _context.Spaceships.Remove(spaceship);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SpaceshipExists(Guid id)
        {
            return (_context.Spaceships?.Any(e => e.SpaceshipGuid == id)).GetValueOrDefault();
        }
    }
}
