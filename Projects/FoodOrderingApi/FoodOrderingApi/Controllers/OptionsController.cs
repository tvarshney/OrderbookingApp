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
    public class OptionsController : ControllerBase
    {
        private readonly FoodOrderingDbContext _context;

        public OptionsController(FoodOrderingDbContext context)
        {
            _context = context;
        }

        // GET: api/Options
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Option>>> GetOptions()
        {
          if (_context.Options == null)
          {
              return NotFound();
          }
            return await _context.Options.ToListAsync();
        }

        // GET: api/Options/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Option>> GetOption(Guid id)
        {
          if (_context.Options == null)
          {
              return NotFound();
          }
            var option = await _context.Options.FindAsync(id);

            if (option == null)
            {
                return NotFound();
            }

            return option;
        }

        // PUT: api/Options/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOption(Guid id, Option option)
        {
            if (id != option.OptionId)
            {
                return BadRequest();
            }

            _context.Entry(option).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OptionExists(id))
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

        // POST: api/Options
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Option>> PostOption(Option option)
        {
          if (_context.Options == null)
          {
              return Problem("Entity set 'FoodOrderingDbContext.Options'  is null.");
          }
            _context.Options.Add(option);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OptionExists(option.OptionId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOption", new { id = option.OptionId }, option);
        }

        // DELETE: api/Options/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOption(Guid id)
        {
            if (_context.Options == null)
            {
                return NotFound();
            }
            var option = await _context.Options.FindAsync(id);
            if (option == null)
            {
                return NotFound();
            }

            _context.Options.Remove(option);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OptionExists(Guid id)
        {
            return (_context.Options?.Any(e => e.OptionId == id)).GetValueOrDefault();
        }
    }
}
