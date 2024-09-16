

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
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<Project> Projects { get; set; }
        public DbSet<AppTask> Tasks { get; set; }
        public DbSet<UserProject> UserProjects { get; set; }
        public DbSet<UserTask> UserTasks { get; set; }

    }
}
