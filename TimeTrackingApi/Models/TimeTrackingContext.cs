using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace TimeTrackingApi.Models 
{
    public class TimeTrackingContext : DbContext
    {
        private readonly IConfiguration configuration;
        public TimeTrackingContext(DbContextOptions<TimeTrackingContext> options, IConfiguration configuration)
            : base(options)
            {
                this.configuration = configuration;
            }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer(this.configuration.GetConnectionString("TimeTrackingConnection"));
            }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    Password = "admin",
                    Email = "andreea.goga@yahoo.com",
                    FirstName = "Andreea",  
                    LastName = "Goga",
                    IsConfirmed = true,
                }
         );
         
        }
            public DbSet<TimeTrackingItem> TimeTrackingItems { get; set; } = null!;
            public DbSet<TimeTrackingSubitem> TimeTrackingSubitems { get; set; } = null!;
            public DbSet<User> Users { get; set; } = null!;
    }
}