using SiteParser.Parsers;
using SiteParser.Providers;
using SiteParser.Services;

namespace SiteParser;

class Program
{
    static async Task Main(string[] args)
    {
        // CancellationTokenSource for cancellation handling
        using var source = new CancellationTokenSource();

        // Create an instance of your DbContext
        await using var dbContext = new ParserDbContext();
        
        // Ensure the database is created and apply any pending migrations
        await dbContext.Database.EnsureCreatedAsync(source.Token);

        const string url = "https://refactoring.guru/refactoring/smells";
        var baseUri = new Uri("https://refactoring.guru");
        
        var provider = new AngleSharpProvider(baseUri);
        var parser = new WebParser(baseUri);

        // Fetch articles from the website
        var articles = await parser.ParseArticles(await provider.GetDocumentAsync(url));
        
        foreach (var article in articles)
        {
            // Parse internal links for each article
            article.InternalLinks = await parser.ParseInternalLinks(await provider.GetDocumentAsync(article.Url));
        }
        
        var saveArticlesService = new SaveArticles(dbContext);
        await saveArticlesService.SaveAsync(articles, source.Token);
    }
}
