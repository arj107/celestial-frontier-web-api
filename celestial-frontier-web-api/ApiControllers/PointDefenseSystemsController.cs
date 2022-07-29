using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using celestial_frontier_web_api.Models;
using celestial_frontier_web_api.Models.DTOs;
using AutoMapper;
using celestial_frontier_web_api.Services;

namespace celestial_frontier_web_api.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PointDefenseSystemsController : ControllerBase
    {
        private readonly CelestialFrontier_DevContext _context;
        private readonly IMapper _mapper;
        private readonly IJWTAuthenticationService _jwtService;

        public PointDefenseSystemsController(CelestialFrontier_DevContext context, IMapper mapper, IJWTAuthenticationService jwtService)
        {
            _context = context;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        // GET: api/PointDefenseSystems?maxPowerLevelGuid={maxPowerLevelGuid}
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PointDefenseSystemDto>>> GetPointDefenseSystems([FromQuery] Guid? maxPowerLevelGuid = null)
        {
            if (_context.PointDefenseSystems == null)
            {
                return NotFound();
            }

            IQueryable<PointDefenseSystem> pointDefenseSystemQueryables = _context.PointDefenseSystems
              .Include(s => s.PowerLevel);

            // if maxPowerLevelGuid parameter is specified, we need to return all PDS entities
            // that have a power level up to the max as specified by maxPowerLevelGuid
            if (maxPowerLevelGuid != null) {
                PowerLevel level3 = _context.PowerLevels.Single(p => p.Text == "Level 3");
                PowerLevel level2 = _context.PowerLevels.Single(p => p.Text == "Level 2");
                PowerLevel level1 = _context.PowerLevels.Single(p => p.Text == "Level 1");

                if (level3.PowerLevelGuid == maxPowerLevelGuid) {
                    // return the entire list of PDS, unfiltered
                } else if (level2.PowerLevelGuid == maxPowerLevelGuid) {
                    // return only level 2 and level 1 PDS
                    pointDefenseSystemQueryables = pointDefenseSystemQueryables.Where(
                        p => p.PowerLevelGuid == level2.PowerLevelGuid || p.PowerLevelGuid == level1.PowerLevelGuid
                    );
                } else if (level1.PowerLevelGuid == maxPowerLevelGuid) {
                    // return only level 1 PDS
                    pointDefenseSystemQueryables = pointDefenseSystemQueryables.Where(
                        p => p.PowerLevelGuid == level1.PowerLevelGuid
                    );
                } else {
                    return NotFound();
                }
            }

            List<PointDefenseSystem> pointDefenseSystemEntities = await pointDefenseSystemQueryables.ToListAsync();
            List<PointDefenseSystemDto> pointDefenseSystemDtos = _mapper.Map<IEnumerable<PointDefenseSystem>, List<PointDefenseSystemDto>>(pointDefenseSystemEntities);

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
              return Problem("Entity set 'CelestialFrontier_DevContext.PointDefenseSystems'  is null.");
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
