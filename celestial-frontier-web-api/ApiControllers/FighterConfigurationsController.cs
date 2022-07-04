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
    public class FighterConfigurationsController : ControllerBase
    {
        private readonly CelestialFrontier_DevContext _context;

        public FighterConfigurationsController(CelestialFrontier_DevContext context)
        {
            _context = context;
        }

        // GET: api/FighterConfigurations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FighterConfiguration>>> GetFighterConfigurations()
        {
            return await _context.FighterConfigurations.ToListAsync();
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
