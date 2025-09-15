using Microsoft.AspNetCore.Mvc;
using JanHofmeyerWebsite.Data;
using Microsoft.EntityFrameworkCore;

namespace JanHofmeyerWebsite.Controllers
{
    public class DonateController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DonateController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // You could load donation statistics here if you have a donations table
            ViewBag.TotalDonations = 2500000; // Example values
            ViewBag.TotalDonors = 850;
            ViewBag.ProgramsFunded = 15;

            return View();
        }

        // If you implement online donations later, you would add methods here
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProcessDonation(decimal amount, string frequency)
        {
            // This is a placeholder for when you implement payment processing
            // For now, just show the banking details

            TempData["Info"] = $"Please transfer R{amount} to our bank account. Thank you for your generosity!";
            return RedirectToAction("Index");
        }
    }
}