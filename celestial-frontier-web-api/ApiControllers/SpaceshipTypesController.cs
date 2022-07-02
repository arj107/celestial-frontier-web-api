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
    public class SpaceshipTypesController : ControllerBase
    {
        private readonly CelestialFrontier_DevContext _context;

        public SpaceshipTypesController(CelestialFrontier_DevContext context)
        {
            _context = context;
        }

        // GET: api/SpaceshipTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpaceshipTypeDto>>> GetSpaceshipTypes()
        {
            if (_context.SpaceshipTypes == null)
            {
                return NotFound();
            }

            List<SpaceshipType> spaceshipTypeEntities = await _context.SpaceshipTypes
              .ToListAsync();

            List<SpaceshipTypeDto> spaceshipTypeDtos = new List<SpaceshipTypeDto>();
            foreach (SpaceshipType spaceshipType in spaceshipTypeEntities) {
                spaceshipTypeDtos.Add(new SpaceshipTypeDto(spaceshipType));
            }

            return spaceshipTypeDtos;
        }

        // GET: api/SpaceshipTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SpaceshipType>> GetSpaceshipType(Guid id)
        {
          if (_context.SpaceshipTypes == null)
          {
              return NotFound();
          }
            var spaceshipType = await _context.SpaceshipTypes.FindAsync(id);

            if (spaceshipType == null)
            {
                return NotFound();
            }

            return spaceshipType;
        }

        // PUT: api/SpaceshipTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpaceshipType(Guid id, SpaceshipType spaceshipType)
        {
            if (id != spaceshipType.SpaceshipTypeGuid)
            {
                return BadRequest();
            }

            _context.Entry(spaceshipType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpaceshipTypeExists(id))
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

        // POST: api/SpaceshipTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SpaceshipType>> PostSpaceshipType(SpaceshipType spaceshipType)
        {
          if (_context.SpaceshipTypes == null)
          {
              return Problem("Entity set 'CelestialFrontier_DevContext.SpaceshipTypes'  is null.");
          }
            _context.SpaceshipTypes.Add(spaceshipType);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SpaceshipTypeExists(spaceshipType.SpaceshipTypeGuid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSpaceshipType", new { id = spaceshipType.SpaceshipTypeGuid }, spaceshipType);
        }

        // DELETE: api/SpaceshipTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpaceshipType(Guid id)
        {
            if (_context.SpaceshipTypes == null)
            {
                return NotFound();
            }
            var spaceshipType = await _context.SpaceshipTypes.FindAsync(id);
            if (spaceshipType == null)
            {
                return NotFound();
            }

            _context.SpaceshipTypes.Remove(spaceshipType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SpaceshipTypeExists(Guid id)
        {
            return (_context.SpaceshipTypes?.Any(e => e.SpaceshipTypeGuid == id)).GetValueOrDefault();
        }
    }
}
