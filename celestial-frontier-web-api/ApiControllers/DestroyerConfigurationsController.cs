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
using Microsoft.AspNetCore.Authorization;
using celestial_frontier_web_api.Models.DTOs;

namespace celestial_frontier_web_api.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DestroyerConfigurationsController : ControllerBase
    {
        private readonly CelestialFrontier_DevContext _context;
        private readonly IMapper _mapper;
        private readonly IJWTAuthenticationService _jwtService;

        public DestroyerConfigurationsController(CelestialFrontier_DevContext context, IMapper mapper, IJWTAuthenticationService jwtService)
        {
            _context = context;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        // GET: api/DestroyerConfigurations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DestroyerConfiguration>>> GetDestroyerConfigurations()
        {
            return await _context.DestroyerConfigurations.ToListAsync();
        }

        // GET: api/User/DestroyerConfigurations
        [HttpGet]
        [Authorize]
        [Route("~/api/User/[controller]")]
        public async Task<ActionResult<IEnumerable<DestroyerConfigurationDto>>> GetUserDestroyerConfigurations() {
            Guid userGuid = _jwtService.GetUserGuid(HttpContext);

            List<DestroyerConfiguration> destroyerConfigurations = await _context.DestroyerConfigurations.Where(f => f.UserGuid == userGuid)
                .Include(f => f.User)
                .Include("Armor.SpaceshipType")
                .Include("Destroyer.PointDefenseSystemPowerLevel")
                .Include("Destroyer.SpaceshipCategory")
                .Include("Destroyer.SpaceshipType")
                .Include("Destroyer.WeaponPowerLevel")
                .Include("FuelSystem.SpaceshipType")
                .Include("Payload1.PayloadType")
                .Include("Payload1.PowerLevel")
                .Include("Payload1.WeaponFrameType")
                .Include("Payload2.PayloadType")
                .Include("Payload2.PowerLevel")
                .Include("Payload2.WeaponFrameType")
                .Include("PointDefenseSystem.PowerLevel")
                .Include("Thruster.SpaceshipType")
                .Include("WeaponFrame1.PowerLevel")
                .Include("WeaponFrame1.WeaponFrameType")
                .Include("WeaponFrame2.PowerLevel")
                .Include("WeaponFrame2.WeaponFrameType")
                .ToListAsync();
            List<DestroyerConfigurationDto> destroyerConfigurationDtos = _mapper.Map<IEnumerable<DestroyerConfiguration>, List<DestroyerConfigurationDto>>(destroyerConfigurations);
            return Ok(destroyerConfigurationDtos);
        }

        // GET: api/User/DestroyerConfigurations/{destroyerConfigurationGuid}
        [HttpGet]
        [Authorize]
        [Route("~/api/User/[controller]/{destroyerConfigurationGuid}")]
        public async Task<ActionResult<DestroyerConfigurationDto>> GetUserDestroyerConfiguration(Guid destroyerConfigurationGuid) {
            Guid userGuid = _jwtService.GetUserGuid(HttpContext);

            DestroyerConfiguration destroyerConfiguration = await _context.DestroyerConfigurations.Where(f => f.DestroyerConfigurationGuid == destroyerConfigurationGuid)
                .Include(f => f.User)
                .Include("Armor.SpaceshipType")
                .Include("Destroyer.PointDefenseSystemPowerLevel")
                .Include("Destroyer.SpaceshipCategory")
                .Include("Destroyer.SpaceshipType")
                .Include("Destroyer.WeaponPowerLevel")
                .Include("FuelSystem.SpaceshipType")
                .Include("Payload1.PayloadType")
                .Include("Payload1.PowerLevel")
                .Include("Payload1.WeaponFrameType")
                .Include("Payload2.PayloadType")
                .Include("Payload2.PowerLevel")
                .Include("Payload2.WeaponFrameType")
                .Include("PointDefenseSystem.PowerLevel")
                .Include("Thruster.SpaceshipType")
                .Include("WeaponFrame1.PowerLevel")
                .Include("WeaponFrame1.WeaponFrameType")
                .Include("WeaponFrame2.PowerLevel")
                .Include("WeaponFrame2.WeaponFrameType")

                .FirstOrDefaultAsync(f => f.UserGuid == userGuid && f.DestroyerConfigurationGuid == destroyerConfigurationGuid);

            if (destroyerConfiguration == null) {
                return NotFound();
            }

            DestroyerConfigurationDto destroyerConfigurationDto = _mapper.Map<DestroyerConfiguration, DestroyerConfigurationDto>(destroyerConfiguration);
            return Ok(destroyerConfigurationDto);
        }

        // GET: api/DestroyerConfigurations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DestroyerConfiguration>> GetDestroyerConfiguration(Guid id)
        {
            var destroyerConfiguration = await _context.DestroyerConfigurations.FindAsync(id);

            if (destroyerConfiguration == null)
            {
                return NotFound();
            }

            return destroyerConfiguration;
        }

        // PUT: api/DestroyerConfigurations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDestroyerConfiguration(Guid id, DestroyerConfiguration destroyerConfiguration)
        {
            if (id != destroyerConfiguration.DestroyerConfigurationGuid)
            {
                return BadRequest();
            }

            _context.Entry(destroyerConfiguration).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DestroyerConfigurationExists(id))
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

        // PUT: api/User/DestroyerConfigurations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        [Route("~/api/User/[controller]/{destroyerConfigurationGuid}")]
        public async Task<IActionResult> PutUserDestroyerConfiguration(
            Guid destroyerConfigurationGuid,
            [FromBody] DestroyerConfiguration destroyerConfiguration
        ) {

            if (destroyerConfigurationGuid != destroyerConfiguration.DestroyerConfigurationGuid) {
                return BadRequest();
            }

            _context.Entry(destroyerConfiguration).State = EntityState.Modified;



            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!DestroyerConfigurationExists(destroyerConfigurationGuid)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/DestroyerConfigurations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DestroyerConfiguration>> PostDestroyerConfiguration(DestroyerConfiguration destroyerConfiguration)
        {
            _context.DestroyerConfigurations.Add(destroyerConfiguration);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DestroyerConfigurationExists(destroyerConfiguration.DestroyerConfigurationGuid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDestroyerConfiguration", new { id = destroyerConfiguration.DestroyerConfigurationGuid }, destroyerConfiguration);
        }

        // DELETE: api/DestroyerConfigurations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDestroyerConfiguration(Guid id)
        {
            var destroyerConfiguration = await _context.DestroyerConfigurations.FindAsync(id);
            if (destroyerConfiguration == null)
            {
                return NotFound();
            }

            _context.DestroyerConfigurations.Remove(destroyerConfiguration);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DestroyerConfigurationExists(Guid id)
        {
            return _context.DestroyerConfigurations.Any(e => e.DestroyerConfigurationGuid == id);
        }
    }
}
