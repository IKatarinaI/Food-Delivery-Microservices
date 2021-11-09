using MenuMicroservice.DBAccess;
using MenuMicroservice.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenuMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodsController : ControllerBase
    {
        private readonly MenuContext _context;

        public FoodsController(MenuContext context)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
        }

        [HttpGet]
        public async Task<IActionResult> GetFoods()
        {
            var menu = await _context.Foods.ToListAsync();
            return Ok(menu);
        }

        [HttpGet("/sortedbyrating")]
        public async Task<IActionResult> GetSortedFoods()
        {
            var menu = await _context.Foods.OrderBy(x => x.Rating).ToListAsync();

            return Ok(menu);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFoodsDetails(int id)
        {
            var food = await _context.Foods.FirstOrDefaultAsync(x => x.Id == id);

            if (food is null)
                return NotFound($"Food with provided id {id} cannot be found");

            return Ok(food);
        }

        [HttpPost]
        public async Task<IActionResult> AddFood(Food food)
        {
            await _context.Foods.AddAsync(food);
            _ = _context.SaveChangesAsync();
            return Ok($"Created food {food.Name}");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFood(int id)
        {
            var food = await _context.Foods.FirstOrDefaultAsync(x => x.Id == id);

            if (food is null)
                return NotFound($"Food with provided id {id} cannot be found");

            _context.Foods.Remove(food);
            _ = _context.SaveChangesAsync();

            return Ok($"Food {food.Name} has been deleted successfully");
        }

        [HttpPatch("rate")]
        public IActionResult AddRating(Food food)
        {
            _context.Foods.Update(food);
            _ = _context.SaveChangesAsync();

            return Ok("Rating has been added successfully");
        }
    }
}
