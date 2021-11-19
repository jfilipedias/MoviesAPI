using Microsoft.EntityFrameworkCore;
using MoviesAPI.Models;

namespace MoviesAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Address> Addresses { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Theater> Theaters { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Relationship 1:1
            builder.Entity<Address>()
                .HasOne(address => address.Theater)
                .WithOne(theater => theater.Address)
                .HasForeignKey<Theater>(theater => theater.AddressId);
        }
    }
}
