using GigHub.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GigHub.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {

        public DbSet<Gig> Gigs { get; set; }
        public DbSet<Genre> Genres { get; set; }

        public DbSet<Attendance> Attendances { get; set; }

        public DbSet<Following> Followings { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        public DbSet<UserNotification> UserNotifications { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendance>()
                .HasKey(a => new { a.GigId, a.AttendeeId });

            modelBuilder.Entity<Attendance>()
                .HasOne(g => g.Gig)
                .WithMany(a => a.Attendances)
                .HasForeignKey(at => at.GigId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.Attendee)
                .WithMany(a => a.Attendances)
                .HasForeignKey(at => at.AttendeeId);


            modelBuilder.Entity<Following>()
                .HasKey(a => new { a.FollowerId, a.FolloweeId });

            modelBuilder.Entity<Following>()
                .HasOne(f => f.Follower)
                .WithMany(u => u.Followees)
                .HasForeignKey(f => f.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Following>()
                .HasOne(f => f.Followee)
                .WithMany(u => u.Followers)
                .HasForeignKey(f => f.FolloweeId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<UserNotification>()
                .HasKey(n => new { n.NotificationId, n.UserId });

            modelBuilder.Entity<UserNotification>()
                .HasOne(f => f.Notification)
                .WithMany(u => u.UserNotifications)
                .HasForeignKey(f => f.NotificationId);


            modelBuilder.Entity<UserNotification>()
                .HasOne(f => f.User)
                .WithMany(u => u.UserNotifications)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Restrict);


            base.OnModelCreating(modelBuilder);
        }
    }
}
