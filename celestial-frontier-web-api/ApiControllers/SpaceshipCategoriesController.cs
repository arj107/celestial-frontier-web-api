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
    public class SpaceshipCategoriesController : ControllerBase
    {
        private readonly CelestialFrontier_DevContext _context;

        public SpaceshipCategoriesController(CelestialFrontier_DevContext context)
        {
            _context = context;
        }

        // GET: api/SpaceshipCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpaceshipCategoryDto>>> GetSpaceshipCategories()
        {
            if (_context.SpaceshipCategories == null)
            {
                return NotFound();
            }

            List<SpaceshipCategory> spaceshipCategoryEntities = await _context.SpaceshipCategories
              .ToListAsync();

            List<SpaceshipCategoryDto> spaceshipCategoryDtos = new List<SpaceshipCategoryDto>();
            foreach (SpaceshipCategory spaceshipCategory in spaceshipCategoryEntities) {
                spaceshipCategoryDtos.Add(new SpaceshipCategoryDto(spaceshipCategory));
            }

            return spaceshipCategoryDtos;
        }

        // GET: api/SpaceshipCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SpaceshipCategory>> GetSpaceshipCategory(Guid id)
        {
          if (_context.SpaceshipCategories == null)
          {
              return NotFound();
          }
            var spaceshipCategory = await _context.SpaceshipCategories.FindAsync(id);

            if (spaceshipCategory == null)
            {
                return NotFound();
            }

            return spaceshipCategory;
        }

        // PUT: api/SpaceshipCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpaceshipCategory(Guid id, SpaceshipCategory spaceshipCategory)
        {
            if (id != spaceshipCategory.SpaceshipCategoryGuid)
            {
                return BadRequest();
            }

            _context.Entry(spaceshipCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpaceshipCategoryExists(id))
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

        // POST: api/SpaceshipCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SpaceshipCategory>> PostSpaceshipCategory(SpaceshipCategory spaceshipCategory)
        {
          if (_context.SpaceshipCategories == null)
          {
              return Problem("Entity set 'CelestialFrontier_DevContext.SpaceshipCategories'  is null.");
          }
            _context.SpaceshipCategories.Add(spaceshipCategory);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SpaceshipCategoryExists(spaceshipCategory.SpaceshipCategoryGuid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSpaceshipCategory", new { id = spaceshipCategory.SpaceshipCategoryGuid }, spaceshipCategory);
        }

        // DELETE: api/SpaceshipCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpaceshipCategory(Guid id)
        {
            if (_context.SpaceshipCategories == null)
            {
                return NotFound();
            }
            var spaceshipCategory = await _context.SpaceshipCategories.FindAsync(id);
            if (spaceshipCategory == null)
            {
                return NotFound();
            }

            _context.SpaceshipCategories.Remove(spaceshipCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SpaceshipCategoryExists(Guid id)
        {
            return (_context.SpaceshipCategories?.Any(e => e.SpaceshipCategoryGuid == id)).GetValueOrDefault();
        }
    }
}
