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
    public class CorvetteConfigurationsController : ControllerBase
    {
        private readonly CelestialFrontier_DevContext _context;

        public CorvetteConfigurationsController(CelestialFrontier_DevContext context)
        {
            _context = context;
        }

        // GET: api/CorvetteConfigurations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CorvetteConfiguration>>> GetCorvetteConfigurations()
        {
            return await _context.CorvetteConfigurations.ToListAsync();
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
