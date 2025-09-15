using System.Collections.Generic;
using JanHofmeyerWebsite.Models;

namespace JanHofmeyerWebsite.ViewModels
{
    public class HomeViewModel
    {
        // Featured Projects Section
        public List<Project> FeaturedProjects { get; set; } = new List<Project>();

        // Statistics Section
        public int TotalPeopleHelped { get; set; }
        public int TotalVolunteers { get; set; }
        public int ActiveProjects { get; set; }
        public int YearsOfService { get; set; }

        // Community Voices Section (Recent Reviews)
        public List<Review> FeaturedReviews { get; set; } = new List<Review>();

        // Optional: Add upcoming events for home page
        public List<Event> UpcomingEvents { get; set; } = new List<Event>();

        // Optional: Mission statement text (could be retrieved from settings/database)
        public string MissionStatement { get; set; } = "Jan Hofmeyer Community has a mission of to empower communities through sustainable development programs, educational initiatives, and support services that create lasting positive change in people's lives.";

        // Optional: Quick stats for donation impact
        public decimal TotalDonationsReceived { get; set; }
        public int TotalFamiliesSupported { get; set; }
        public int TotalMealsProvided { get; set; }
    }
}