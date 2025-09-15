using Microsoft.EntityFrameworkCore;
using JanHofmeyerWebsite.Models;

namespace JanHofmeyerWebsite.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventRegistration> EventRegistrations { get; set; }
        public DbSet<GalleryItem> Gallery { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<VolunteerApplication> VolunteerApplications { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<AdminUser> AdminUsers { get; set; }

        // Add this for design-time DbContext creation
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=tcp:st10496876-server.database.windows.net,1433;Initial Catalog=XADAD7112-db;Persist Security Info=False;User ID=sqladmin;Password=YourStrongPassword123!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }
    }
}