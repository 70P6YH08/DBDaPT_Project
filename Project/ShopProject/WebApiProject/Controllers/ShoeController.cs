using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopLibrary.Contexts;
using ShopLibrary.Models;

namespace WebApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoeController(ProjectDbContext context) : ControllerBase
    {
        private readonly ProjectDbContext _context = context;

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shoe>>> GetShoes()
            => await _context.Shoes.ToListAsync();

        
        [AllowAnonymous]
        [HttpGet("{article}")]
        public async Task<ActionResult<Shoe>> GetShoeAsync(string article)
        {
            var shoe = await _context.Shoes.FirstOrDefaultAsync(s => s.Article == article);

            if(shoe == null)
                return NotFound();
            return Ok(shoe);
        }

        [Authorize(Roles = "admin, manager")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShoeAsync(int id, Shoe shoe)
        {
            if (id != shoe.ShoeId)
                return BadRequest();

            _context.Entry(shoe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ShoeExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [Authorize(Roles = "admin, manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShoe(int id)
        {
            var shoe = await _context.Shoes.FindAsync(id);

            if (shoe == null)
            {
                return NotFound();
            }

            _context.Shoes.Remove(shoe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [Authorize(Roles = "admin, manager")]
        [HttpPost]
        public async Task<ActionResult<Shoe>> PostShoeAsync(Shoe shoe)
        {
            _context.Shoes.Add(shoe);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetShoeAsync), new { id = shoe.ShoeId }, shoe);
        }
        private async Task<bool> ShoeExists(int id)
        {
            return await _context.Shoes.AnyAsync(s => s.ShoeId == id);
        }
    }
}
