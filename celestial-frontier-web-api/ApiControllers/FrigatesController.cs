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
    public class FrigatesController : ControllerBase
    {
        private readonly CelestialFrontier_DevContext _context;
        private readonly IMapper _mapper;
        private readonly IJWTAuthenticationService _jwtService;

        public FrigatesController(CelestialFrontier_DevContext context, IMapper mapper, IJWTAuthenticationService jwtService)
        {
            _context = context;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        // GET: api/Frigates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Frigate>>> GetFrigates()
        {
            return await _context.Frigates
                    .Include(f => f.SpaceshipType)
                    .Include(f => f.SpaceshipCategory)
                    .Include(f => f.PointDefenseSystemPowerLevel)
                    .Include(f => f.WeaponPowerLevel)
                    .ToListAsync();
        }

        // GET: api/Frigates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Frigate>> GetFrigate(Guid id)
        {
            var frigate = await _context.Frigates.FindAsync(id);

            if (frigate == null)
            {
                return NotFound();
            }

            return frigate;
        }

        // PUT: api/Frigates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFrigate(Guid id, Frigate frigate)
        {
            if (id != frigate.FrigateGuid)
            {
                return BadRequest();
            }

            _context.Entry(frigate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FrigateExists(id))
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

        // POST: api/Frigates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Frigate>> PostFrigate(Frigate frigate)
        {
            _context.Frigates.Add(frigate);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FrigateExists(frigate.FrigateGuid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFrigate", new { id = frigate.FrigateGuid }, frigate);
        }

        // DELETE: api/Frigates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFrigate(Guid id)
        {
            var frigate = await _context.Frigates.FindAsync(id);
            if (frigate == null)
            {
                return NotFound();
            }

            _context.Frigates.Remove(frigate);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FrigateExists(Guid id)
        {
            return _context.Frigates.Any(e => e.FrigateGuid == id);
        }
    }
}
