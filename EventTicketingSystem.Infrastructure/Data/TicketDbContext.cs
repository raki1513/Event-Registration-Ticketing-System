using EventTicketingSystem.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTicketingSystem.Infrastructure.Data
{

    
    public class TicketDbContext:DbContext
    {


        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public TicketDbContext(DbContextOptions options) : base(options) { }


        

      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



            modelBuilder.Entity<UserRole>()
                .HasIndex(ur => new { ur.UserId, ur.RoleId })
                .IsUnique();

            modelBuilder.Entity<Ticket>()
                .HasIndex(t => new { t.AttendeeId, t.EventId })
                .IsUnique();



            modelBuilder.Entity<Role>()
              .Property(r => r.RoleName)
              .HasConversion<string>();
            
            
            modelBuilder.Entity<Ticket>()
                .HasOne(e => e.Event)
                .WithMany(t => t.Tickets)
                .HasForeignKey(e => e.EventId)
                .OnDelete(DeleteBehavior.Cascade);




            modelBuilder.Entity<Event>()
                .HasOne(e => e.Host)
                .WithMany(u => u.HostedEvents)
                .HasForeignKey(e => e.HostId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Attendee)
                .WithMany(u => u.Tickets)
                .HasForeignKey(t => t.AttendeeId)
                .OnDelete(DeleteBehavior.Restrict);  


        }

    }
}
