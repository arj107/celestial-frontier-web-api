using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using celestial_frontier_web_api.Models;
using Microsoft.AspNetCore.Authorization;
using celestial_frontier_web_api.Models.DTOs;
using AutoMapper;
using celestial_frontier_web_api.Services;

namespace celestial_frontier_web_api.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BattlecruiserConfigurationsController : ControllerBase
    {
        private readonly CelestialFrontier_DevContext _context;
        private readonly IMapper _mapper;
        private readonly IJWTAuthenticationService _jwtService;

        public BattlecruiserConfigurationsController(CelestialFrontier_DevContext context, IMapper mapper, IJWTAuthenticationService jwtService)
        {
            _context = context;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        // GET: api/BattlecruiserConfigurations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BattlecruiserConfiguration>>> GetBattlecruiserConfigurations()
        {
            return await _context.BattlecruiserConfigurations.ToListAsync();
        }

        // GET: api/User/BattlecruiserConfigurations
        [HttpGet]
        [Authorize]
        [Route("~/api/User/[controller]")]
        public async Task<ActionResult<IEnumerable<BattlecruiserConfigurationDto>>> GetUserBattlecruiserConfigurations() {
            Guid userGuid = _jwtService.GetUserGuid(HttpContext);

            List<BattlecruiserConfiguration> battlecruiserConfigurations = await _context.BattlecruiserConfigurations.Where(f => f.UserGuid == userGuid)
                .Include(f => f.User)
                .Include("Armor.SpaceshipType")
                .Include("Battlecruiser.PointDefenseSystemPowerLevel")
                .Include("Battlecruiser.SpaceshipCategory")
                .Include("Battlecruiser.SpaceshipType")
                .Include("Battlecruiser.WeaponPowerLevel")
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
            List<BattlecruiserConfigurationDto> battlecruiserConfigurationDtos = _mapper.Map<IEnumerable<BattlecruiserConfiguration>, List<BattlecruiserConfigurationDto>>(battlecruiserConfigurations);
            return Ok(battlecruiserConfigurationDtos);
        }

        // GET: api/User/Fleets
        [HttpGet]
        [Authorize]
        [Route("~/api/User/[controller]/{battlecruiserConfigurationGuid}")]
        public async Task<ActionResult<BattlecruiserConfigurationDto>> GetUserBattlecruiserConfiguration(Guid battlecruiserConfigurationGuid) {
            Guid userGuid = _jwtService.GetUserGuid(HttpContext);

            BattlecruiserConfiguration battlecruiserConfiguration = await _context.BattlecruiserConfigurations.Where(f => f.BattlecruiserConfigurationGuid == battlecruiserConfigurationGuid)
                .Include(f => f.User)
                .Include("Armor.SpaceshipType")
                .Include("Battlecruiser.PointDefenseSystemPowerLevel")
                .Include("Battlecruiser.SpaceshipCategory")
                .Include("Battlecruiser.SpaceshipType")
                .Include("Battlecruiser.WeaponPowerLevel")
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

                .FirstOrDefaultAsync(f => f.UserGuid == userGuid && f.BattlecruiserConfigurationGuid == battlecruiserConfigurationGuid);

            if (battlecruiserConfiguration == null) {
                return NotFound();
            }

            BattlecruiserConfigurationDto battlecruiserConfigurationDto = _mapper.Map<BattlecruiserConfiguration, BattlecruiserConfigurationDto>(battlecruiserConfiguration);
            return Ok(battlecruiserConfigurationDto);
        }

        // GET: api/BattlecruiserConfigurations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BattlecruiserConfiguration>> GetBattlecruiserConfiguration(Guid id)
        {
            var battlecruiserConfiguration = await _context.BattlecruiserConfigurations.FindAsync(id);

            if (battlecruiserConfiguration == null)
            {
                return NotFound();
            }

            return battlecruiserConfiguration;
        }

        // PUT: api/BattlecruiserConfigurations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBattlecruiserConfiguration(Guid id, BattlecruiserConfiguration battlecruiserConfiguration)
        {
            if (id != battlecruiserConfiguration.BattlecruiserConfigurationGuid)
            {
                return BadRequest();
            }

            _context.Entry(battlecruiserConfiguration).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BattlecruiserConfigurationExists(id))
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

        // POST: api/BattlecruiserConfigurations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BattlecruiserConfiguration>> PostBattlecruiserConfiguration(BattlecruiserConfiguration battlecruiserConfiguration)
        {
            _context.BattlecruiserConfigurations.Add(battlecruiserConfiguration);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BattlecruiserConfigurationExists(battlecruiserConfiguration.BattlecruiserConfigurationGuid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBattlecruiserConfiguration", new { id = battlecruiserConfiguration.BattlecruiserConfigurationGuid }, battlecruiserConfiguration);
        }

        // DELETE: api/BattlecruiserConfigurations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBattlecruiserConfiguration(Guid id)
        {
            var battlecruiserConfiguration = await _context.BattlecruiserConfigurations.FindAsync(id);
            if (battlecruiserConfiguration == null)
            {
                return NotFound();
            }

            _context.BattlecruiserConfigurations.Remove(battlecruiserConfiguration);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BattlecruiserConfigurationExists(Guid id)
        {
            return _context.BattlecruiserConfigurations.Any(e => e.BattlecruiserConfigurationGuid == id);
        }
    }
}
