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
    public class PowerLevelsController : ControllerBase
    {
        private readonly CelestialFrontier_DevContext _context;

        public PowerLevelsController(CelestialFrontier_DevContext context)
        {
            _context = context;
        }

        // GET: api/PowerLevels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PowerLevelDto>>> GetPowerLevels()
        {
            if (_context.PowerLevels == null)
            {
                return NotFound();
            }

            List<PowerLevel> powerLevelEntities = await _context.PowerLevels
              .ToListAsync();

            List<PowerLevelDto> powerLevelDtos = new List<PowerLevelDto>();
            foreach (PowerLevel powerLevel in powerLevelEntities) {
                powerLevelDtos.Add(new PowerLevelDto(powerLevel));
            }

            return powerLevelDtos;
        }

        // GET: api/PowerLevels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PowerLevel>> GetPowerLevel(Guid id)
        {
          if (_context.PowerLevels == null)
          {
              return NotFound();
          }
            var powerLevel = await _context.PowerLevels.FindAsync(id);

            if (powerLevel == null)
            {
                return NotFound();
            }

            return powerLevel;
        }

        // PUT: api/PowerLevels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPowerLevel(Guid id, PowerLevel powerLevel)
        {
            if (id != powerLevel.PowerLevelGuid)
            {
                return BadRequest();
            }

            _context.Entry(powerLevel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PowerLevelExists(id))
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

        // POST: api/PowerLevels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PowerLevel>> PostPowerLevel(PowerLevel powerLevel)
        {
          if (_context.PowerLevels == null)
          {
              return Problem("Entity set 'CelestialFrontier_DevContext.PowerLevels'  is null.");
          }
            _context.PowerLevels.Add(powerLevel);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PowerLevelExists(powerLevel.PowerLevelGuid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPowerLevel", new { id = powerLevel.PowerLevelGuid }, powerLevel);
        }

        // DELETE: api/PowerLevels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePowerLevel(Guid id)
        {
            if (_context.PowerLevels == null)
            {
                return NotFound();
            }
            var powerLevel = await _context.PowerLevels.FindAsync(id);
            if (powerLevel == null)
            {
                return NotFound();
            }

            _context.PowerLevels.Remove(powerLevel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PowerLevelExists(Guid id)
        {
            return (_context.PowerLevels?.Any(e => e.PowerLevelGuid == id)).GetValueOrDefault();
        }
    }
}
