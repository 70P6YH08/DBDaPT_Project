using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopLibrary.Contexts;
using ShopLibrary.DTOs;
using ShopLibrary.Models;

namespace WebApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoesController(ProjectDbContext context) : ControllerBase
    {
        private readonly ProjectDbContext _context = context;

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shoe>>> GetShoesAsync()
        {
            return await _context.Shoes.ToListAsync();
        }

        [AllowAnonymous]
        [HttpGet("{article}")]
        public async Task<ActionResult<Shoe>> GetShoe(string article)
        {
            var shoe = await _context.Shoes
                .FirstOrDefaultAsync(s => s.Article == article);

            if (shoe == null)
                return NotFound();
            return Ok(shoe);
        }

        [Authorize(Roles = "admin, manager")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShoeAsync(int id, ShoeDto shoeDto)
        {
            Shoe newShoe = new()
            {
                ShoeId = shoeDto.ShoeId,
                Article = shoeDto.Article,
                Price = shoeDto.Price,
                Discount = shoeDto.Discount,
                Quantity = shoeDto.Quantity,
                Description = shoeDto.Description,
                Size = shoeDto.Size,
                Color = shoeDto.Color,
                VendorId = shoeDto.VendorId,
                MakerId = shoeDto.MakerId,
                CategoryId = shoeDto.CategoryId,
                IsFemale = shoeDto.IsFemale,
                PhotoName = shoeDto.PhotoName
            };

            if (id != newShoe.ShoeId)
                return BadRequest();

            _context.Entry(newShoe).State = EntityState.Modified;

            if (!await ShoeExists(id))
                return NotFound();

            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetShoe),
                new { article = newShoe.Article }, newShoe);
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
        public async Task<ActionResult<Shoe>> PostShoeAsync(ShoeDto shoeDto)
        {
            Shoe newShoe = new()
            {
                ShoeId = shoeDto.ShoeId,
                Article = shoeDto.Article,
                Price = shoeDto.Price,
                Discount = shoeDto.Discount,
                Quantity = shoeDto.Quantity,
                Description = shoeDto.Description,
                Size = shoeDto.Size,
                Color = shoeDto.Color,
                VendorId = shoeDto.VendorId,
                MakerId = shoeDto.MakerId,
                CategoryId = shoeDto.CategoryId,
                IsFemale = shoeDto.IsFemale,
                PhotoName = shoeDto.PhotoName
            };

            _context.Shoes.Add(newShoe);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetShoe),
                new { article = newShoe.Article }, newShoe);
        }
        private async Task<bool> ShoeExists(int id)
        {
            return await _context.Shoes.AnyAsync(s => s.ShoeId == id);
        }
    }
}
