using System;
using System.ComponentModel.DataAnnotations;

namespace JanHofmeyerWebsite.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string ShortSummary { get; set; }

        [MaxLength(50)]
        public string Category { get; set; }

        [MaxLength(50)]
        public string Tag { get; set; }

        public DateTime EventDate { get; set; }

        [MaxLength(50)]
        public string EventTime { get; set; }

        [MaxLength(300)]
        public string Location { get; set; }

        public int? ExpectedAttendees { get; set; }

        public int? ActualAttendees { get; set; }

        public string GoalsAchieved { get; set; }

        public bool IsUpcoming { get; set; } = true;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; } = false;
    }
}