using Microsoft.EntityFrameworkCore;

using TrailerPark.Core.Models;

namespace TrailerPark.Infrastructure.Config;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Movie> Movies { get; set; }
    // public DbSet<Rating> Ratings { get; set; }
    public DbSet<Person> People { get; set; }
    public DbSet<Core.Models.Type> Types { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>()
        .HasKey(m => m.imdbID);

        modelBuilder.Entity<Movie>()
        .Property(m => m.imdbID)
        .ValueGeneratedNever();

        modelBuilder.Entity<Movie>()
        .HasOne(m => m.Type)
        .WithMany()
        .HasForeignKey("TypeID");
        
        modelBuilder.Entity<Movie>()
        .HasOne(m => m.Director)
        .WithMany()
        .HasForeignKey("DirectorID");

        modelBuilder.Entity<Movie>()
        .HasOne(m => m.Writer)
        .WithMany()
        .HasForeignKey("WriterID");

    }
}
