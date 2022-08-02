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
    public class FightersController : ControllerBase
    {
        private readonly CelestialFrontier_DevContext _context;
        private readonly IMapper _mapper;
        private readonly IJWTAuthenticationService _jwtService;

        public FightersController(CelestialFrontier_DevContext context, IMapper mapper, IJWTAuthenticationService jwtService)
        {
            _context = context;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        // GET: api/Fighters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fighter>>> GetFighters()
        {
            return await _context.Fighters
                    .Include(f => f.SpaceshipType)
                    .Include(f => f.SpaceshipCategory)
                    .Include(f => f.PointDefenseSystemPowerLevel)
                    .Include(f => f.WeaponPowerLevel)
                    .ToListAsync();
        }

        // GET: api/Fighters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Fighter>> GetFighter(Guid id)
        {
            var fighter = await _context.Fighters.FindAsync(id);

            if (fighter == null)
            {
                return NotFound();
            }

            return fighter;
        }

        // PUT: api/Fighters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFighter(Guid id, Fighter fighter)
        {
            if (id != fighter.FighterGuid)
            {
                return BadRequest();
            }

            _context.Entry(fighter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FighterExists(id))
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

        // POST: api/Fighters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Fighter>> PostFighter(Fighter fighter)
        {
            _context.Fighters.Add(fighter);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FighterExists(fighter.FighterGuid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFighter", new { id = fighter.FighterGuid }, fighter);
        }

        // DELETE: api/Fighters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFighter(Guid id)
        {
            var fighter = await _context.Fighters.FindAsync(id);
            if (fighter == null)
            {
                return NotFound();
            }

            _context.Fighters.Remove(fighter);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FighterExists(Guid id)
        {
            return _context.Fighters.Any(e => e.FighterGuid == id);
        }
    }
}
