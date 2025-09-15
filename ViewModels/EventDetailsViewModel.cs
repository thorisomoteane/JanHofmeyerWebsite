using JanHofmeyerWebsite.Models;

namespace JanHofmeyerWebsite.ViewModels
{
    public class EventDetailsViewModel
    {
        public Event Event { get; set; }
        public List<EventRegistration> Registrations { get; set; }
        public int RegisteredCount { get; set; }
        public int SpotsAvailable { get; set; }
        public bool CanRegister { get; set; }
    }
}