using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopLibrary.Contexts;
using ShopLibrary.DTOs;
using ShopLibrary.Extensions;

namespace WebApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(ProjectDbContext context) : ControllerBase
    {
        private readonly ProjectDbContext _context = context;

        [Authorize]
        [HttpGet("{login}")]
        public async Task<ActionResult<IEnumerable<OrderDto?>>> GetOrdersByLoginAsync(string login)
        {
            var orders = await _context.Orders
                .Include(o => o.User)
                .Where(o => o.User.Login == login)
                .ToListAsync();

            if (orders == null)
                return NotFound();
            return Ok(OrderExtension.ToDtos(orders));
        }

        [Authorize(Roles = "admin, manager")]
        [HttpPut("{id}/delivered")]
        public async Task<ActionResult<OrderDto>> PutOrderDateAsync(int id, [FromQuery] DateOnly? deliveryDate = null, [FromQuery] bool isFinished = false)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
                return NotFound();

            if (deliveryDate == null)
                order.DeliveryDate = DateOnly.FromDateTime(DateTime.Now);
            else
                order.DeliveryDate = deliveryDate;

            order.IsFinished = isFinished;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return OrderExtension.ToDto(order);
        }
    }
}
