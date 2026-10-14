using Microsoft.EntityFrameworkCore;
using RaceDay.Models;

namespace RaceDay.Data
{
    public class RaceDayContext : DbContext
    {
        public RaceDayContext(
            DbContextOptions<RaceDayContext> options)
            : base(options)
        {
        }

        // Database tables
        public DbSet<User> Users { get; set; }
        public DbSet<Events> Events { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Enrolment> Enrolments { get; set; }
        public DbSet<RaceDay.Models.Results> Results { get; set; }
        public DbSet<History> History { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Every user must have a different email
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // One user can organise many events
            modelBuilder.Entity<Events>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(e => e.OrganiserId)
                .OnDelete(DeleteBehavior.Restrict);

            // One event can have many categories
            modelBuilder.Entity<Categories>()
                .HasOne<Events>()
                .WithMany()
                .HasForeignKey(c => c.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            // One user can manage many categories
            modelBuilder.Entity<Categories>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(c => c.OrganiserId)
                .OnDelete(DeleteBehavior.Restrict);

            // A user can enrol in an event only once
            modelBuilder.Entity<Enrolment>()
                .HasIndex(e => new { e.EventId, e.UserId })
                .IsUnique();

            // Connect enrolment to event
            modelBuilder.Entity<Enrolment>()
                .HasOne<Events>()
                .WithMany()
                .HasForeignKey(e => e.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            // Connect enrolment to participant user
            modelBuilder.Entity<Enrolment>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Connect enrolment to organiser user
            modelBuilder.Entity<Enrolment>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(e => e.OrganiserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Connect enrolment to selected category
            modelBuilder.Entity<Enrolment>()
                .HasOne<Categories>()
                .WithMany()
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // One enrolment can have results
            modelBuilder.Entity<RaceDay.Models.Results>()
                .HasOne<Enrolment>()
                .WithMany()
                .HasForeignKey(r => r.EnrolmentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Connect history to result
            modelBuilder.Entity<History>()
                .HasOne<RaceDay.Models.Results>()
                .WithMany()
                .HasForeignKey(h => h.ResultId)
                .OnDelete(DeleteBehavior.Cascade);

            // Connect history to user
            modelBuilder.Entity<History>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(h => h.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}