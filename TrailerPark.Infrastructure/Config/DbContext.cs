using Microsoft.EntityFrameworkCore;

using TrailerPark.Core.Models;

namespace TrailerPark.Infrastructure.Config;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Movie> Movies { get; set; }
    public DbSet<Country> Countries { get; set; }
    // public DbSet<Rating> Ratings { get; set; }
    // public DbSet<Person> People { get; set; }
    public DbSet<Core.Models.Type> Types { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>()
        .HasKey(movie => movie.imdbID);

        modelBuilder.Entity<Movie>()
        .Property(movie => movie.imdbID)
        .ValueGeneratedNever();
        
        modelBuilder.Entity<Movie>()
        .HasOne(movie => movie.Type)
        .WithMany()
        .HasForeignKey("TypeID");
        
        modelBuilder.Entity<Rating>()
        .Property(rating => rating.RatingID);
    }
}
