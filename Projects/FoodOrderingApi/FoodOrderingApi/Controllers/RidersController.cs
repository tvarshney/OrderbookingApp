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
    public class RidersController : ControllerBase
    {
        private readonly FoodOrderingDbContext _context;

        public RidersController(FoodOrderingDbContext context)
        {
            _context = context;
        }

        // GET: api/Riders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rider>>> GetRiders()
        {
          if (_context.Riders == null)
          {
              return NotFound();
          }
            return await _context.Riders.ToListAsync();
        }

        // GET: api/Riders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rider>> GetRider(Guid id)
        {
          if (_context.Riders == null)
          {
              return NotFound();
          }
            var rider = await _context.Riders.FindAsync(id);

            if (rider == null)
            {
                return NotFound();
            }

            return rider;
        }

        // PUT: api/Riders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRider(Guid id, Rider rider)
        {
            if (id != rider.RiderId)
            {
                return BadRequest();
            }

            _context.Entry(rider).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RiderExists(id))
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

        // POST: api/Riders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Rider>> PostRider(Rider rider)
        {
          if (_context.Riders == null)
          {
              return Problem("Entity set 'FoodOrderingDbContext.Riders'  is null.");
          }
            _context.Riders.Add(rider);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RiderExists(rider.RiderId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRider", new { id = rider.RiderId }, rider);
        }

        // DELETE: api/Riders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRider(Guid id)
        {
            if (_context.Riders == null)
            {
                return NotFound();
            }
            var rider = await _context.Riders.FindAsync(id);
            if (rider == null)
            {
                return NotFound();
            }

            _context.Riders.Remove(rider);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RiderExists(Guid id)
        {
            return (_context.Riders?.Any(e => e.RiderId == id)).GetValueOrDefault();
        }
    }
}
