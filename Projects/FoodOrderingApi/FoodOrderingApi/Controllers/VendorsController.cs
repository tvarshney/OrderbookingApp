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
    public class VendorsController : ControllerBase
    {
        private readonly FoodOrderingDbContext _context;

        public VendorsController(FoodOrderingDbContext context)
        {
            _context = context;
        }

        // GET: api/Vendors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vendor>>> GetVendors()
        {
          if (_context.Vendors == null)
          {
              return NotFound();
          }
          List<Vendor> venderData = new List<Vendor>();
          venderData = await _context.Vendors.ToListAsync();
            List<Restaurant> restaurants = new List<Restaurant>();
            foreach(var vendor in venderData) { 
                var restaurantList = _context.Restaurants.Where(r => r.VendorId == vendor.VendorId).ToList();
                vendor.Restaurants.AddRange(restaurantList);
            }
            return venderData;
        }

        // GET: api/Vendors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vendor>> GetVendor(Guid id)
        {
          if (_context.Vendors == null)
          {
              return NotFound();
          }
            
            Vendor vendor = new Vendor();
            vendor = await _context.Vendors.FindAsync(id);
            if (vendor == null)
            {
                return NotFound();
            }
            List<Restaurant> restaurants = new List<Restaurant>();
                var restaurantList = _context.Restaurants.Where(r => r.VendorId == vendor.VendorId).ToList();
                vendor.Restaurants.AddRange(restaurantList);
            return vendor;
        }

        // PUT: api/Vendors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVendor(Guid id, Vendor vendor)
        {
            if (id != vendor.VendorId)
            {
                return BadRequest();
            }

            _context.Entry(vendor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendorExists(id))
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

        // POST: api/Vendors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Vendor>> PostVendor(Vendor vendor)
        {
          if (_context.Vendors == null)
          {
              return Problem("Entity set 'FoodOrderingDbContext.Vendors'  is null.");
          }
            _context.Vendors.Add(vendor);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (VendorExists(vendor.VendorId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetVendor", new { id = vendor.VendorId }, vendor);
        }

        // DELETE: api/Vendors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVendor(Guid id)
        {
            if (_context.Vendors == null)
            {
                return NotFound();
            }
            var vendor = await _context.Vendors.FindAsync(id);
            if (vendor == null)
            {
                return NotFound();
            }

            _context.Vendors.Remove(vendor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VendorExists(Guid id)
        {
            return (_context.Vendors?.Any(e => e.VendorId == id)).GetValueOrDefault();
        }
    }
}
