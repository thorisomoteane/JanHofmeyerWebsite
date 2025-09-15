using Microsoft.AspNetCore.Mvc;
using JanHofmeyerWebsite.Data;
using JanHofmeyerWebsite.Models;
using System;
using System.Threading.Tasks;

namespace JanHofmeyerWebsite.Controllers
{
    public class ContactController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendMessage(
            string fullName,
            string email,
            string phone,
            string subject,
            string message,
            string inquiryType)
        {
            try
            {
                // Create the message object manually
                var contactMessage = new ContactMessage
                {
                    FullName = fullName ?? "",
                    Email = email ?? "",
                    Phone = phone ?? "",
                    Subject = subject ?? "",
                    Message = message ?? "",
                    InquiryType = inquiryType ?? "General",
                    MessageDate = DateTime.Now,
                    IsRead = false
                };

                // Validate required fields
                if (string.IsNullOrWhiteSpace(contactMessage.FullName) ||
                    string.IsNullOrWhiteSpace(contactMessage.Email) ||
                    string.IsNullOrWhiteSpace(contactMessage.Subject) ||
                    string.IsNullOrWhiteSpace(contactMessage.Message))
                {
                    TempData["Error"] = "Please fill in all required fields.";
                    return RedirectToAction("Index");
                }

                _context.ContactMessages.Add(contactMessage);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Thank you for your message. We'll get back to you within 24 hours.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"An error occurred. Please try again. Error: {ex.InnerException?.Message ?? ex.Message}";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitVolunteerApplication(
            string fullName,
            string email,
            string phone,
            string ageGroup,
            string availability,
            string areasOfInterest,
            string relevantExperience)
        {
            try
            {
                var application = new VolunteerApplication
                {
                    FullName = fullName ?? "",
                    Email = email ?? "",
                    Phone = phone ?? "",
                    AgeGroup = ageGroup ?? "",
                    Availability = availability ?? "",
                    AreasOfInterest = areasOfInterest ?? "",
                    RelevantExperience = relevantExperience ?? "",
                    ApplicationDate = DateTime.Now,
                    Status = "Pending",
                    AdminNotes = ""
                };

                // Validate required fields
                if (string.IsNullOrWhiteSpace(application.FullName) ||
                    string.IsNullOrWhiteSpace(application.Email) ||
                    string.IsNullOrWhiteSpace(application.Phone))
                {
                    TempData["Error"] = "Please fill in all required fields.";
                    return RedirectToAction("Index");
                }

                _context.VolunteerApplications.Add(application);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Thank you for your volunteer application! We'll review it and get back to you within 48 hours.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"An error occurred. Please try again. Error: {ex.InnerException?.Message ?? ex.Message}";
            }

            return RedirectToAction("Index");
        }
    }
}