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
using Microsoft.AspNetCore.Authorization;

namespace celestial_frontier_web_api.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CorvetteConfigurationsController : ControllerBase
    {
        private readonly CelestialFrontier_DevContext _context;
        private readonly IMapper _mapper;
        private readonly IJWTAuthenticationService _jwtService;

        public CorvetteConfigurationsController(CelestialFrontier_DevContext context, IMapper mapper, IJWTAuthenticationService jwtService) 
        {
            _context = context;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        // GET: api/CorvetteConfigurations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CorvetteConfiguration>>> GetCorvetteConfigurations()
        {
            return await _context.CorvetteConfigurations.ToListAsync();
        }

        // GET: api/User/CorvetteConfigurations
        [HttpGet]
        [Authorize]
        [Route("~/api/User/[controller]")]
        public async Task<ActionResult<IEnumerable<CorvetteConfigurationDto>>> GetUserCorvetteConfigurations() {
            Guid userGuid = _jwtService.GetUserGuid(HttpContext);

            List<CorvetteConfiguration> corrvetteConfigurations = await _context.CorvetteConfigurations.Where(f => f.UserGuid == userGuid)
                .Include(f => f.User)
                .Include("Armor.SpaceshipType")
                .Include("Corvette.PointDefenseSystemPowerLevel")
                .Include("Corvette.SpaceshipCategory")
                .Include("Corvette.SpaceshipType")
                .Include("Corvette.WeaponPowerLevel")
                .Include("FuelSystem.SpaceshipType")
                .Include("Payload.PayloadType")
                .Include("Payload.PowerLevel")
                .Include("Payload.WeaponFrameType")
                .Include("PointDefenseSystem.PowerLevel")
                .Include("Thruster.SpaceshipType")
                .Include("WeaponFrame.PowerLevel")
                .Include("WeaponFrame.WeaponFrameType")
                .ToListAsync();
            List<CorvetteConfigurationDto> corvetteConfigurationDtos = _mapper.Map<IEnumerable<CorvetteConfiguration>, List<CorvetteConfigurationDto>>(corrvetteConfigurations);
            return Ok(corvetteConfigurationDtos);
        }

        // GET: api/User/CorvetteConfigurations/{corvetteConfigurationGuid}
        [HttpGet]
        [Authorize]
        [Route("~/api/User/[controller]/{corvetteConfigurationGuid}")]
        public async Task<ActionResult<CorvetteConfigurationDto>> GetUserCorvetteConfiguration(Guid corvetteConfigurationGuid) {
            Guid userGuid = _jwtService.GetUserGuid(HttpContext);

            CorvetteConfiguration corvetteConfiguration = await _context.CorvetteConfigurations.Where(f => f.CorvetteConfigurationGuid == corvetteConfigurationGuid)
                .Include(f => f.User)
                .Include("Armor.SpaceshipType")
                .Include("Corvette.PointDefenseSystemPowerLevel")
                .Include("Corvette.SpaceshipCategory")
                .Include("Corvette.SpaceshipType")
                .Include("Corvette.WeaponPowerLevel")
                .Include("FuelSystem.SpaceshipType")
                .Include("Payload.PayloadType")
                .Include("Payload.PowerLevel")
                .Include("Payload.WeaponFrameType")
                .Include("PointDefenseSystem.PowerLevel")
                .Include("Thruster.SpaceshipType")
                .Include("WeaponFrame.PowerLevel")
                .Include("WeaponFrame.WeaponFrameType")

                .FirstOrDefaultAsync(f => f.UserGuid == userGuid && f.CorvetteConfigurationGuid == corvetteConfigurationGuid);

            if (corvetteConfiguration == null) {
                return NotFound();
            }

            CorvetteConfigurationDto corvetteConfigurationDto = _mapper.Map<CorvetteConfiguration, CorvetteConfigurationDto>(corvetteConfiguration);
            return Ok(corvetteConfigurationDto);
        }

        // GET: api/CorvetteConfigurations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CorvetteConfiguration>> GetCorvetteConfiguration(Guid id)
        {
            var corvetteConfiguration = await _context.CorvetteConfigurations.FindAsync(id);

            if (corvetteConfiguration == null)
            {
                return NotFound();
            }

            return corvetteConfiguration;
        }

        // PUT: api/CorvetteConfigurations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCorvetteConfiguration(Guid id, CorvetteConfiguration corvetteConfiguration)
        {
            if (id != corvetteConfiguration.CorvetteConfigurationGuid)
            {
                return BadRequest();
            }

            _context.Entry(corvetteConfiguration).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CorvetteConfigurationExists(id))
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

        // PUT: api/User/FrigateConfigurations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        [Route("~/api/User/[controller]/{corvetteConfigurationGuid}")]
        public async Task<IActionResult> PutUserCorvetteConfiguration(
            Guid corvetteConfigurationGuid,
            [FromBody] CorvetteConfiguration corvetteConfiguration
        ) {

            if (corvetteConfigurationGuid != corvetteConfiguration.CorvetteConfigurationGuid) {
                return BadRequest();
            }

            _context.Entry(corvetteConfiguration).State = EntityState.Modified;



            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!CorvetteConfigurationExists(corvetteConfigurationGuid)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CorvetteConfigurations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CorvetteConfiguration>> PostCorvetteConfiguration(CorvetteConfiguration corvetteConfiguration)
        {
            _context.CorvetteConfigurations.Add(corvetteConfiguration);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CorvetteConfigurationExists(corvetteConfiguration.CorvetteConfigurationGuid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCorvetteConfiguration", new { id = corvetteConfiguration.CorvetteConfigurationGuid }, corvetteConfiguration);
        }

        // DELETE: api/CorvetteConfigurations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCorvetteConfiguration(Guid id)
        {
            var corvetteConfiguration = await _context.CorvetteConfigurations.FindAsync(id);
            if (corvetteConfiguration == null)
            {
                return NotFound();
            }

            _context.CorvetteConfigurations.Remove(corvetteConfiguration);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CorvetteConfigurationExists(Guid id)
        {
            return _context.CorvetteConfigurations.Any(e => e.CorvetteConfigurationGuid == id);
        }
    }
}
