﻿using System;
using System.ComponentModel.DataAnnotations;

namespace JanHofmeyerWebsite.Models
{
    public class ContactMessage
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string FullName { get; set; } = "";

        [Required]
        [EmailAddress]
        [MaxLength(200)]
        public string Email { get; set; } = "";

        [Phone]
        [MaxLength(50)]
        public string Phone { get; set; } = "";

        [Required]
        [MaxLength(300)]
        public string Subject { get; set; } = "";

        [Required]
        public string Message { get; set; } = "";

        public DateTime MessageDate { get; set; } = DateTime.Now;

        public bool IsRead { get; set; } = false;

        [MaxLength(50)]
        public string InquiryType { get; set; } = "General";
    }
}