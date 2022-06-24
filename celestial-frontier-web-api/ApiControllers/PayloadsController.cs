using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using celestial_frontier_web_api.Models;
using celestial_frontier_web_api.Models.DTOs;

namespace celestial_frontier_web_api.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayloadsController : ControllerBase
    {
        private readonly CelestialFrontierContext _context;

        public PayloadsController(CelestialFrontierContext context)
        {
            _context = context;
        }

        // GET: api/Payloads
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PayloadDto>>> GetPayloads()
        {
            if (_context.Payloads == null)
            {
                return NotFound();
            }

            List<Payload> payloadEntities = await _context.Payloads
              .Include(s => s.PayloadType)
              .Include(s => s.WeaponFrameType)
              .Include(s => s.PowerLevel)
              .ToListAsync();

            List<PayloadDto> payloadDtos = new List<PayloadDto>();
            foreach (Payload payload in payloadEntities) {
                payloadDtos.Add(new PayloadDto(payload));
            }

            return payloadDtos;
        }

        // GET: api/Payloads/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Payload>> GetPayload(Guid id)
        {
          if (_context.Payloads == null)
          {
              return NotFound();
          }
            var payload = await _context.Payloads.FindAsync(id);

            if (payload == null)
            {
                return NotFound();
            }

            return payload;
        }

        // PUT: api/Payloads/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayload(Guid id, Payload payload)
        {
            if (id != payload.PayloadGuid)
            {
                return BadRequest();
            }

            _context.Entry(payload).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PayloadExists(id))
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

        // POST: api/Payloads
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Payload>> PostPayload(Payload payload)
        {
          if (_context.Payloads == null)
          {
              return Problem("Entity set 'CelestialFrontierContext.Payloads'  is null.");
          }
            _context.Payloads.Add(payload);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PayloadExists(payload.PayloadGuid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPayload", new { id = payload.PayloadGuid }, payload);
        }

        // DELETE: api/Payloads/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayload(Guid id)
        {
            if (_context.Payloads == null)
            {
                return NotFound();
            }
            var payload = await _context.Payloads.FindAsync(id);
            if (payload == null)
            {
                return NotFound();
            }

            _context.Payloads.Remove(payload);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PayloadExists(Guid id)
        {
            return (_context.Payloads?.Any(e => e.PayloadGuid == id)).GetValueOrDefault();
        }
    }
}
