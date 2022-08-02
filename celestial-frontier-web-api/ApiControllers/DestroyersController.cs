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
using celestial_frontier_web_api.Models.DTOs;

namespace celestial_frontier_web_api.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DestroyersController : ControllerBase
    {
        private readonly CelestialFrontier_DevContext _context;
        private readonly IMapper _mapper;
        private readonly IJWTAuthenticationService _jwtService;

        public DestroyersController(CelestialFrontier_DevContext context, IMapper mapper, IJWTAuthenticationService jwtService)
        {
            _context = context;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        // GET: api/Destroyers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DestroyerDto>>> GetDestroyers() {
            return _mapper.Map<IEnumerable<Destroyer>, List<DestroyerDto>>(
                await _context.Destroyers
                    .Include(f => f.SpaceshipType)
                    .Include(f => f.SpaceshipCategory)
                    .Include(f => f.PointDefenseSystemPowerLevel)
                    .Include(f => f.WeaponPowerLevel)
                    .ToListAsync()
            );
        }

        // GET: api/Destroyers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Destroyer>> GetDestroyer(Guid id)
        {
            var destroyer = await _context.Destroyers.FindAsync(id);

            if (destroyer == null)
            {
                return NotFound();
            }

            return destroyer;
        }

        // PUT: api/Destroyers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDestroyer(Guid id, Destroyer destroyer)
        {
            if (id != destroyer.DestroyerGuid)
            {
                return BadRequest();
            }

            _context.Entry(destroyer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DestroyerExists(id))
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

        // POST: api/Destroyers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Destroyer>> PostDestroyer(Destroyer destroyer)
        {
            _context.Destroyers.Add(destroyer);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DestroyerExists(destroyer.DestroyerGuid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDestroyer", new { id = destroyer.DestroyerGuid }, destroyer);
        }

        // DELETE: api/Destroyers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDestroyer(Guid id)
        {
            var destroyer = await _context.Destroyers.FindAsync(id);
            if (destroyer == null)
            {
                return NotFound();
            }

            _context.Destroyers.Remove(destroyer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DestroyerExists(Guid id)
        {
            return _context.Destroyers.Any(e => e.DestroyerGuid == id);
        }
    }
}
