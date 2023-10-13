using Microsoft.EntityFrameworkCore;
using TechChallenge.Domain.Entities;

namespace TechChallenge.Infrastructure;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<News>? News { get; set; }

    public DbSet<User>? Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<News>()
            .ToTable("News")
            .HasKey(c => c.Id);

        modelBuilder.Entity<User>()
            .ToTable("Users")
            .HasKey(c => c.Id);

        base.OnModelCreating(modelBuilder);
    }
}