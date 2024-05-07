using Microsoft.EntityFrameworkCore;
using SiteParser.Entities;

namespace SiteParser;

public class ParserDbContext : DbContext
{
    public DbSet<Article> Articles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql("Server=localhost;Port=5432;Database=parser;Username=postgres;Password=123456")
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }
    
    // Optionally, you can override the OnModelCreating method to configure the model
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>()
            .HasMany(a => a.InternalLinks)
            .WithOne()
            .HasForeignKey(il => il.ArticleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}