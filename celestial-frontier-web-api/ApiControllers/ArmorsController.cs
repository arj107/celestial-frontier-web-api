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
    public class ArmorsController : ControllerBase
    {
        private readonly CelestialFrontier_DevContext _context;

        public ArmorsController(CelestialFrontier_DevContext context)
        {
            _context = context;
        }

        // GET: api/Armors?spaceshipTypeGuid={spaceshipTypeGuid}
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArmorDto>>> GetArmors([FromQuery] Guid? spaceshipTypeGuid = null)
        {
            if (_context.Armors == null)
            {
                return NotFound();
            }

            IQueryable<Armor> armorQueryables = _context.Armors
              .Include(s => s.SpaceshipType);

            if (spaceshipTypeGuid != null) {
                armorQueryables = armorQueryables.Where(s => s.SpaceshipTypeGuid == spaceshipTypeGuid);
            }

            List<Armor> armorEntities = await armorQueryables.ToListAsync();

            List<ArmorDto> armorDtos = new List<ArmorDto>();
            foreach (Armor armor in armorEntities) {
                armorDtos.Add(new ArmorDto(armor));
            }

            return armorDtos;
        }

        // GET: api/Armors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Armor>> GetArmor(Guid id)
        {
          if (_context.Armors == null)
          {
              return NotFound();
          }
            var armor = await _context.Armors.FindAsync(id);

            if (armor == null)
            {
                return NotFound();
            }

            return armor;
        }

        // PUT: api/Armors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArmor(Guid id, Armor armor)
        {
            if (id != armor.ArmorGuid)
            {
                return BadRequest();
            }

            _context.Entry(armor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArmorExists(id))
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

        // POST: api/Armors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Armor>> PostArmor(Armor armor)
        {
          if (_context.Armors == null)
          {
              return Problem("Entity set 'CelestialFrontier_DevContext.Armors'  is null.");
          }
            _context.Armors.Add(armor);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ArmorExists(armor.ArmorGuid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetArmor", new { id = armor.ArmorGuid }, armor);
        }

        // DELETE: api/Armors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArmor(Guid id)
        {
            if (_context.Armors == null)
            {
                return NotFound();
            }
            var armor = await _context.Armors.FindAsync(id);
            if (armor == null)
            {
                return NotFound();
            }

            _context.Armors.Remove(armor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArmorExists(Guid id)
        {
            return (_context.Armors?.Any(e => e.ArmorGuid == id)).GetValueOrDefault();
        }
    }
}
