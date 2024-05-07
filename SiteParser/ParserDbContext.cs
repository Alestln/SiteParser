using Microsoft.EntityFrameworkCore;
using SiteParser.Entities;

namespace SiteParser;

public class ParserDbContext : DbContext
{
    public DbSet<Article> Articles { get; set; }

    // Constructor to configure the DbContext
    public ParserDbContext(DbContextOptions<ParserDbContext> options) : base(options)
    {
    }

    // Optionally, you can override the OnModelCreating method to configure the model
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure relationships, if necessary
    }
}