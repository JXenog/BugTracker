using BugTrackerWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BugTrackerWeb.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
    }
}
