using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RotaMe.Data.Models;
using System;

namespace RotaMe.Data
{
    public class RotaMeDbContext : IdentityDbContext<RotaMeUser, IdentityRole, string>
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventNeed> EventNeeds { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Availability> Availabilities { get; set; }


        public RotaMeDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RotaMeUser>()
                .Property(u => u.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Project>()
                .HasOne(p => p.Owner)
                .WithMany(u => u.OwnProjects)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => p.OwnerId);

            modelBuilder.Entity<UserProject>()
                .HasKey(up => new { up.UserId, up.ProjectId });

            modelBuilder.Entity<UserEvent>()
                .HasKey(ue => new { ue.UserId, ue.EventId });

            modelBuilder.Entity<Availability>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<Availability>()
                .HasOne(a => a.User)
                .WithMany(u => u.Availabilities)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(a => a.UserId);

            modelBuilder.Entity<Availability>()
                .HasOne(a => a.Event)
                .WithMany(e => e.Availabilities)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(a => a.EventId);

            modelBuilder.Entity<UserProject>()
                .HasOne(up => up.User)
                .WithMany(u => u.Projects)
                .HasForeignKey(up => up.UserId);

            modelBuilder.Entity<UserProject>()
                .HasOne(up => up.Project)
                .WithMany(p => p.Users)
                .HasForeignKey(up => up.ProjectId);



            modelBuilder.Entity<UserEvent>()
                .HasOne(ue => ue.User)
                .WithMany(u => u.Events)
                .HasForeignKey(ue => ue.UserId);

            modelBuilder.Entity<UserEvent>()
                .HasOne(ue => ue.Event)
                .WithMany(e => e.Users)
                .HasForeignKey(ue => ue.EventId);
        }
    }
}
