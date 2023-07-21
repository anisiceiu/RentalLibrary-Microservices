using Identity.Entities;
using Microsoft.EntityFrameworkCore;

namespace Identity.Data
{
    public class IdentityContext : DbContext
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = 1, Name = "Administrator" },
                new Role { RoleId = 2, Name = "User" }
                );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    Username = "Admin",
                    Email = "admin@rentallibrary.com",
                    Phone = "0123786534",
                    PasswordHash = "240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9"
                }
                );
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { UserRoleId=1, UserId = 1, RoleId = 1 }
                );
        }
    }
}
