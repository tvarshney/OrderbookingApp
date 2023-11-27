using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodOrderingApi.Models;

namespace FoodOrderingApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TimingsController : ControllerBase
    {
        private readonly FoodOrderingDbContext _context;

        public TimingsController(FoodOrderingDbContext context)
        {
            _context = context;
        }

        // GET: api/Timings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Timing>>> GetTimings()
        {
          if (_context.Timings == null)
          {
              return NotFound();
          }
            return await _context.Timings.ToListAsync();
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Timing>> GetTiming(Guid id)
        {
          if (_context.Timings == null)
          {
              return NotFound();
          }
            var timing = await _context.Timings.FindAsync(id);

            if (timing == null)
            {
                return NotFound();
            }

            return timing;
        }

        // PUT: api/Timings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTiming(Guid id, Timing timing)
        {
            if (id != timing.TimingId)
            {
                return BadRequest();
            }

            _context.Entry(timing).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TimingExists(id))
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

        // POST: api/Timings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Category>> PostTiming(Timing timing)
        {
          if (_context.Timings == null)
          {
              return Problem("Entity set 'FoodOrderingDbContext.Timings'  is null.");
          }
            _context.Timings.Add(timing);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TimingExists(timing.TimingId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTiming", new { id = timing.TimingId }, timing);
        }

        // DELETE: api/Timings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTiming(Guid id)
        {
            if (_context.Timings == null)
            {
                return NotFound();
            }
            var timing = await _context.Timings.FindAsync(id);
            if (timing == null)
            {
                return NotFound();
            }

            _context.Timings.Remove(timing);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TimingExists(Guid id)
        {
            return (_context.Categories?.Any(e => e.CategoryId == id)).GetValueOrDefault();
        }
    }
}
