using Microsoft.EntityFrameworkCore;
using RaceDayAPI.Models;
using System.Collections.Generic;
using System.Reflection.Emit;
namespace RaceDayAPI.Data
{
    public class RaceDayContext : DbContext
    {
        public RaceDayContext(DbContextOptions<RaceDayContext> options)
        : base(options)
        {
        }

        public DbSet<Organiser> Organisers { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<RaceEvent> Events { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Enrolment> Enrolments { get; set; }
        public DbSet<RaceResult> Results { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<AppUser> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Organiser>()
                .HasKey(o => o.OrganiserID);

            modelBuilder.Entity<Participant>()
                .HasKey(p => p.ParticipantID);

            modelBuilder.Entity<RaceEvent>()
                .HasOne<Organiser>()
                .WithMany()
                .HasForeignKey(e => e.OrganiserID);

            modelBuilder.Entity<Category>()
                .HasOne<Organiser>()
                .WithMany()
                .HasForeignKey(c => c.OrganiserID);

            modelBuilder.Entity<Category>()
                .HasOne<RaceEvent>()
                .WithMany()
                .HasForeignKey(c => c.EventID);

            modelBuilder.Entity<Enrolment>()
                .HasOne<RaceEvent>()
                .WithMany()
                .HasForeignKey(e => e.EventID);

            modelBuilder.Entity<Enrolment>()
                .HasOne<Participant>()
                .WithMany()
                .HasForeignKey(e => e.ParticipantID);

            modelBuilder.Entity<Enrolment>()
                .HasOne<Organiser>()
                .WithMany()
                .HasForeignKey(e => e.OrganiserID);

            modelBuilder.Entity<Enrolment>()
                .HasOne<Category>()
                .WithMany()
                .HasForeignKey(e => e.CategoryID);

            modelBuilder.Entity<RaceResult>()
                .HasOne<Enrolment>()
                .WithMany()
                .HasForeignKey(r => r.EnrolmentID);

            modelBuilder.Entity<History>()
                .HasOne<RaceResult>()
                .WithMany()
                .HasForeignKey(h => h.ResultID);

            modelBuilder.Entity<History>()
                .HasOne<Participant>()
                .WithMany()
                .HasForeignKey(h => h.ParticipantID);

            modelBuilder.Entity<AppUser>()
                .HasOne<Organiser>()
                .WithMany()
                .HasForeignKey(u => u.OrganiserID)
                .IsRequired(false);

            modelBuilder.Entity<AppUser>()
                .HasOne<Participant>()
                .WithMany()
                .HasForeignKey(u => u.ParticipantID)
                .IsRequired(false);
        }
    }
}