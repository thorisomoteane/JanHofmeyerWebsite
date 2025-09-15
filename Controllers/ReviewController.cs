using Microsoft.AspNetCore.Mvc;
using JanHofmeyerWebsite.Data;
using JanHofmeyerWebsite.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JanHofmeyerWebsite.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReviewController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                // Create a list to hold cleaned reviews
                var cleanReviews = new List<Review>();

                // Get only approved and displayed reviews using raw SQL
                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = @"
                        SELECT * FROM Reviews 
                        WHERE IsApproved = 1 AND IsDisplayed = 1 
                        ORDER BY ReviewDate DESC";

                    await _context.Database.OpenConnectionAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var review = new Review
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                ReviewerName = reader.IsDBNull(reader.GetOrdinal("ReviewerName")) ? "Anonymous" : reader.GetString(reader.GetOrdinal("ReviewerName")),
                                ServiceUsed = reader.IsDBNull(reader.GetOrdinal("ServiceUsed")) ? "General Services" : reader.GetString(reader.GetOrdinal("ServiceUsed")),
                                Rating = reader.GetInt32(reader.GetOrdinal("Rating")),
                                ReviewMessage = reader.IsDBNull(reader.GetOrdinal("ReviewMessage")) ? "" : reader.GetString(reader.GetOrdinal("ReviewMessage")),
                                ReviewDate = reader.GetDateTime(reader.GetOrdinal("ReviewDate")),
                                IsApproved = reader.GetBoolean(reader.GetOrdinal("IsApproved")),
                                IsDisplayed = reader.GetBoolean(reader.GetOrdinal("IsDisplayed"))
                            };
                            cleanReviews.Add(review);
                        }
                    }
                }

                return View(cleanReviews);
            }
            catch (Exception ex)
            {
                // Log error and return empty list
                System.Diagnostics.Debug.WriteLine($"Error loading reviews: {ex.Message}");
                return View(new List<Review>());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(Review review)
        {
            try
            {
                // Ensure no nulls for required database fields
                review.ReviewerName = string.IsNullOrWhiteSpace(review.ReviewerName) ? "" : review.ReviewerName;
                review.ServiceUsed = string.IsNullOrWhiteSpace(review.ServiceUsed) ? "" : review.ServiceUsed;
                review.ReviewMessage = string.IsNullOrWhiteSpace(review.ReviewMessage) ? "" : review.ReviewMessage;
                review.ReviewDate = DateTime.Now;
                review.IsApproved = false;
                review.IsDisplayed = false;

                // Ensure Rating is valid
                if (review.Rating < 1 || review.Rating > 5)
                {
                    TempData["Error"] = "Please select a rating between 1 and 5 stars.";
                    return RedirectToAction("Index");
                }

                // Validate required fields
                if (string.IsNullOrWhiteSpace(review.ReviewerName))
                {
                    TempData["Error"] = "Please enter your name.";
                    return RedirectToAction("Index");
                }

                if (string.IsNullOrWhiteSpace(review.ServiceUsed))
                {
                    TempData["Error"] = "Please select a service.";
                    return RedirectToAction("Index");
                }

                if (string.IsNullOrWhiteSpace(review.ReviewMessage))
                {
                    TempData["Error"] = "Please enter your review message.";
                    return RedirectToAction("Index");
                }

                _context.Reviews.Add(review);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Thank you for your review! It will be displayed after moderation.";
            }
            catch (Exception ex)
            {
                // Get the real error message
                var innerMessage = ex.InnerException?.Message ?? ex.Message;
                TempData["Error"] = $"There was an error submitting your review. Please try again.";

                // Log the error
                System.Diagnostics.Debug.WriteLine($"Review submission error: {innerMessage}");
            }

            return RedirectToAction("Index");
        }
    }
}