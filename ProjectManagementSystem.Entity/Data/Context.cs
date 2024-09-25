

using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Entity.Entities;

namespace ProjectManagementSystem.Entity.Data
{
    public class Context : DbContext
    {

        public Context(DbContextOptions<Context> option) : base(option)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }


        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<AppTask> Tasks { get; set; }
        public DbSet<UserProject> UserProjects { get; set; }
        public DbSet<TaskTimer> TaskTimers { get; set; }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();

            var deletedEntries = ChangeTracker.Entries()
                                        .Where(p => p.State == EntityState.Deleted);


            foreach (var entry in deletedEntries)
            {
                entry.State = EntityState.Modified;
                entry.CurrentValues["IsDeleted"] = true;
            }
            
            return base.SaveChanges();
        }

    }
}
