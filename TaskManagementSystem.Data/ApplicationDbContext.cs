using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Data.DataModels;

namespace TaskManagementSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Roles>().HasData(
                new Roles { Id = 1, Name = "CEO" },
                new Roles { Id = 2, Name = "CTO" },
                new Roles { Id = 3, Name = "QA" },
                new Roles { Id = 4, Name = "Developer" }
            );
            modelBuilder.Entity<Permissions>().HasData(
                new Permissions { Id = 1, Name = "addUsers"},
                new Permissions { Id = 2, Name = "assignRoles" },
                new Permissions { Id = 3, Name = "assignPermissions" },
                new Permissions { Id = 4, Name = "assignTasks" },
                new Permissions { Id = 5, Name = "reqToChangeStatus" },
                new Permissions { Id = 6, Name = "changeStatus" },
                new Permissions { Id = 7, Name = "editTask"},
                new Permissions { Id = 8, Name = "assignUsers" }
            );
            modelBuilder.Entity<RoleHasPermissions>().HasData(
                new RoleHasPermissions { Id = 1, RoleId = 1, PermissionId = 1},
                new RoleHasPermissions { Id = 2, RoleId = 1, PermissionId = 2},
                new RoleHasPermissions { Id = 3, RoleId = 1, PermissionId = 3 },
                new RoleHasPermissions { Id = 4, RoleId = 1, PermissionId = 4 },
                new RoleHasPermissions { Id = 5, RoleId = 1, PermissionId = 6 },
                new RoleHasPermissions { Id = 6, RoleId = 2, PermissionId = 4 },
                new RoleHasPermissions { Id = 7, RoleId = 2, PermissionId = 6 },
                new RoleHasPermissions { Id = 8, RoleId = 3, PermissionId = 6 },
                new RoleHasPermissions { Id = 9, RoleId = 4, PermissionId = 5 },
                new RoleHasPermissions { Id = 10, RoleId = 1, PermissionId = 7 },
                new RoleHasPermissions { Id = 11, RoleId = 2, PermissionId = 7 },
                new RoleHasPermissions { Id = 12, RoleId = 1, PermissionId = 8 },
                new RoleHasPermissions { Id = 13, RoleId = 2, PermissionId = 8 }
            );
            modelBuilder.Entity<Users>().HasData(
                new Users { Id = 1, UserName = "ceo", PhoneNumber = "7894561032", Email = "test.ceo@gmail.com", Country = "India", Password = "Dev@123", IsDeleted = 0, RoleId = 1 },
                new Users { Id = 2, UserName = "cto", PhoneNumber = "7894562532", Email = "test.cto@gmail.com", Country = "India", Password = "Dev@123", IsDeleted = 0, RoleId = 2 }
            );
        }

        // CEO (Manages Permissions), CTO (Assign Tasks), Developer (Req. to change status),        QA (change - todo, inprogress, bug, error, compleated) 
        public DbSet<Notifications> Notifications { get; set; }
        public DbSet<NotificationStatus> NotificationStatus { get; set; }
        public DbSet<Permissions> Permissions { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<RoleHasPermissions> RoleHasPermission { get; set; }
        public DbSet<SubTask> SubTask { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<UserHasTask> UserHasTask { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<RefreshTokens> RefreshTokens { get; set; }
        public DbSet<Requests> Requests { get; set; }
    }
}
