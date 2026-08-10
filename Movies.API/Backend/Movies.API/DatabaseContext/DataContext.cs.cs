using Microsoft.EntityFrameworkCore;
using Movies.API.Mappings;
using Movies.API.Models;

namespace Movies.API.DatabaseContext;

public class DataContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<Movie> Movies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL")
            ?? new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build()
                .GetConnectionString("DefaultConnection");

        optionsBuilder.UseNpgsql(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new MovieMap());
    }
}
