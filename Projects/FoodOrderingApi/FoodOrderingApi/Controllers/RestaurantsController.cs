using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodOrderingApi.Models;
using NuGet.Packaging;
using NuGet.Packaging.Signing;

namespace FoodOrderingApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly FoodOrderingDbContext _context;

        public RestaurantsController(FoodOrderingDbContext context)
        {
            _context = context;
        }

        // GET: api/Restaurants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetRestaurants()
        {
          if (_context.Restaurants == null)
          {
              return NotFound();
          }
          var restaurantData = await _context.Restaurants.ToListAsync();
            if(restaurantData != null)
            {
                foreach(var restaurant  in restaurantData) 
                { 
                    var foods = _context.Foods.Where(f => f.RestaurantId == restaurant.RestaurantId).ToList();
                    if(foods != null)
                    {
                        foreach (var food in foods)
                        {
                            restaurant.Foods.Add(food);
                        }
                    }
                    var categories = _context.Categories.Where(c => c.RestaurantId == restaurant.RestaurantId).ToList();
                    if (categories != null)
                    {
                        foreach (var category in categories)
                        {
                            if (!restaurant.Categories.Any(c => c.RestaurantId == category.RestaurantId))
                            {
                                restaurant.Categories.Add(category);
                            }
                        }
                    }
                }
                return restaurantData;
            }            
            return NotFound();
        }

        // GET: api/Restaurants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Restaurant>> GetRestaurant(Guid id)
        {
          if (_context.Restaurants == null)
          {
              return NotFound();
          }
            var restaurantData = await _context.Restaurants.FindAsync(id);

            if (restaurantData != null)
            {
                var foods = _context.Foods.Where(f => f.RestaurantId == restaurantData.RestaurantId).ToList();
                if (foods != null)
                {
                    // Clear the existing collection of foods if needed
                    restaurantData.Foods.Clear();

                    // Add each food individually to the collection
                    foreach (var food in foods)
                    {
                        restaurantData.Foods.Add(food);
                    }
                }                
                var categories = _context.Categories.Where(c => c.RestaurantId == restaurantData.RestaurantId).ToList();
                if (categories != null)
                {
                    foreach (var category in categories)
                    {
                        if (!restaurantData.Categories.Any(c => c.RestaurantId == category.RestaurantId))
                        {
                            restaurantData.Categories.Add(category);
                        }
                    }
                }
                var orders = _context.Orders.Where(o => o.RestaurantId == restaurantData.RestaurantId).ToList();
                if (orders != null)
                {
                    foreach (var order in orders)
                    {
                        if (!restaurantData.Orders.Any(o => o.RestaurantId == order.RestaurantId))
                        {
                            restaurantData.Orders.Add(order);
                        }
                    }
                }
                var options = _context.Options.Where(opt => opt.RestaurantId == restaurantData.RestaurantId).ToList();
                if (options != null)
                {
                    foreach (var option in options)
                    {
                        if (!restaurantData.Options.Any(opt => opt.RestaurantId == option.RestaurantId))
                        {
                            restaurantData.Options.Add(option);
                        }
                    }
                }
                var addons = _context.Addons.Where(add => add.RestaurantId == restaurantData.RestaurantId).ToList();
                if (addons != null)
                {
                    foreach (var addon in addons)
                    {
                        if (!restaurantData.Addons.Any(add => add.RestaurantId == addon.RestaurantId))
                        {
                            restaurantData.Addons.Add(addon);
                        }
                    }
                }
                var timings = _context.Timings.Where(t => t.RestaurantId == restaurantData.RestaurantId).ToList();
                if (timings != null)
                {
                    foreach (var timing in timings)
                    {
                        if (!restaurantData.Timings.Any(t => t.RestaurantId == timing.RestaurantId))
                        {
                            restaurantData.Timings.Add(timing);
                        }
                    }
                }
                return restaurantData;
            }
            return NotFound();
        }

        // PUT: api/Restaurants/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestaurant(Guid id, Restaurant restaurant)
        {
            if (id != restaurant.RestaurantId)
            {
                return BadRequest();
            }

            _context.Entry(restaurant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestaurantExists(id))
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

        // POST: api/Restaurants
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Restaurant>> PostRestaurant(Restaurant restaurant)
        {
          if (_context.Restaurants == null)
          {
              return Problem("Entity set 'FoodOrderingDbContext.Restaurants'  is null.");
          }
            _context.Restaurants.Add(restaurant);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RestaurantExists(restaurant.RestaurantId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRestaurant", new { id = restaurant.RestaurantId }, restaurant);
        }

        // DELETE: api/Restaurants/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant(Guid id)
        {
            if (_context.Restaurants == null)
            {
                return NotFound();
            }
            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RestaurantExists(Guid id)
        {
            return (_context.Restaurants?.Any(e => e.RestaurantId == id)).GetValueOrDefault();
        }
    }
}
