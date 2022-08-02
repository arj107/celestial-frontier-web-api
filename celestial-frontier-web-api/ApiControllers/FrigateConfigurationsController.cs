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
    public class FrigateConfigurationsController : ControllerBase
    {
        private readonly CelestialFrontier_DevContext _context;
        private readonly IMapper _mapper;
        private readonly IJWTAuthenticationService _jwtService;

        public FrigateConfigurationsController(CelestialFrontier_DevContext context, IMapper mapper, IJWTAuthenticationService jwtService)
        {
            _context = context;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        // GET: api/FrigateConfigurations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FrigateConfiguration>>> GetFrigateConfigurations()
        {
            return await _context.FrigateConfigurations.ToListAsync();
        }

        // GET: api/User/DestroyerConfigurations
        [HttpGet]
        [Authorize]
        [Route("~/api/User/[controller]")]
        public async Task<ActionResult<IEnumerable<FrigateConfigurationDto>>> GetUserFrigateConfigurations() {
            Guid userGuid = _jwtService.GetUserGuid(HttpContext);

            List<FrigateConfiguration> frigateConfigurations = await _context.FrigateConfigurations.Where(f => f.UserGuid == userGuid)
                .Include(f => f.User)
                .Include("Armor.SpaceshipType")
                .Include("Frigate.PointDefenseSystemPowerLevel")
                .Include("Frigate.SpaceshipCategory")
                .Include("Frigate.SpaceshipType")
                .Include("Frigate.WeaponPowerLevel")
                .Include("FuelSystem.SpaceshipType")
                .Include("Payload.PayloadType")
                .Include("Payload.PowerLevel")
                .Include("Payload.WeaponFrameType")
                .Include("PointDefenseSystem.PowerLevel")
                .Include("Thruster.SpaceshipType")
                .Include("WeaponFrame.PowerLevel")
                .Include("WeaponFrame.WeaponFrameType")
                .ToListAsync();
            List<FrigateConfigurationDto> frigateConfigurationDtos = _mapper.Map<IEnumerable<FrigateConfiguration>, List<FrigateConfigurationDto>>(frigateConfigurations);
            return Ok(frigateConfigurationDtos);
        }

        // GET: api/User/DestroyerConfigurations/{frigateConfigurationGuid}
        [HttpGet]
        [Authorize]
        [Route("~/api/User/[controller]/{frigateConfigurationGuid}")]
        public async Task<ActionResult<FrigateConfigurationDto>> GetUserFrigateConfiguration(Guid frigateConfigurationGuid) {
            Guid userGuid = _jwtService.GetUserGuid(HttpContext);

            FrigateConfiguration frigateConfiguration = await _context.FrigateConfigurations.Where(f => f.FrigateConfigurationGuid == frigateConfigurationGuid)
                .Include(f => f.User)
                .Include("Armor.SpaceshipType")
                .Include("Frigate.PointDefenseSystemPowerLevel")
                .Include("Frigate.SpaceshipCategory")
                .Include("Frigate.SpaceshipType")
                .Include("Frigate.WeaponPowerLevel")
                .Include("FuelSystem.SpaceshipType")
                .Include("Payload.PayloadType")
                .Include("Payload.PowerLevel")
                .Include("Payload.WeaponFrameType")
                .Include("PointDefenseSystem.PowerLevel")
                .Include("Thruster.SpaceshipType")
                .Include("WeaponFrame.PowerLevel")
                .Include("WeaponFrame.WeaponFrameType")

                .FirstOrDefaultAsync(f => f.UserGuid == userGuid && f.FrigateConfigurationGuid == frigateConfigurationGuid);

            if (frigateConfiguration == null) {
                return NotFound();
            }

            FrigateConfigurationDto frigateConfigurationDto = _mapper.Map<FrigateConfiguration, FrigateConfigurationDto>(frigateConfiguration);
            return Ok(frigateConfigurationDto);
        }

        // GET: api/FrigateConfigurations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FrigateConfiguration>> GetFrigateConfiguration(Guid id)
        {
            var frigateConfiguration = await _context.FrigateConfigurations.FindAsync(id);

            if (frigateConfiguration == null)
            {
                return NotFound();
            }

            return frigateConfiguration;
        }

        // PUT: api/FrigateConfigurations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFrigateConfiguration(Guid id, FrigateConfiguration frigateConfiguration)
        {
            if (id != frigateConfiguration.FrigateConfigurationGuid)
            {
                return BadRequest();
            }

            _context.Entry(frigateConfiguration).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FrigateConfigurationExists(id))
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
        [Route("~/api/User/[controller]/{frigateConfigurationGuid}")]
        public async Task<IActionResult> PutUserFrigateConfiguration(
            Guid frigateConfigurationGuid,
            [FromBody] FrigateConfiguration frigateConfiguration
        ) {

            if (frigateConfigurationGuid != frigateConfiguration.FrigateConfigurationGuid) {
                return BadRequest();
            }

            _context.Entry(frigateConfiguration).State = EntityState.Modified;



            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!FrigateConfigurationExists(frigateConfigurationGuid)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/FrigateConfigurations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FrigateConfiguration>> PostFrigateConfiguration(FrigateConfiguration frigateConfiguration)
        {
            _context.FrigateConfigurations.Add(frigateConfiguration);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FrigateConfigurationExists(frigateConfiguration.FrigateConfigurationGuid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFrigateConfiguration", new { id = frigateConfiguration.FrigateConfigurationGuid }, frigateConfiguration);
        }

        // DELETE: api/FrigateConfigurations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFrigateConfiguration(Guid id)
        {
            var frigateConfiguration = await _context.FrigateConfigurations.FindAsync(id);
            if (frigateConfiguration == null)
            {
                return NotFound();
            }

            _context.FrigateConfigurations.Remove(frigateConfiguration);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FrigateConfigurationExists(Guid id)
        {
            return _context.FrigateConfigurations.Any(e => e.FrigateConfigurationGuid == id);
        }
    }
}
