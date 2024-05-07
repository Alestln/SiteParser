using Microsoft.EntityFrameworkCore;
using SiteParser.Entities;

namespace SiteParser;

public class ParserDbContext : DbContext
{
    private const string DbSchema = "refactoring_guru";
    private const string DbMigrationsHistoryTable = "__RefactoringGuruDbMigrationsHistory";
    
    public DbSet<Article> Articles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = "Server=localhost;Port=5432;Database=site_parser;Username=postgres;Password=postgres";
        optionsBuilder
            .UseNpgsql(connectionString,
                npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsHistoryTable(
                        DbMigrationsHistoryTable,
                        DbSchema);
                })
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }
    
    // Optionally, you can override the OnModelCreating method to configure the model
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(DbSchema);
        
        modelBuilder.Entity<Article>()
            .HasMany(a => a.InternalLinks)
            .WithOne()
            .HasForeignKey(il => il.ArticleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}