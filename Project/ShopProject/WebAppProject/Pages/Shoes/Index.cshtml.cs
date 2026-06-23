using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShopLibrary.Contexts;
using ShopLibrary.Models;

namespace WebAppProject.Pages.Shoes
{
    public class IndexModel : PageModel
    {
        private readonly ProjectDbContext _context;

        public IndexModel(ProjectDbContext context)
        {
            _context = context;
        }

        public List<Shoe> Shoe { get; set; } = new();
        public List<Maker> Makers { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? ShoeDescription { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Maker { get; set; } = 0;

        [BindProperty(SupportsGet = true)]
        public int? MaxPrice { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool IsDiscount { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool IsInStock { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SortColumn { get; set; }

        public async Task OnGetAsync()
        {
            Makers = await _context.Makers.ToListAsync();

            var query = _context.Shoes
                .Include(s => s.Category)
                .Include(s => s.Maker)
                .Include(s => s.Vendor)
                .AsQueryable();

            if (!string.IsNullOrEmpty(ShoeDescription))
                query = query.Where(s => s.Description != null &&
                                       s.Description.ToLower().Contains(ShoeDescription.ToLower()));

            if (Maker > 0)
                query = query.Where(s => s.MakerId == Maker);

            if (MaxPrice.HasValue && MaxPrice > 0)
                query = query.Where(s => s.Price <= MaxPrice.Value);

            if (IsDiscount)
                query = query.Where(s => s.Discount > 0);

            if (IsInStock)
                query = query.Where(s => s.Quantity > 0);

            query = SortColumn switch
            {
                "name" => query.OrderBy(s => s.Category.Name),
                "vendor" => query.OrderBy(s => s.Vendor.Name),
                "price" => query.OrderBy(s => s.Price),
                "price_desc" => query.OrderByDescending(s => s.Price),
                _ => query.OrderBy(s => s.Category.Name)
            };

            Shoe = await query.ToListAsync();
        }
    }
}