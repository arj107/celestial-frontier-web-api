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
    public class FrigateConfigurationsController : ControllerBase
    {
        private readonly CelestialFrontier_DevContext _context;

        public FrigateConfigurationsController(CelestialFrontier_DevContext context)
        {
            _context = context;
        }

        // GET: api/FrigateConfigurations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FrigateConfiguration>>> GetFrigateConfigurations()
        {
            return await _context.FrigateConfigurations.ToListAsync();
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
