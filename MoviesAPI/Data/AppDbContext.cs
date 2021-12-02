using Microsoft.EntityFrameworkCore;
using MoviesAPI.Models;

namespace MoviesAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Theater> Theaters { get; set; }
        public DbSet<Session> Sessions { get; set; }

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

            // Relationship 1:n
            builder.Entity<Theater>()
                .HasOne(theater => theater.Manager)
                .WithMany(manager => manager.Theaters)
                .HasForeignKey(theater => theater.ManagerId);

            // Relationship n:n
            builder.Entity<Session>()
                .HasOne(session => session.Movie)
                .WithMany(movie => movie.Sessions)
                .HasForeignKey(session => session.MovieId);

            builder.Entity<Session>()
                .HasOne(session => session.Theater)
                .WithMany(theater => theater.Sessions)
                .HasForeignKey(session => session.TheaterId);
        }
    }
}
