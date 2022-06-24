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
    public class WeaponFrameTypesController : ControllerBase
    {
        private readonly CelestialFrontierContext _context;

        public WeaponFrameTypesController(CelestialFrontierContext context)
        {
            _context = context;
        }

        // GET: api/WeaponFrameTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeaponFrameTypeDto>>> GetWeaponFrameTypes()
        {
            if (_context.WeaponFrameTypes == null)
            {
                return NotFound();
            }

            List<WeaponFrameType> weaponFrameTypeEntities = await _context.WeaponFrameTypes
              .ToListAsync();

            List<WeaponFrameTypeDto> weaponFrameTypeDtos = new List<WeaponFrameTypeDto>();
            foreach (WeaponFrameType weaponFrameType in weaponFrameTypeEntities) {
                weaponFrameTypeDtos.Add(new WeaponFrameTypeDto(weaponFrameType));
            }

            return weaponFrameTypeDtos;
        }

        // GET: api/WeaponFrameTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WeaponFrameType>> GetWeaponFrameType(Guid id)
        {
          if (_context.WeaponFrameTypes == null)
          {
              return NotFound();
          }
            var weaponFrameType = await _context.WeaponFrameTypes.FindAsync(id);

            if (weaponFrameType == null)
            {
                return NotFound();
            }

            return weaponFrameType;
        }

        // PUT: api/WeaponFrameTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWeaponFrameType(Guid id, WeaponFrameType weaponFrameType)
        {
            if (id != weaponFrameType.WeaponFrameTypeGuid)
            {
                return BadRequest();
            }

            _context.Entry(weaponFrameType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WeaponFrameTypeExists(id))
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

        // POST: api/WeaponFrameTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WeaponFrameType>> PostWeaponFrameType(WeaponFrameType weaponFrameType)
        {
          if (_context.WeaponFrameTypes == null)
          {
              return Problem("Entity set 'CelestialFrontierContext.WeaponFrameTypes'  is null.");
          }
            _context.WeaponFrameTypes.Add(weaponFrameType);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (WeaponFrameTypeExists(weaponFrameType.WeaponFrameTypeGuid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetWeaponFrameType", new { id = weaponFrameType.WeaponFrameTypeGuid }, weaponFrameType);
        }

        // DELETE: api/WeaponFrameTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWeaponFrameType(Guid id)
        {
            if (_context.WeaponFrameTypes == null)
            {
                return NotFound();
            }
            var weaponFrameType = await _context.WeaponFrameTypes.FindAsync(id);
            if (weaponFrameType == null)
            {
                return NotFound();
            }

            _context.WeaponFrameTypes.Remove(weaponFrameType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WeaponFrameTypeExists(Guid id)
        {
            return (_context.WeaponFrameTypes?.Any(e => e.WeaponFrameTypeGuid == id)).GetValueOrDefault();
        }
    }
}
