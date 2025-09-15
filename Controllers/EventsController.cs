using Microsoft.AspNetCore.Mvc;
using JanHofmeyerWebsite.Data;
using JanHofmeyerWebsite.Models;
using Microsoft.EntityFrameworkCore;

namespace JanHofmeyerWebsite.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var upcomingEvents = await _context.Events
                .Where(e => !e.IsDeleted && e.IsUpcoming)
                .OrderBy(e => e.EventDate)
                .ToListAsync();

            var recentEvents = await _context.Events
                .Where(e => !e.IsDeleted && !e.IsUpcoming)
                .OrderByDescending(e => e.EventDate)
                .Take(6)
                .ToListAsync();

            ViewBag.RecentEvents = recentEvents;
            return View(upcomingEvents);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(EventRegistration registration)
        {
            try
            {
                // Remove ModelState validation temporarily to debug
                if (registration.EventId == 0)
                {
                    TempData["Error"] = "Please select an event to register for.";
                    return RedirectToAction("Index");
                }

                if (string.IsNullOrWhiteSpace(registration.FullName) || string.IsNullOrWhiteSpace(registration.Email))
                {
                    TempData["Error"] = "Please fill in all required fields.";
                    return RedirectToAction("Index");
                }

                // Check if event exists
                var eventItem = await _context.Events
                    .FirstOrDefaultAsync(e => e.Id == registration.EventId && !e.IsDeleted);

                if (eventItem == null)
                {
                    TempData["Error"] = "Event not found.";
                    return RedirectToAction("Index");
                }

                // Check for duplicate registration
                var existingRegistration = await _context.EventRegistrations
                    .FirstOrDefaultAsync(r => r.EventId == registration.EventId &&
                                              r.Email == registration.Email);

                if (existingRegistration != null)
                {
                    TempData["Info"] = "You are already registered for this event.";
                    return RedirectToAction("Index");
                }

                // Set registration date and save
                registration.RegistrationDate = DateTime.Now;
                _context.EventRegistrations.Add(registration);
                await _context.SaveChangesAsync();

                TempData["Success"] = $"Successfully registered for {eventItem.Title}!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Registration failed: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            var eventItem = await _context.Events
                .FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);

            if (eventItem == null)
            {
                return NotFound();
            }

            var registrations = await _context.EventRegistrations
                .Where(r => r.EventId == id)
                .ToListAsync();

            ViewBag.RegisteredCount = registrations.Count;
            ViewBag.Registrations = registrations;

            return View(eventItem);
        }
    }
}