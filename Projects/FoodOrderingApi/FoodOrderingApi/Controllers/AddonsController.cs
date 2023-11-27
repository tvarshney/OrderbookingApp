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
    public class AddonsController : ControllerBase
    {
        private readonly FoodOrderingDbContext _context;

        public AddonsController(FoodOrderingDbContext context)
        {
            _context = context;
        }

        // GET: api/Addons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Addon>>> GetAddons()
        {
          if (_context.Addons == null)
          {
              return NotFound();
          }
            return await _context.Addons.ToListAsync();
        }

        // GET: api/Addons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Addon>> GetAddon(Guid id)
        {
          if (_context.Addons == null)
          {
              return NotFound();
          }
            var addon = await _context.Addons.FindAsync(id);

            if (addon == null)
            {
                return NotFound();
            }

            return addon;
        }

        // PUT: api/Addons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddon(Guid id, Addon addon)
        {
            if (id != addon.AddonId)
            {
                return BadRequest();
            }

            _context.Entry(addon).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddonExists(id))
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

        // POST: api/Addons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Addon>> PostAddon(Addon addon)
        {
          if (_context.Addons == null)
          {
              return Problem("Entity set 'FoodOrderingDbContext.Addons'  is null.");
          }
            _context.Addons.Add(addon);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AddonExists(addon.AddonId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAddon", new { id = addon.AddonId }, addon);
        }

        // DELETE: api/Addons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddon(Guid id)
        {
            if (_context.Addons == null)
            {
                return NotFound();
            }
            var addon = await _context.Addons.FindAsync(id);
            if (addon == null)
            {
                return NotFound();
            }

            _context.Addons.Remove(addon);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AddonExists(Guid id)
        {
            return (_context.Addons?.Any(e => e.AddonId == id)).GetValueOrDefault();
        }
    }
}
