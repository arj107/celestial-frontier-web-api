using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using celestial_frontier_web_api.Models;
using AutoMapper;
using celestial_frontier_web_api.Services;

namespace celestial_frontier_web_api.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CorvettesController : ControllerBase
    {
        private readonly CelestialFrontier_DevContext _context;
        private readonly IMapper _mapper;
        private readonly IJWTAuthenticationService _jwtService;

        public CorvettesController(CelestialFrontier_DevContext context, IMapper mapper, IJWTAuthenticationService jwtService)
        {
            _context = context;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        // GET: api/Corvettes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Corvette>>> GetCorvettes()
        {
            return await _context.Corvettes
                    .Include(f => f.SpaceshipType)
                    .Include(f => f.SpaceshipCategory)
                    .Include(f => f.PointDefenseSystemPowerLevel)
                    .Include(f => f.WeaponPowerLevel)
                    .ToListAsync();
        }

        // GET: api/Corvettes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Corvette>> GetCorvette(Guid id)
        {
            var corvette = await _context.Corvettes.FindAsync(id);

            if (corvette == null)
            {
                return NotFound();
            }

            return corvette;
        }

        // PUT: api/Corvettes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCorvette(Guid id, Corvette corvette)
        {
            if (id != corvette.CorvetteGuid)
            {
                return BadRequest();
            }

            _context.Entry(corvette).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CorvetteExists(id))
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

        // POST: api/Corvettes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Corvette>> PostCorvette(Corvette corvette)
        {
            _context.Corvettes.Add(corvette);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CorvetteExists(corvette.CorvetteGuid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCorvette", new { id = corvette.CorvetteGuid }, corvette);
        }

        // DELETE: api/Corvettes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCorvette(Guid id)
        {
            var corvette = await _context.Corvettes.FindAsync(id);
            if (corvette == null)
            {
                return NotFound();
            }

            _context.Corvettes.Remove(corvette);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CorvetteExists(Guid id)
        {
            return _context.Corvettes.Any(e => e.CorvetteGuid == id);
        }
    }
}
