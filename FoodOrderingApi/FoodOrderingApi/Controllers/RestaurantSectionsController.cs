using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodOrderingApi.Models;
using NuGet.Packaging;

namespace FoodOrderingApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RestaurantSectionsController : ControllerBase
    {
        private readonly FoodOrderingDbContext _context;

        public RestaurantSectionsController(FoodOrderingDbContext context)
        {
            _context = context;
        }

        // GET: api/RestaurantSections
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantSection>>> GetRestaurantSections()
        {          
            if (_context.RestaurantSections == null)
            {
                return NotFound();
            }
            return await _context.RestaurantSections.ToListAsync();
        }

        // GET: api/RestaurantSections/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RestaurantSection>> GetRestaurantSection(Guid id)
        {
          if (_context.RestaurantSections == null)
          {
              return NotFound();
          }
            var restaurantSection = await _context.RestaurantSections.FindAsync(id);

            if (restaurantSection == null)
            {
                return NotFound();
            }

            return restaurantSection;
        }

        // PUT: api/RestaurantSections/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestaurantSection(Guid id, RestaurantSection restaurantSection)
        {
            if (id != restaurantSection.RestaurantSectionId)
            {
                return BadRequest();
            }

            _context.Entry(restaurantSection).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestaurantSectionExists(id))
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

        // POST: api/RestaurantSections
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RestaurantSection>> PostRestaurantSection(RestaurantSection restaurantSection)
        {
          if (_context.RestaurantSections == null)
          {
              return Problem("Entity set 'FoodOrderingDbContext.RestaurantSections'  is null.");
          }
            _context.RestaurantSections.Add(restaurantSection);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RestaurantSectionExists(restaurantSection.RestaurantSectionId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRestaurantSection", new { id = restaurantSection.RestaurantSectionId }, restaurantSection);
        }

        // DELETE: api/RestaurantSections/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurantSection(Guid id)
        {
            if (_context.RestaurantSections == null)
            {
                return NotFound();
            }
            var restaurantSection = await _context.RestaurantSections.FindAsync(id);
            if (restaurantSection == null)
            {
                return NotFound();
            }

            _context.RestaurantSections.Remove(restaurantSection);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RestaurantSectionExists(Guid id)
        {
            return (_context.RestaurantSections?.Any(e => e.RestaurantSectionId == id)).GetValueOrDefault();
        }
    }
}
