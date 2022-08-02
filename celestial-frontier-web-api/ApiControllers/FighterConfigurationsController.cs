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
    public class FighterConfigurationsController : ControllerBase
    {
        private readonly CelestialFrontier_DevContext _context;
        private readonly IMapper _mapper;
        private readonly IJWTAuthenticationService _jwtService;

        public FighterConfigurationsController(CelestialFrontier_DevContext context, IMapper mapper, IJWTAuthenticationService jwtService)
        {
            _context = context;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        // GET: api/FighterConfigurations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FighterConfiguration>>> GetFighterConfigurations()
        {
            return await _context.FighterConfigurations.ToListAsync();
        }

        // GET: api/User/FighterConfigurations
        [HttpGet]
        [Authorize]
        [Route("~/api/User/[controller]")]
        public async Task<ActionResult<IEnumerable<FighterConfigurationDto>>> GetUserFighterConfigurations() {
            Guid userGuid = _jwtService.GetUserGuid(HttpContext);

            List<FighterConfiguration> fighterConfigurations = await _context.FighterConfigurations.Where(f => f.UserGuid == userGuid)
                .Include(f => f.User)
                .Include("Armor.SpaceshipType")
                .Include("Fighter.PointDefenseSystemPowerLevel")
                .Include("Fighter.SpaceshipCategory")
                .Include("Fighter.SpaceshipType")
                .Include("Fighter.WeaponPowerLevel")
                .Include("FuelSystem.SpaceshipType")
                .Include("Payload.PayloadType")
                .Include("Payload.PowerLevel")
                .Include("Payload.WeaponFrameType")
                .Include("PointDefenseSystem.PowerLevel")
                .Include("Thruster.SpaceshipType")
                .Include("WeaponFrame.PowerLevel")
                .Include("WeaponFrame.WeaponFrameType")
                .ToListAsync();
            List<FighterConfigurationDto> fighterConfigurationDtos = _mapper.Map<IEnumerable<FighterConfiguration>, List<FighterConfigurationDto>>(fighterConfigurations);
            return Ok(fighterConfigurationDtos);
        }

        // GET: api/User/DestroyerConfigurations/{frigateConfigurationGuid}
        [HttpGet]
        [Authorize]
        [Route("~/api/User/[controller]/{fighterConfigurationGuid}")]
        public async Task<ActionResult<FighterConfigurationDto>> GetUserFighterConfiguration(Guid fighterConfigurationGuid) {
            Guid userGuid = _jwtService.GetUserGuid(HttpContext);

            FighterConfiguration fighterConfiguration = await _context.FighterConfigurations.Where(f => f.FighterConfigurationGuid == fighterConfigurationGuid)
                .Include(f => f.User)
                .Include("Armor.SpaceshipType")
                .Include("Fighter.PointDefenseSystemPowerLevel")
                .Include("Fighter.SpaceshipCategory")
                .Include("Fighter.SpaceshipType")
                .Include("Fighter.WeaponPowerLevel")
                .Include("FuelSystem.SpaceshipType")
                .Include("Payload.PayloadType")
                .Include("Payload.PowerLevel")
                .Include("Payload.WeaponFrameType")
                .Include("PointDefenseSystem.PowerLevel")
                .Include("Thruster.SpaceshipType")
                .Include("WeaponFrame.PowerLevel")
                .Include("WeaponFrame.WeaponFrameType")

                .FirstOrDefaultAsync(f => f.UserGuid == userGuid && f.FighterConfigurationGuid == fighterConfigurationGuid);

            if (fighterConfiguration == null) {
                return NotFound();
            }

            FighterConfigurationDto fighterConfigurationDto = _mapper.Map<FighterConfiguration, FighterConfigurationDto>(fighterConfiguration);
            return Ok(fighterConfigurationDto);
        }

        // GET: api/FighterConfigurations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FighterConfiguration>> GetFighterConfiguration(Guid id)
        {
            var fighterConfiguration = await _context.FighterConfigurations.FindAsync(id);

            if (fighterConfiguration == null)
            {
                return NotFound();
            }

            return fighterConfiguration;
        }

        // PUT: api/FighterConfigurations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFighterConfiguration(Guid id, FighterConfiguration fighterConfiguration)
        {
            if (id != fighterConfiguration.FighterConfigurationGuid)
            {
                return BadRequest();
            }

            _context.Entry(fighterConfiguration).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FighterConfigurationExists(id))
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

        // PUT: api/User/FighterConfigurations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        [Route("~/api/User/[controller]/{fighterConfigurationGuid}")]
        public async Task<IActionResult> PutUserFighterConfiguration(
            Guid fighterConfigurationGuid,
            [FromBody] FighterConfiguration fighterConfiguration
        ) {

            if (fighterConfigurationGuid != fighterConfiguration.FighterConfigurationGuid) {
                return BadRequest();
            }

            _context.Entry(fighterConfiguration).State = EntityState.Modified;



            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!FighterConfigurationExists(fighterConfigurationGuid)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/FighterConfigurations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FighterConfiguration>> PostFighterConfiguration(FighterConfiguration fighterConfiguration)
        {
            _context.FighterConfigurations.Add(fighterConfiguration);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FighterConfigurationExists(fighterConfiguration.FighterConfigurationGuid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFighterConfiguration", new { id = fighterConfiguration.FighterConfigurationGuid }, fighterConfiguration);
        }

        // DELETE: api/FighterConfigurations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFighterConfiguration(Guid id)
        {
            var fighterConfiguration = await _context.FighterConfigurations.FindAsync(id);
            if (fighterConfiguration == null)
            {
                return NotFound();
            }

            _context.FighterConfigurations.Remove(fighterConfiguration);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FighterConfigurationExists(Guid id)
        {
            return _context.FighterConfigurations.Any(e => e.FighterConfigurationGuid == id);
        }
    }
}
