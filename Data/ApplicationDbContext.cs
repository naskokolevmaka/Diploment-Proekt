using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Airport.Areas.Identity.Data;
using Airport.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Airport.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Airline> airlines { get; set; }
        public DbSet<Destination> destinations { get; set; }
        public DbSet<Schedule> schedules { get; set; }
        public DbSet<Ticket> tickets { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<User>()
                .Property(e => e.FirstName)
                .HasMaxLength(250);

            builder.Entity<User>()
                .Property(e => e.Surname)
                .HasMaxLength(250);
        }
    }
}
