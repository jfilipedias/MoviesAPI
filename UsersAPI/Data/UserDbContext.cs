using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UsersAPI.Data
{
    public class UserDbContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
    {
        private IConfiguration _configuration;

        public UserDbContext(DbContextOptions<UserDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
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
            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, 
                _configuration.GetValue<string>("AdminInfo:Password"));

            var adminRole = new IdentityRole<int> { Id = 9999, Name = "admin", NormalizedName = "ADMIN" };
            var userRole = new IdentityUserRole<int> { RoleId = adminRole.Id, UserId = adminUser.Id };

            builder.Entity<IdentityUser<int>>().HasData(adminUser);
            builder.Entity<IdentityRole<int>>().HasData(adminRole);
            builder.Entity<IdentityUserRole<int>>().HasData(userRole);
        }
    }
}
