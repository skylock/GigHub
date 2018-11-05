using GigHub.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GigHub.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {

        public DbSet<Gig> Gigs { get; set; }
        public DbSet<Genre> Genres { get; set; }

        public DbSet<Attendance> Attendances { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Attendance>()
                .HasKey(a => new {a.GigId, a.AttendeeId});

            modelBuilder.Entity<Attendance>()
                .HasOne(g => g.Gig)
                .WithMany(a => a.Attendees)
                .HasForeignKey(at => at.GigId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.Attendee)
                .WithMany(a => a.Attendances)
                .HasForeignKey(at => at.AttendeeId);
        }
    }
}
