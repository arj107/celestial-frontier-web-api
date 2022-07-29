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
    public class BattlecruisersController : ControllerBase
    {
        private readonly CelestialFrontier_DevContext _context;
        private readonly IMapper _mapper;
        private readonly IJWTAuthenticationService _jwtService;

        public BattlecruisersController(CelestialFrontier_DevContext context, IMapper mapper, IJWTAuthenticationService jwtService)
        {
            _context = context;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        // GET: api/Battlecruisers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BattlecruiserDto>>> GetBattlecruisers()
        {
            return _mapper.Map<IEnumerable<Battlecruiser>, List<BattlecruiserDto>>(
                await _context.Battlecruisers
                    .Include(f => f.SpaceshipType)
                    .Include(f => f.SpaceshipCategory)
                    .Include(f => f.PointDefenseSystemPowerLevel)
                    .Include(f => f.WeaponPowerLevel)
                    .ToListAsync()
            );
        }

        // GET: api/Battlecruisers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Battlecruiser>> GetBattlecruiser(Guid id)
        {
            var battlecruiser = await _context.Battlecruisers.FindAsync(id);

            if (battlecruiser == null)
            {
                return NotFound();
            }

            return battlecruiser;
        }

        // PUT: api/Battlecruisers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBattlecruiser(Guid id, Battlecruiser battlecruiser)
        {
            if (id != battlecruiser.BattlecruiserGuid)
            {
                return BadRequest();
            }

            _context.Entry(battlecruiser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BattlecruiserExists(id))
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

        // POST: api/Battlecruisers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Battlecruiser>> PostBattlecruiser(Battlecruiser battlecruiser)
        {
            _context.Battlecruisers.Add(battlecruiser);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BattlecruiserExists(battlecruiser.BattlecruiserGuid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBattlecruiser", new { id = battlecruiser.BattlecruiserGuid }, battlecruiser);
        }

        // DELETE: api/Battlecruisers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBattlecruiser(Guid id)
        {
            var battlecruiser = await _context.Battlecruisers.FindAsync(id);
            if (battlecruiser == null)
            {
                return NotFound();
            }

            _context.Battlecruisers.Remove(battlecruiser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BattlecruiserExists(Guid id)
        {
            return _context.Battlecruisers.Any(e => e.BattlecruiserGuid == id);
        }
    }
}
