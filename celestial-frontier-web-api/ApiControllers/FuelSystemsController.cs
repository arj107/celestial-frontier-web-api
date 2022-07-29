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
    public class FuelSystemsController : ControllerBase
    {
        private readonly CelestialFrontier_DevContext _context;
        private readonly IMapper _mapper;
        private readonly IJWTAuthenticationService _jwtService;

        public FuelSystemsController(CelestialFrontier_DevContext context, IMapper mapper, IJWTAuthenticationService jwtService)
        {
            _context = context;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        // GET: api/FuelSystems?spaceshipTypeGuid={spaceshipTypeGuid}
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FuelSystemDto>>> GetFuelSystems([FromQuery] Guid? spaceshipTypeGuid = null)
        {
            if (_context.FuelSystems == null) {
                return NotFound();
            }

            IQueryable<FuelSystem> fuelSystemQueryables = _context.FuelSystems
              .Include(s => s.SpaceshipType);

            if (spaceshipTypeGuid != null) {
                fuelSystemQueryables = fuelSystemQueryables.Where(s => s.SpaceshipTypeGuid == spaceshipTypeGuid);
            }

            List<FuelSystem> fuelSystemEntities = await fuelSystemQueryables.ToListAsync();
            List<FuelSystemDto> fuelSystemDtos = _mapper.Map<IEnumerable<FuelSystem>, List<FuelSystemDto>>(fuelSystemEntities);

            return fuelSystemDtos;
        }

        // GET: api/FuelSystems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FuelSystem>> GetFuelSystem(Guid id)
        {
          if (_context.FuelSystems == null)
          {
              return NotFound();
          }
            var fuelSystem = await _context.FuelSystems.FindAsync(id);

            if (fuelSystem == null)
            {
                return NotFound();
            }

            return fuelSystem;
        }

        // PUT: api/FuelSystems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFuelSystem(Guid id, FuelSystem fuelSystem)
        {
            if (id != fuelSystem.FuelSystemGuid)
            {
                return BadRequest();
            }

            _context.Entry(fuelSystem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FuelSystemExists(id))
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

        // POST: api/FuelSystems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FuelSystem>> PostFuelSystem(FuelSystem fuelSystem)
        {
          if (_context.FuelSystems == null)
          {
              return Problem("Entity set 'CelestialFrontier_DevContext.FuelSystems'  is null.");
          }
            _context.FuelSystems.Add(fuelSystem);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FuelSystemExists(fuelSystem.FuelSystemGuid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFuelSystem", new { id = fuelSystem.FuelSystemGuid }, fuelSystem);
        }

        // DELETE: api/FuelSystems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFuelSystem(Guid id)
        {
            if (_context.FuelSystems == null)
            {
                return NotFound();
            }
            var fuelSystem = await _context.FuelSystems.FindAsync(id);
            if (fuelSystem == null)
            {
                return NotFound();
            }

            _context.FuelSystems.Remove(fuelSystem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FuelSystemExists(Guid id)
        {
            return (_context.FuelSystems?.Any(e => e.FuelSystemGuid == id)).GetValueOrDefault();
        }
    }
}
