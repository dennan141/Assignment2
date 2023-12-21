using CoursesMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace CoursesMVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        //Creates the table for the courses
        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().HasData(
                new Course { Id = 1, Title = "MVC", Description = "A course about Model Views and Controllers" },
                new Course { Id = 2, Title = "MVP", Description = "Minimum viable product" },
                new Course { Id = 3, Title = "Network programming", Description = "Using Python and SQL programs networks and databases" }
                );
        }
    }
}
