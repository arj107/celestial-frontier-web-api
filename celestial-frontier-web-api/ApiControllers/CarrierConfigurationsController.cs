using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using celestial_frontier_web_api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using celestial_frontier_web_api.Models.DTOs;
using celestial_frontier_web_api.Services;

namespace celestial_frontier_web_api.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrierConfigurationsController : ControllerBase
    {
        private readonly CelestialFrontier_DevContext _context;
        private readonly IMapper _mapper;
        private readonly IJWTAuthenticationService _jwtService;

        public CarrierConfigurationsController(CelestialFrontier_DevContext context, IMapper mapper, IJWTAuthenticationService jwtService) {
            _context = context;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        // GET: api/User/CarrierConfigurations
        [HttpGet]
        [Authorize]
        [Route("~/api/User/[controller]")]
        public async Task<ActionResult<IEnumerable<CarrierConfigurationDto>>> GetUserCarrierConfigurations() {
            Guid userGuid = _jwtService.GetUserGuid(HttpContext);

            List<CarrierConfiguration> carrierConfigurations = await _context.CarrierConfigurations.Where(f => f.UserGuid == userGuid)
                .Include(f => f.User)
                .Include("Armor.SpaceshipType")
                .Include("Carrier.PointDefenseSystemPowerLevel")
                .Include("Carrier.SpaceshipCategory")
                .Include("Carrier.SpaceshipType")
                .Include("Carrier.WeaponPowerLevel")
                .Include("FuelSystem.SpaceshipType")
                .Include("Payload.PayloadType")
                .Include("Payload.PowerLevel")
                .Include("Payload.WeaponFrameType")
                .Include("PointDefenseSystem.PowerLevel")
                .Include("Thruster.SpaceshipType")
                .Include("WeaponFrame.PowerLevel")
                .Include("WeaponFrame.WeaponFrameType")
                .ToListAsync();
            List<CarrierConfigurationDto> carrierConfigurationDtos = _mapper.Map<IEnumerable<CarrierConfiguration>, List<CarrierConfigurationDto>>(carrierConfigurations);
            return Ok(carrierConfigurationDtos);
        }

        // GET: api/User/DestroyerConfigurations/{carrierConfigurationGuid}
        [HttpGet]
        [Authorize]
        [Route("~/api/User/[controller]/{carrierConfigurationGuid}")]
        public async Task<ActionResult<CarrierConfigurationDto>> GetUserCarrierConfiguration(Guid carrierConfigurationGuid) {
            Guid userGuid = _jwtService.GetUserGuid(HttpContext);

            CarrierConfiguration carrierConfiguration = await _context.CarrierConfigurations.Where(f => f.CarrierConfigurationGuid == carrierConfigurationGuid)
                .Include(f => f.User)
                .Include("Armor.SpaceshipType")
                .Include("Carrier.PointDefenseSystemPowerLevel")
                .Include("Carrier.SpaceshipCategory")
                .Include("Carrier.SpaceshipType")
                .Include("Carrier.WeaponPowerLevel")
                .Include("FuelSystem.SpaceshipType")
                .Include("Payload.PayloadType")
                .Include("Payload.PowerLevel")
                .Include("Payload.WeaponFrameType")
                .Include("PointDefenseSystem.PowerLevel")
                .Include("Thruster.SpaceshipType")
                .Include("WeaponFrame.PowerLevel")
                .Include("WeaponFrame.WeaponFrameType")

                .FirstOrDefaultAsync(f => f.UserGuid == userGuid && f.CarrierConfigurationGuid == carrierConfigurationGuid);

            if (carrierConfiguration == null) {
                return NotFound();
            }

            CarrierConfigurationDto carrierConfigurationDto = _mapper.Map<CarrierConfiguration, CarrierConfigurationDto>(carrierConfiguration);
            return Ok(carrierConfigurationDto);
        }

        // GET: api/CarrierConfigurations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarrierConfiguration>>> GetCarrierConfigurations()
        {
            return await _context.CarrierConfigurations.ToListAsync();
        }

        // GET: api/CarrierConfigurations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarrierConfiguration>> GetCarrierConfiguration(Guid id)
        {
            var carrierConfiguration = await _context.CarrierConfigurations.FindAsync(id);

            if (carrierConfiguration == null)
            {
                return NotFound();
            }

            return carrierConfiguration;
        }

        // PUT: api/CarrierConfigurations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarrierConfiguration(Guid id, CarrierConfiguration carrierConfiguration)
        {
            if (id != carrierConfiguration.CarrierConfigurationGuid)
            {
                return BadRequest();
            }

            _context.Entry(carrierConfiguration).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarrierConfigurationExists(id))
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

        // PUT: api/User/CarrierConfigurations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        [Route("~/api/User/[controller]/{carrierConfigurationGuid}")]
        public async Task<IActionResult> PutUserCarrierConfiguration(
            Guid carrierConfigurationGuid,
            [FromBody] CarrierConfiguration carrierConfiguration
        ) {

            if (carrierConfigurationGuid != carrierConfiguration.CarrierConfigurationGuid) {
                return BadRequest();
            }

            _context.Entry(carrierConfiguration).State = EntityState.Modified;



            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!CarrierConfigurationExists(carrierConfigurationGuid)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CarrierConfigurations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CarrierConfiguration>> PostCarrierConfiguration(CarrierConfiguration carrierConfiguration)
        {
            _context.CarrierConfigurations.Add(carrierConfiguration);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CarrierConfigurationExists(carrierConfiguration.CarrierConfigurationGuid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCarrierConfiguration", new { id = carrierConfiguration.CarrierConfigurationGuid }, carrierConfiguration);
        }

        // DELETE: api/CarrierConfigurations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarrierConfiguration(Guid id)
        {
            var carrierConfiguration = await _context.CarrierConfigurations.FindAsync(id);
            if (carrierConfiguration == null)
            {
                return NotFound();
            }

            _context.CarrierConfigurations.Remove(carrierConfiguration);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarrierConfigurationExists(Guid id)
        {
            return _context.CarrierConfigurations.Any(e => e.CarrierConfigurationGuid == id);
        }
    }
}
