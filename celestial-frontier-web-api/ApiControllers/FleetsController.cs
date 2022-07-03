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
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using celestial_frontier_web_api.Services;
using System.Net;

namespace celestial_frontier_web_api.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FleetsController : ControllerBase
    {
        private readonly CelestialFrontier_DevContext _context;
        private readonly IMapper _mapper;
        private readonly IJWTAuthenticationService _jwtService;

        public FleetsController(CelestialFrontier_DevContext context, IMapper mapper, IJWTAuthenticationService jwtService)
        {
            _context = context;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        // GET: api/Fleets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FleetDto>>> GetFleets()
        {
            return _mapper.Map<IEnumerable<Fleet>, List<FleetDto>>(
                await _context.Fleets
                    .Include(f=> f.User)
                    .ToListAsync()
            );
        }

        // GET: api/User/Fleets
        [HttpGet]
        [Authorize]
        [Route("~/api/User/[controller]")]
        public async Task<ActionResult<IEnumerable<FleetDto>>> GetUserFleets() {
            Guid userGuid = _jwtService.GetUserGuid(HttpContext);

            List<Fleet> fleets = await _context.Fleets.Where(f => f.UserGuid == userGuid).Include(f => f.User).ToListAsync();
            List<FleetDto> fleetDtos = _mapper.Map<IEnumerable<Fleet>, List<FleetDto>>(fleets);
            return Ok(fleetDtos);
        }
        // GET: api/User/Fleets
        [HttpGet]
        [Authorize]
        [Route("~/api/User/[controller]/{fleetGuid}")]
        public async Task<ActionResult<FleetDto>> GetUserFleet(Guid fleetGuid) {
            Guid userGuid = _jwtService.GetUserGuid(HttpContext);

            Fleet fleet = await _context.Fleets
                .Include(f=> f.User)
                .Include("FleetBattlecruisers.Armor.SpaceshipType")
                .Include("FleetBattlecruisers.Battlecruiser.PointDefenseSystemPowerLevel")
                .Include("FleetBattlecruisers.Battlecruiser.SpaceshipCategory")
                .Include("FleetBattlecruisers.Battlecruiser.SpaceshipType")
                .Include("FleetBattlecruisers.Battlecruiser.WeaponPowerLevel")
                .Include("FleetBattlecruisers.FuelSystem.SpaceshipType")
                .Include("FleetBattlecruisers.Payload1.PayloadType")
                .Include("FleetBattlecruisers.Payload1.PowerLevel")
                .Include("FleetBattlecruisers.Payload1.WeaponFrameType")
                .Include("FleetBattlecruisers.Payload2.PayloadType")
                .Include("FleetBattlecruisers.Payload2.PowerLevel")
                .Include("FleetBattlecruisers.Payload2.WeaponFrameType")
                .Include("FleetBattlecruisers.PointDefenseSystem.PowerLevel")
                .Include("FleetBattlecruisers.Thruster.SpaceshipType")
                .Include("FleetBattlecruisers.WeaponFrame1.PowerLevel")
                .Include("FleetBattlecruisers.WeaponFrame1.WeaponFrameType")
                .Include("FleetBattlecruisers.WeaponFrame2.PowerLevel")
                .Include("FleetBattlecruisers.WeaponFrame2.WeaponFrameType")

                .Include("FleetCarriers.Armor.SpaceshipType")
                .Include("FleetCarriers.Carrier.PointDefenseSystemPowerLevel")
                .Include("FleetCarriers.Carrier.SpaceshipCategory")
                .Include("FleetCarriers.Carrier.SpaceshipType")
                .Include("FleetCarriers.Carrier.WeaponPowerLevel")
                .Include("FleetCarriers.FuelSystem.SpaceshipType")
                .Include("FleetCarriers.Payload.PayloadType")
                .Include("FleetCarriers.Payload.PowerLevel")
                .Include("FleetCarriers.Payload.WeaponFrameType")
                .Include("FleetCarriers.PointDefenseSystem.PowerLevel")
                .Include("FleetCarriers.Thruster.SpaceshipType")
                .Include("FleetCarriers.WeaponFrame.PowerLevel")
                .Include("FleetCarriers.WeaponFrame.WeaponFrameType")

                .Include("FleetCorvettes.Armor.SpaceshipType")
                .Include("FleetCorvettes.Corvette.PointDefenseSystemPowerLevel")
                .Include("FleetCorvettes.Corvette.SpaceshipCategory")
                .Include("FleetCorvettes.Corvette.SpaceshipType")
                .Include("FleetCorvettes.Corvette.WeaponPowerLevel")
                .Include("FleetCorvettes.FuelSystem.SpaceshipType")
                .Include("FleetCorvettes.Payload.PayloadType")
                .Include("FleetCorvettes.Payload.PowerLevel")
                .Include("FleetCorvettes.Payload.WeaponFrameType")
                .Include("FleetCorvettes.PointDefenseSystem.PowerLevel")
                .Include("FleetCorvettes.Thruster.SpaceshipType")
                .Include("FleetCorvettes.WeaponFrame.PowerLevel")
                .Include("FleetCorvettes.WeaponFrame.WeaponFrameType")

                .Include("FleetFrigates.Armor.SpaceshipType")
                .Include("FleetFrigates.Frigate.PointDefenseSystemPowerLevel")
                .Include("FleetFrigates.Frigate.SpaceshipCategory")
                .Include("FleetFrigates.Frigate.SpaceshipType")
                .Include("FleetFrigates.Frigate.WeaponPowerLevel")
                .Include("FleetFrigates.FuelSystem.SpaceshipType")
                .Include("FleetFrigates.Payload.PayloadType")
                .Include("FleetFrigates.Payload.PowerLevel")
                .Include("FleetFrigates.Payload.WeaponFrameType")
                .Include("FleetFrigates.PointDefenseSystem.PowerLevel")
                .Include("FleetFrigates.Thruster.SpaceshipType")
                .Include("FleetFrigates.WeaponFrame.PowerLevel")
                .Include("FleetFrigates.WeaponFrame.WeaponFrameType")

                .Include("FleetFighters.Armor.SpaceshipType")
                .Include("FleetFighters.Fighter.PointDefenseSystemPowerLevel")
                .Include("FleetFighters.Fighter.SpaceshipCategory")
                .Include("FleetFighters.Fighter.SpaceshipType")
                .Include("FleetFighters.Fighter.WeaponPowerLevel")
                .Include("FleetFighters.FuelSystem.SpaceshipType")
                .Include("FleetFighters.Payload.PayloadType")
                .Include("FleetFighters.Payload.PowerLevel")
                .Include("FleetFighters.Payload.WeaponFrameType")
                .Include("FleetFighters.PointDefenseSystem.PowerLevel")
                .Include("FleetFighters.Thruster.SpaceshipType")
                .Include("FleetFighters.WeaponFrame.PowerLevel")
                .Include("FleetFighters.WeaponFrame.WeaponFrameType")

                .Include("FleetDestroyers.Armor.SpaceshipType")
                .Include("FleetDestroyers.Destroyer.PointDefenseSystemPowerLevel")
                .Include("FleetDestroyers.Destroyer.SpaceshipCategory")
                .Include("FleetDestroyers.Destroyer.SpaceshipType")
                .Include("FleetDestroyers.Destroyer.WeaponPowerLevel")
                .Include("FleetDestroyers.FuelSystem.SpaceshipType")
                .Include("FleetDestroyers.Payload1.PayloadType")
                .Include("FleetDestroyers.Payload1.PowerLevel")
                .Include("FleetDestroyers.Payload1.WeaponFrameType")
                .Include("FleetDestroyers.Payload2.PayloadType")
                .Include("FleetDestroyers.Payload2.PowerLevel")
                .Include("FleetDestroyers.Payload2.WeaponFrameType")
                .Include("FleetDestroyers.PointDefenseSystem.PowerLevel")
                .Include("FleetDestroyers.Thruster.SpaceshipType")
                .Include("FleetDestroyers.WeaponFrame1.PowerLevel")
                .Include("FleetDestroyers.WeaponFrame1.WeaponFrameType")
                .Include("FleetDestroyers.WeaponFrame2.PowerLevel")
                .Include("FleetDestroyers.WeaponFrame2.WeaponFrameType")


                .FirstOrDefaultAsync(f => f.UserGuid == userGuid && f.FleetGuid == fleetGuid);

            if (fleet == null) {
                return NotFound();
            }

            FleetDto fleetDto = _mapper.Map<Fleet, FleetDto>(fleet);
            return Ok(fleetDto);
        }

        // GET: api/Fleets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Fleet>> GetFleet(Guid id)
        {
            var fleet = await _context.Fleets.FindAsync(id);

            if (fleet == null)
            {
                return NotFound();
            }

            return fleet;
        }

        // PUT: api/Fleets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFleet(Guid id, Fleet fleet)
        {
            if (id != fleet.FleetGuid)
            {
                return BadRequest();
            }

            _context.Entry(fleet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FleetExists(id))
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

        // POST: api/Fleets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Fleet>> PostFleet(Fleet fleet)
        {
            _context.Fleets.Add(fleet);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FleetExists(fleet.FleetGuid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFleet", new { id = fleet.FleetGuid }, fleet);
        }

        // POST: api/Fleets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        [Route("~/api/User/[controller]")]
        public async Task<ActionResult<Fleet>> PostUserFleet(Fleet fleet) {
            Guid userGuid = _jwtService.GetUserGuid(HttpContext);
            fleet.UserGuid = userGuid;
            fleet.FleetGuid = Guid.NewGuid();
            fleet.CrewCapacity = 2000;

            _context.Fleets.Add(fleet);
            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateException) {
                if (FleetExists(fleet.FleetGuid)) {
                    return Conflict();
                } else {
                    throw;
                }
            }

            return CreatedAtAction("GetFleet", new {
                id = fleet.FleetGuid
            }, fleet);
        }

        // DELETE: api/Fleets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFleet(Guid id)
        {
            var fleet = await _context.Fleets.FindAsync(id);
            if (fleet == null)
            {
                return NotFound();
            }

            _context.Fleets.Remove(fleet);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FleetExists(Guid id)
        {
            return _context.Fleets.Any(e => e.FleetGuid == id);
        }
    }
}
