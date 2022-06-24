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
    public class PayloadTypesController : ControllerBase
    {
        private readonly CelestialFrontierContext _context;

        public PayloadTypesController(CelestialFrontierContext context)
        {
            _context = context;
        }

        // GET: api/PayloadTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PayloadTypeDto>>> GetPayloadTypes()
        {
            if (_context.PayloadTypes == null)
            {
                return NotFound();
            }

            List<PayloadType> payloadTypeEntities = await _context.PayloadTypes
              .ToListAsync();

            List<PayloadTypeDto> payloadTypeDtos = new List<PayloadTypeDto>();
            foreach (PayloadType payloadType in payloadTypeEntities) {
                payloadTypeDtos.Add(new PayloadTypeDto(payloadType));
            }

            return payloadTypeDtos;
        }

        // GET: api/PayloadTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PayloadType>> GetPayloadType(Guid id)
        {
          if (_context.PayloadTypes == null)
          {
              return NotFound();
          }
            var payloadType = await _context.PayloadTypes.FindAsync(id);

            if (payloadType == null)
            {
                return NotFound();
            }

            return payloadType;
        }

        // PUT: api/PayloadTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayloadType(Guid id, PayloadType payloadType)
        {
            if (id != payloadType.PayloadTypeGuid)
            {
                return BadRequest();
            }

            _context.Entry(payloadType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PayloadTypeExists(id))
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

        // POST: api/PayloadTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PowerLevel>> PostPayloadType(PayloadType payloadType)
        {
          if (_context.PayloadTypes == null)
          {
              return Problem("Entity set 'CelestialFrontierContext.PayloadTypes'  is null.");
          }
            _context.PayloadTypes.Add(payloadType);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PayloadTypeExists(payloadType.PayloadTypeGuid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPayloadType", new { id = payloadType.PayloadTypeGuid }, payloadType);
        }

        // DELETE: api/PayloadTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayloadType(Guid id)
        {
            if (_context.PayloadTypes == null)
            {
                return NotFound();
            }
            var payloadType = await _context.PayloadTypes.FindAsync(id);
            if (payloadType == null)
            {
                return NotFound();
            }

            _context.PayloadTypes.Remove(payloadType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PayloadTypeExists(Guid id)
        {
            return (_context.PayloadTypes?.Any(e => e.PayloadTypeGuid == id)).GetValueOrDefault();
        }
    }
}
