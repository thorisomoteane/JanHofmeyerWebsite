using Microsoft.AspNetCore.Mvc;
using JanHofmeyerWebsite.Data;
using JanHofmeyerWebsite.Models;
using Microsoft.EntityFrameworkCore;

namespace JanHofmeyerWebsite.Controllers
{
    public class GalleryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GalleryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var galleryItems = await _context.Gallery
                .Where(g => !g.IsDeleted)
                .OrderByDescending(g => g.UploadDate)
                .ToListAsync();

            return View(galleryItems);
        }
    }
}