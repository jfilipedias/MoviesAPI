﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UsersAPI.Models;

namespace UsersAPI.Data
{
    public class UserDbContext : IdentityDbContext<CustomIdentityUser<int>, IdentityRole<int>, int>
    {
        private IConfiguration _configuration;

        public UserDbContext(DbContextOptions<UserDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var adminUser = new CustomIdentityUser<int>
            {
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                Id = 9999
            };

            var passwordHasher = new PasswordHasher<CustomIdentityUser<int>>();
            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, 
                _configuration.GetValue<string>("AdminInfo:Password"));

            var adminRole = new IdentityRole<int> { Id = 9999, Name = "admin", NormalizedName = "ADMIN" };
            var regularRole = new IdentityRole<int> { Id = 9998, Name = "regular", NormalizedName = "REGULAR" };
            var identityUserRole = new IdentityUserRole<int> { RoleId = adminRole.Id, UserId = adminUser.Id };

            builder.Entity<CustomIdentityUser<int>>().HasData(adminUser);
            builder.Entity<IdentityRole<int>>().HasData(adminRole);
            builder.Entity<IdentityRole<int>>().HasData(regularRole);
            builder.Entity<IdentityUserRole<int>>().HasData(identityUserRole);
        }
    }
}
