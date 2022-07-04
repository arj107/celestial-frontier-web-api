using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using celestial_frontier_web_api.Models;

namespace celestial_frontier_web_api.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrierConfigurationsController : ControllerBase
    {
        private readonly CelestialFrontier_DevContext _context;

        public CarrierConfigurationsController(CelestialFrontier_DevContext context)
        {
            _context = context;
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
