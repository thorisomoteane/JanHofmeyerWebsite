using System;
using System.ComponentModel.DataAnnotations;

namespace JanHofmeyerWebsite.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string ShortSummary { get; set; }

        [MaxLength(50)]
        public string Status { get; set; } // Active or Planning

        [MaxLength(100)]
        public string Theme { get; set; }

        public int? PeopleImpacted { get; set; }

        public string AboutSection { get; set; }

        public string KeyGoals { get; set; }

        [MaxLength(500)]
        public string ImageUrl { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; } = false;
    }
}