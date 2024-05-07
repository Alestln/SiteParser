using Microsoft.EntityFrameworkCore;
using SiteParser.Commands;
using SiteParser.Entities;
using SiteParser.Parsers;
using SiteParser.Providers;

namespace SiteParser;

class Program
{
    static async Task Main(string[] args)
    {
        // CancellationTokenSource for cancellation handling
        using var source = new CancellationTokenSource();
        
        // Configure DbContext options
        var optionsBuilder = new DbContextOptionsBuilder<ParserDbContext>();
        optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=parser;Username=postgres;Password=123456");

        // Create an instance of your DbContext
        await using var dbContext = new ParserDbContext(optionsBuilder.Options);
        
        // Ensure the database is created and apply any pending migrations
        await dbContext.Database.EnsureCreatedAsync(source.Token);

        var baseUri = new Uri("https://refactoring.guru");
        var url = "https://refactoring.guru/refactoring/smells";
        
        var provider = new FlexibleUrlProvider(baseUri);
        var parser = new WebParser(baseUri);

        // Fetch articles from the website
        var articles = await parser.ParseArticles(await provider.GetDocumentAsync(url));

        var saveArticleService = new SaveArticle(dbContext);
        foreach (var article in articles)
        {
            // Parse internal links for each article
            article.InternalLinks = await parser.ParseInternalLinks(await provider.GetDocumentAsync(article.Url));

            // Save the article to the database
            await saveArticleService.SaveAsync(article, source.Token);
        }
    }
}
