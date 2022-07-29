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
    public class WeaponFramesController : ControllerBase
    {
        private readonly CelestialFrontier_DevContext _context;
        private readonly IMapper _mapper;
        private readonly IJWTAuthenticationService _jwtService;

        public WeaponFramesController(CelestialFrontier_DevContext context, IMapper mapper, IJWTAuthenticationService jwtService)
        {
            _context = context;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        // GET: api/WeaponFrames
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeaponFrameDto>>> GetWeaponFrames()
        {
            if (_context.WeaponFrames == null)
            {
                return NotFound();
            }

            List<WeaponFrame> weaponFrameEntities = await _context.WeaponFrames
              .Include(s => s.WeaponFrameType)
              .Include(s => s.PowerLevel)
              .ToListAsync();

            List<WeaponFrameDto> weaponFrameDtos = _mapper.Map<IEnumerable<WeaponFrame>, List<WeaponFrameDto>>(weaponFrameEntities);

            return weaponFrameDtos;
        }

        // GET: api/WeaponFrames/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WeaponFrame>> GetWeaponFrame(Guid id)
        {
          if (_context.WeaponFrames == null)
          {
              return NotFound();
          }
            var weaponFrame = await _context.WeaponFrames.FindAsync(id);

            if (weaponFrame == null)
            {
                return NotFound();
            }

            return weaponFrame;
        }

        // PUT: api/WeaponFrames/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWeaponFrame(Guid id, WeaponFrame weaponFrame)
        {
            if (id != weaponFrame.WeaponFrameGuid)
            {
                return BadRequest();
            }

            _context.Entry(weaponFrame).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WeaponFrameExists(id))
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

        // POST: api/WeaponFrames
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WeaponFrame>> PostWeaponFrame(WeaponFrame weaponFrame)
        {
          if (_context.WeaponFrames == null)
          {
              return Problem("Entity set 'CelestialFrontier_DevContext.WeaponFrames'  is null.");
          }
            _context.WeaponFrames.Add(weaponFrame);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (WeaponFrameExists(weaponFrame.WeaponFrameGuid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetWeaponFrame", new { id = weaponFrame.WeaponFrameGuid }, weaponFrame);
        }

        // DELETE: api/WeaponFrames/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWeaponFrame(Guid id)
        {
            if (_context.WeaponFrames == null)
            {
                return NotFound();
            }
            var weaponFrame = await _context.WeaponFrames.FindAsync(id);
            if (weaponFrame == null)
            {
                return NotFound();
            }

            _context.WeaponFrames.Remove(weaponFrame);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WeaponFrameExists(Guid id)
        {
            return (_context.WeaponFrames?.Any(e => e.WeaponFrameGuid == id)).GetValueOrDefault();
        }
    }
}
