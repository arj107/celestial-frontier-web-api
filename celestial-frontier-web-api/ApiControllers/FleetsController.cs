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
                .Include("FleetBattlecruisers.BattlecruiserConfiguration.Armor.SpaceshipType")
                .Include("FleetBattlecruisers.BattlecruiserConfiguration.Battlecruiser.PointDefenseSystemPowerLevel")
                .Include("FleetBattlecruisers.BattlecruiserConfiguration.Battlecruiser.SpaceshipCategory")
                .Include("FleetBattlecruisers.BattlecruiserConfiguration.Battlecruiser.SpaceshipType")
                .Include("FleetBattlecruisers.BattlecruiserConfiguration.Battlecruiser.WeaponPowerLevel")
                .Include("FleetBattlecruisers.BattlecruiserConfiguration.FuelSystem.SpaceshipType")
                .Include("FleetBattlecruisers.BattlecruiserConfiguration.Payload1.PayloadType")
                .Include("FleetBattlecruisers.BattlecruiserConfiguration.Payload1.PowerLevel")
                .Include("FleetBattlecruisers.BattlecruiserConfiguration.Payload1.WeaponFrameType")
                .Include("FleetBattlecruisers.BattlecruiserConfiguration.Payload2.PayloadType")
                .Include("FleetBattlecruisers.BattlecruiserConfiguration.Payload2.PowerLevel")
                .Include("FleetBattlecruisers.BattlecruiserConfiguration.Payload2.WeaponFrameType")
                .Include("FleetBattlecruisers.BattlecruiserConfiguration.PointDefenseSystem.PowerLevel")
                .Include("FleetBattlecruisers.BattlecruiserConfiguration.Thruster.SpaceshipType")
                .Include("FleetBattlecruisers.BattlecruiserConfiguration.WeaponFrame1.PowerLevel")
                .Include("FleetBattlecruisers.BattlecruiserConfiguration.WeaponFrame1.WeaponFrameType")
                .Include("FleetBattlecruisers.BattlecruiserConfiguration.WeaponFrame2.PowerLevel")
                .Include("FleetBattlecruisers.BattlecruiserConfiguration.WeaponFrame2.WeaponFrameType")

                .Include("FleetCarriers.CarrierConfiguration.Armor.SpaceshipType")
                .Include("FleetCarriers.CarrierConfiguration.Carrier.PointDefenseSystemPowerLevel")
                .Include("FleetCarriers.CarrierConfiguration.Carrier.SpaceshipCategory")
                .Include("FleetCarriers.CarrierConfiguration.Carrier.SpaceshipType")
                .Include("FleetCarriers.CarrierConfiguration.Carrier.WeaponPowerLevel")
                .Include("FleetCarriers.CarrierConfiguration.FuelSystem.SpaceshipType")
                .Include("FleetCarriers.CarrierConfiguration.Payload.PayloadType")
                .Include("FleetCarriers.CarrierConfiguration.Payload.PowerLevel")
                .Include("FleetCarriers.CarrierConfiguration.Payload.WeaponFrameType")
                .Include("FleetCarriers.CarrierConfiguration.PointDefenseSystem.PowerLevel")
                .Include("FleetCarriers.CarrierConfiguration.Thruster.SpaceshipType")
                .Include("FleetCarriers.CarrierConfiguration.WeaponFrame.PowerLevel")
                .Include("FleetCarriers.CarrierConfiguration.WeaponFrame.WeaponFrameType")

                .Include("FleetCorvettes.CorvetteConfiguration.Armor.SpaceshipType")
                .Include("FleetCorvettes.CorvetteConfiguration.Corvette.PointDefenseSystemPowerLevel")
                .Include("FleetCorvettes.CorvetteConfiguration.Corvette.SpaceshipCategory")
                .Include("FleetCorvettes.CorvetteConfiguration.Corvette.SpaceshipType")
                .Include("FleetCorvettes.CorvetteConfiguration.Corvette.WeaponPowerLevel")
                .Include("FleetCorvettes.CorvetteConfiguration.FuelSystem.SpaceshipType")
                .Include("FleetCorvettes.CorvetteConfiguration.Payload.PayloadType")
                .Include("FleetCorvettes.CorvetteConfiguration.Payload.PowerLevel")
                .Include("FleetCorvettes.CorvetteConfiguration.Payload.WeaponFrameType")
                .Include("FleetCorvettes.CorvetteConfiguration.PointDefenseSystem.PowerLevel")
                .Include("FleetCorvettes.CorvetteConfiguration.Thruster.SpaceshipType")
                .Include("FleetCorvettes.CorvetteConfiguration.WeaponFrame.PowerLevel")
                .Include("FleetCorvettes.CorvetteConfiguration.WeaponFrame.WeaponFrameType")

                .Include("FleetFrigates.FrigateConfiguration.Armor.SpaceshipType")
                .Include("FleetFrigates.FrigateConfiguration.Frigate.PointDefenseSystemPowerLevel")
                .Include("FleetFrigates.FrigateConfiguration.Frigate.SpaceshipCategory")
                .Include("FleetFrigates.FrigateConfiguration.Frigate.SpaceshipType")
                .Include("FleetFrigates.FrigateConfiguration.Frigate.WeaponPowerLevel")
                .Include("FleetFrigates.FrigateConfiguration.FuelSystem.SpaceshipType")
                .Include("FleetFrigates.FrigateConfiguration.Payload.PayloadType")
                .Include("FleetFrigates.FrigateConfiguration.Payload.PowerLevel")
                .Include("FleetFrigates.FrigateConfiguration.Payload.WeaponFrameType")
                .Include("FleetFrigates.FrigateConfiguration.PointDefenseSystem.PowerLevel")
                .Include("FleetFrigates.FrigateConfiguration.Thruster.SpaceshipType")
                .Include("FleetFrigates.FrigateConfiguration.WeaponFrame.PowerLevel")
                .Include("FleetFrigates.FrigateConfiguration.WeaponFrame.WeaponFrameType")

                .Include("FleetFighters.FighterConfiguration.Armor.SpaceshipType")
                .Include("FleetFighters.FighterConfiguration.Fighter.PointDefenseSystemPowerLevel")
                .Include("FleetFighters.FighterConfiguration.Fighter.SpaceshipCategory")
                .Include("FleetFighters.FighterConfiguration.Fighter.SpaceshipType")
                .Include("FleetFighters.FighterConfiguration.Fighter.WeaponPowerLevel")
                .Include("FleetFighters.FighterConfiguration.FuelSystem.SpaceshipType")
                .Include("FleetFighters.FighterConfiguration.Payload.PayloadType")
                .Include("FleetFighters.FighterConfiguration.Payload.PowerLevel")
                .Include("FleetFighters.FighterConfiguration.Payload.WeaponFrameType")
                .Include("FleetFighters.FighterConfiguration.PointDefenseSystem.PowerLevel")
                .Include("FleetFighters.FighterConfiguration.Thruster.SpaceshipType")
                .Include("FleetFighters.FighterConfiguration.WeaponFrame.PowerLevel")
                .Include("FleetFighters.FighterConfiguration.WeaponFrame.WeaponFrameType")

                .Include("FleetDestroyers.DestroyerConfiguration.Armor.SpaceshipType")
                .Include("FleetDestroyers.DestroyerConfiguration.Destroyer.PointDefenseSystemPowerLevel")
                .Include("FleetDestroyers.DestroyerConfiguration.Destroyer.SpaceshipCategory")
                .Include("FleetDestroyers.DestroyerConfiguration.Destroyer.SpaceshipType")
                .Include("FleetDestroyers.DestroyerConfiguration.Destroyer.WeaponPowerLevel")
                .Include("FleetDestroyers.DestroyerConfiguration.FuelSystem.SpaceshipType")
                .Include("FleetDestroyers.DestroyerConfiguration.Payload1.PayloadType")
                .Include("FleetDestroyers.DestroyerConfiguration.Payload1.PowerLevel")
                .Include("FleetDestroyers.DestroyerConfiguration.Payload1.WeaponFrameType")
                .Include("FleetDestroyers.DestroyerConfiguration.Payload2.PayloadType")
                .Include("FleetDestroyers.DestroyerConfiguration.Payload2.PowerLevel")
                .Include("FleetDestroyers.DestroyerConfiguration.Payload2.WeaponFrameType")
                .Include("FleetDestroyers.DestroyerConfiguration.PointDefenseSystem.PowerLevel")
                .Include("FleetDestroyers.DestroyerConfiguration.Thruster.SpaceshipType")
                .Include("FleetDestroyers.DestroyerConfiguration.WeaponFrame1.PowerLevel")
                .Include("FleetDestroyers.DestroyerConfiguration.WeaponFrame1.WeaponFrameType")
                .Include("FleetDestroyers.DestroyerConfiguration.WeaponFrame2.PowerLevel")
                .Include("FleetDestroyers.DestroyerConfiguration.WeaponFrame2.WeaponFrameType")


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
