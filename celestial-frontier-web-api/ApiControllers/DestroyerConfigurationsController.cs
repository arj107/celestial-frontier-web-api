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
    public class DestroyerConfigurationsController : ControllerBase
    {
        private readonly CelestialFrontier_DevContext _context;

        public DestroyerConfigurationsController(CelestialFrontier_DevContext context)
        {
            _context = context;
        }

        // GET: api/DestroyerConfigurations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DestroyerConfiguration>>> GetDestroyerConfigurations()
        {
            return await _context.DestroyerConfigurations.ToListAsync();
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
