using JanHofmeyerWebsite.Data;
using JanHofmeyerWebsite.Models;
using JanHofmeyerWebsite.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JanHofmeyerWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new HomeViewModel
            {
                FeaturedProjects = await _context.Projects
                    .Where(p => !p.IsDeleted && p.Status == "Active")
                    .Take(3)
                    .ToListAsync(),
                TotalPeopleHelped = await _context.Projects
                    .Where(p => !p.IsDeleted)
                    .SumAsync(p => p.PeopleImpacted ?? 0),
                TotalVolunteers = await _context.VolunteerApplications
                    .Where(v => v.Status == "Approved")
                    .CountAsync(),
                ActiveProjects = await _context.Projects
                    .Where(p => !p.IsDeleted && p.Status == "Active")
                    .CountAsync(),
                YearsOfService = 25 // This could be calculated or stored in settings
            };

            return View(viewModel);
        }

        public IActionResult About()
        {
            return View();
        }
    }
}