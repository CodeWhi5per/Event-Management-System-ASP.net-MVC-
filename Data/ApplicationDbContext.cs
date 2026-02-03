using Microsoft.EntityFrameworkCore;
using EventManagementSystem.Models;

namespace EventManagementSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Member> Members { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingDetail> BookingDetails { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Preference> Preferences { get; set; }
        public DbSet<Inquiry> Inquiries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Member Configuration
            modelBuilder.Entity<Member>(entity =>
            {
                entity.ToTable("MEMBER");
                entity.HasKey(e => e.MemberID);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.Status).HasDefaultValue("Active");
            });

            // Venue Configuration
            modelBuilder.Entity<Venue>(entity =>
            {
                entity.ToTable("VENUE");
                entity.HasKey(e => e.VenueID);
            });

            // Category Configuration
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("CATEGORY");
                entity.HasKey(e => e.CategoryID);
                entity.HasIndex(e => e.CategoryName).IsUnique();
                entity.Property(e => e.IsActive).HasDefaultValue(true);
            });

            // Event Configuration
            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("EVENT");
                entity.HasKey(e => e.EventID);
                entity.HasOne(e => e.Venue)
                      .WithMany(v => v.Events)
                      .HasForeignKey(e => e.VenueID)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.Category)
                      .WithMany(c => c.Events)
                      .HasForeignKey(e => e.CategoryID)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.Property(e => e.Status).HasDefaultValue("Upcoming");
            });

            // Booking Configuration
            modelBuilder.Entity<Booking>(entity =>
            {
                entity.ToTable("BOOKING");
                entity.HasKey(e => e.BookingID);
                entity.HasOne(e => e.Member)
                      .WithMany(m => m.Bookings)
                      .HasForeignKey(e => e.MemberID)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.Event)
                      .WithMany(e => e.Bookings)
                      .HasForeignKey(e => e.EventID)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasIndex(e => e.BookingReference).IsUnique();
                entity.Property(e => e.Status).HasDefaultValue("Confirmed");
            });

            // BookingDetail Configuration
            modelBuilder.Entity<BookingDetail>(entity =>
            {
                entity.ToTable("BOOKING_DETAIL");
                entity.HasKey(e => e.DetailID);
                entity.HasOne(e => e.Booking)
                      .WithMany(b => b.BookingDetails)
                      .HasForeignKey(e => e.BookingID)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Review Configuration
            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("REVIEW");
                entity.HasKey(e => e.ReviewID);
                entity.HasOne(e => e.Member)
                      .WithMany(m => m.Reviews)
                      .HasForeignKey(e => e.MemberID)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.Event)
                      .WithMany(e => e.Reviews)
                      .HasForeignKey(e => e.EventID)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.Property(e => e.IsApproved).HasDefaultValue(false);
            });

            // Preference Configuration
            modelBuilder.Entity<Preference>(entity =>
            {
                entity.ToTable("PREFERENCE");
                entity.HasKey(e => e.PreferenceID);
                entity.HasOne(e => e.Member)
                      .WithMany(m => m.Preferences)
                      .HasForeignKey(e => e.MemberID)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Category)
                      .WithMany(c => c.Preferences)
                      .HasForeignKey(e => e.CategoryID)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasIndex(e => new { e.MemberID, e.CategoryID }).IsUnique();
            });

            // Inquiry Configuration
            modelBuilder.Entity<Inquiry>(entity =>
            {
                entity.ToTable("INQUIRY");
                entity.HasKey(e => e.InquiryID);
                entity.Property(e => e.Status).HasDefaultValue("Pending");
            });
        }
    }
}
