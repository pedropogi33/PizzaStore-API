using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaStore_API.DTOs;
using PizzaStore_API.Models;

namespace PizzaStore_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DataController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("total-sales")]
        public async Task<IActionResult> GetTotalSalesOfEachPizza(DateTime? startDate,DateTime? endDate, string? pizzaId)
        {
            var query = _context.OrderDetails.AsQueryable();

            if (startDate.HasValue && endDate.HasValue)
            {
                if (startDate.Value > endDate.Value)
                {
                    return BadRequest("Start date must be before or equal to end date.");
                }
                query = query.Where(od => od.order.date >= startDate.Value && od.order.date <= endDate.Value);
            }

            if (!string.IsNullOrEmpty(pizzaId))
            {
                query = query.Where(od => od.pizza_id == pizzaId);
            }

            var result = query
               .GroupBy(od => new { od.pizza.pizza_id, od.pizza.pizzatype.name })
               .Select(g => new 
               {
                   PizzaId = g.Key.pizza_id,
                   PizzaName = g.Key.name,
                   TotalSales = g.Sum(od => od.quantity)
               })
           .ToList();

            return Ok(result);
        }

        [HttpGet("pizza/{id}")]
        public async Task<IActionResult> GetPizzaDetails(string id)
        {
            var pizza = await _context.Pizzas
                .Where(p => p.pizza_id == id)
                .Select(p => new
                {
                    p.pizza_id,
                    p.size,
                    p.price,
                    Ingredients = p.pizzatype.ingredients 
                })
                .FirstOrDefaultAsync();

            if (pizza == null)
            {
                return NotFound();
            }

            return Ok(pizza);
        }
        [HttpGet("pizzas")]
        public async Task<IActionResult> GetPizzas(int pageNumber = 1, int pageSize = 10)
        {
            var skip = (pageNumber - 1) * pageSize;

            var pizzas = await _context.Pizzas
                .Skip(skip)
                .Take(pageSize)
                .Select(p => new
                {
                    p.pizza_id,
                    p.size,
                    p.price,
                    Ingredients = p.pizzatype.ingredients 
                })
                .ToListAsync();

            var totalPizzas = await _context.Pizzas.CountAsync();

            var result = new
            {
                TotalCount = totalPizzas,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Pizzas = pizzas
            };

            return Ok(result);
        }
        [HttpPut("update-pizza")]
        public async Task<IActionResult> UpdatePizza(string pizzaId, decimal? newPrice, string ingredients = null, string size = null)
        {
            if (string.IsNullOrEmpty(pizzaId))
            {
                return BadRequest("Pizza ID is required.");
            }

            var pizza = await _context.Pizzas.FirstOrDefaultAsync(p => p.pizza_id == pizzaId);
            if (pizza == null)
            {
                return NotFound("Pizza not found.");
            }

            bool hasChanges = false;

            if (newPrice.HasValue && pizza.price != newPrice.Value)
            {
                pizza.price = newPrice.Value;
                hasChanges = true;
            }

            if (!string.IsNullOrEmpty(ingredients) && pizza.pizzatype.ingredients != ingredients)
            {
                pizza.pizzatype.ingredients = ingredients;
                hasChanges = true;
            }

            if (!string.IsNullOrEmpty(size) && pizza.size != size)
            {
                pizza.size = size;
                hasChanges = true;
            }

            if (!hasChanges)
            {
                return BadRequest("No changes were made to the pizza.");
            }

            _context.Pizzas.Update(pizza);
            await _context.SaveChangesAsync();

            return Ok("Pizza updated successfully.");
        }
    }
}
