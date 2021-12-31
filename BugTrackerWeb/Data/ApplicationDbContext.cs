using BugTrackerWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BugTrackerWeb.Data {
    public class ApplicationDbContext : DbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Bug> Bugs { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) {
            var entries = ChangeTracker.Entries().Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries) {
                ((BaseEntity)entry.Entity).UpdateDate = DateTime.Now;

                if (entry.State == EntityState.Added) {
                    ((BaseEntity)entry.Entity).CreatedDate = DateTime.Now;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
