using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UsersAPI.Data
{
    public class UserDbContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var adminUser = new IdentityUser<int>
            {
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                Id = 9999
            };

            var passwordHasher = new PasswordHasher<IdentityUser<int>>();
            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "Admin123456!");

            var adminRole = new IdentityRole<int> { Id = 9999, Name = "admin", NormalizedName = "ADMIN" };
            var userRole = new IdentityUserRole<int> { RoleId = adminRole.Id, UserId = adminUser.Id };

            builder.Entity<IdentityUser<int>>().HasData(adminUser);
            builder.Entity<IdentityRole<int>>().HasData(adminRole);
            builder.Entity<IdentityUserRole<int>>().HasData(userRole);
        }
    }
}
